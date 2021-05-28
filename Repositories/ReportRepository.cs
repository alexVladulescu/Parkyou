using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Parkyou.Data;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Report> GetReportsByUsername(string username)
        {
            return _context.Reports.Where(r => r.ReportedBy == username).ToList();
        }

        public Report GetReportById(string id)
        {
            return _context.Reports.FirstOrDefault(r => r.Id == id);
        }

        public List<Report> GetReportsByRow(string row)
        {
            return _context.Reports.Where(r => r.Row == row).ToList();
        }

        public List<Report> GetReportsByCol(string col)
        {
            return _context.Reports.Where(r => r.Col == col).ToList();
        }

        public List<Report> GetReportsByRowAndCol(string row, string col)
        {
            return _context.Reports.Where(r => r.Row == row && r.Col == col).ToList();
        }

        public List<Report> GetAllReports()
        {
            return _context.Reports.ToList();
        }

        public List<Report> GetAllSolvedReports()
        {
            return _context.Reports.Where(r => r.Solved).ToList();
        }

        public List<Report> GetAllUnsolvedReports()
        {
            return _context.Reports.Where(r => !r.Solved).ToList();
        }



        public bool EditReport(string id, string title, string description, string row, string col)
        {
            Report report = GetReportById(id);
            if (report == null)
                return false;
            report.Title = title;
            report.Description = description;
            report.Row = row;
            report.Col = col;
            report.LastModified = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public bool SolveReport(Report report, string resolution)
        {
            if (report == null)
                return false;
            report.Solved = true;
            report.LastModified = DateTime.Now;
            if (!resolution.IsNullOrEmpty())
                report.Resolution = "";
            else report.Resolution = resolution;
            _context.SaveChanges();
            return true;
        }

        public bool AddResolution(string id, string resolution)
        {
            Report report = GetReportById(id);
            if (report == null)
                return false;
            report.Solved = true;
            report.Resolution = resolution;
            report.LastModified = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public bool CloseReport(Report report)
        {
            if (report == null)
                return false;
            report.Closed = true;
            report.LastModified = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public bool CreateNewReport(Report report)
        {
            if (report == null || report.Title.IsNullOrEmpty() || report.Description.IsNullOrEmpty() || report.ReportedBy.IsNullOrEmpty())
                return false;
            _context.Reports.Add(report);
            _context.SaveChanges();
            return true;
        }
    }
}