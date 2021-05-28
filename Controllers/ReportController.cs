using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportRepository reportRepository, 
            IParkingSpotRepository parkingSpotRepository,
            ILogger<ReportController> logger)
        {
            _reportRepository = reportRepository;
            _parkingSpotRepository = parkingSpotRepository;
            _logger = logger;
        }

        public IActionResult CreateNewReport(ReportModel model)
        {
            Report report = new Report
            {
                Title = model.Title,
                Description = model.Description,
                Row = model.Row,
                Col = model.Col,
                ReportedBy = model.ReportedBy,
                Solved = false,
                Resolution = "",
                Closed = false,
                Created = DateTime.Now,
                LastModified = null
            };
            bool status = _reportRepository.CreateNewReport(report);
            if (status)
                return RedirectToAction("CreateReport");
            return RedirectToAction("Error");
        }

        public IActionResult SolveReport(string id, string resolution)
        {
            Report report = _reportRepository.GetReportById(id);
            bool status = _reportRepository.SolveReport(report, resolution);
            if (status)
                return RedirectToAction("Report", new { id = id });
            return RedirectToAction("Error");
        }
        
        public IActionResult AddResolution(Report model)
        {
            bool status = _reportRepository.AddResolution(model.Id, model.Resolution);
            if (status)
                return RedirectToAction("Report", new { id = model.Id });
            return RedirectToAction("Error");
        }

        public IActionResult CloseReport(string id)
        {
            Report report = _reportRepository.GetReportById(id);
            bool status = _reportRepository.CloseReport(report);
            if (status)
                return RedirectToAction("MyReports", new { username = report.ReportedBy });
            return RedirectToAction("Error");
        }
        
        public IActionResult CloseReportAdmin(string id)
        {
            Report report = _reportRepository.GetReportById(id);
            bool status = _reportRepository.CloseReport(report);
            if (status)
                return RedirectToAction("AllReports");
            return RedirectToAction("Error");
        }

        // GET
        public IActionResult CreateReport()
        {
            ReportModel model = new ReportModel();
            model.RowList = new List<string>();
            model.ColList = new List<string>();
            foreach (var parkingSpot in _parkingSpotRepository.GetAll().ToArray())
            {
                if (!model.RowList.Contains(parkingSpot.Row))
                    model.RowList.Add(parkingSpot.Row);
                if (!model.ColList.Contains(parkingSpot.Col.ToString()))
                    model.ColList.Add(parkingSpot.Col.ToString());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditReport(Report model)
        {
            bool validationError = false;
            string rowError = null;
            string colError = null;
            List<string> rowList = _parkingSpotRepository.GetRowList();
            rowList.Sort();
            if (!rowList.Contains(model.Row))
            {
                validationError = true;
                rowError = "Row must be in range " + rowList.First() + "-" + rowList.Last();
            }
            List<string> colList = _parkingSpotRepository.GetColList();
            colList.Sort();
            if (!colList.Contains(model.Col))
            {
                validationError = true;
                colError = "Column must be in range " + colList.First() + "-" + colList.Last();
            }

            if (validationError)
                return RedirectToAction("Report", new {id = model.Id, rowError = rowError, colError = colError});

            bool status = _reportRepository.EditReport(model.Id, model.Title, model.Description, model.Row, model.Col);
            if (!status)
                return RedirectToAction("Error");
            return RedirectToAction("Report", new {id = model.Id});
        }

        /*// GET
        public IActionResult Report(string id)
        {
            return View(_reportRepository.GetReportById(id));
        }*/
        
        public IActionResult Report(string id, string rowError, string colError)
        {
            if (!rowError.IsNullOrEmpty())
                ModelState.AddModelError("Row", rowError);
            else ModelState.ClearValidationState("Row");
            
            if (!colError.IsNullOrEmpty()) 
                ModelState.AddModelError("Col", colError);
            else ModelState.ClearValidationState("Col");
            
            ModelState.ClearValidationState("Title");
            ModelState.ClearValidationState("Description");
            return View(_reportRepository.GetReportById(id));
        }

        public IActionResult MyReports(string username)
        {
            ReportListView model = new ReportListView {Reports = _reportRepository.GetReportsByUsername(username)};
            return View(model);
        }
        
        public IActionResult AllReports()
        {
            ReportListView model = new ReportListView {Reports = _reportRepository.GetAll().ToList()};
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}