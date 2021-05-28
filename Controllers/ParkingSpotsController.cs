using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Controllers
{
    public class ParkingSpotsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IAdministratorRepository _administratorRepository;
        private readonly ILogger<ParkingSpotsController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ParkingSpotsController(IUserRepository userRepository,
            IParkingSpotRepository parkingSpotRepository,
            IAdministratorRepository administratorRepository,
            ILogger<ParkingSpotsController> logger,
            SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _parkingSpotRepository = parkingSpotRepository;
            _administratorRepository = administratorRepository;
            _logger = logger;
            _signInManager = signInManager;
        }
        
        public IActionResult AddParkingSpotToUser(string username, int parkingSpotId)
        {
            bool status = _parkingSpotRepository.AddParkingSpotToUser(
                _userRepository.GetByUsername(username),
                _parkingSpotRepository.GetById(parkingSpotId));
            if (status)
                return RedirectToAction("ParkingSpots");
            return RedirectToAction("Error");
        }
        
        public IActionResult AddParkingSpotToUserFromModel(ParkingSpotsViewModel model)
        {
            bool status = _parkingSpotRepository.AddParkingSpotToUser(
                _userRepository.GetByUsername(model.SelectedUser),
                _parkingSpotRepository.GetById(model.SelectedParkingSpot));
            if (status)
                return RedirectToAction("ParkingSpots");
            return RedirectToAction("Error");
        }

        public IActionResult GetRandomParkingSpotForUser(string username)
        {
            User user = _userRepository.GetByUsername(username);
            ParkingSpot parkingSpot = _parkingSpotRepository.GetAll().ElementAt(new Random().Next(0, _parkingSpotRepository.GetAll().Count()));
            bool status = _parkingSpotRepository.AddParkingSpotToUser(user, parkingSpot);
            if (status)
                return RedirectToAction("FindParkingSpot");
            if (username == User.Identity?.Name)
                return RedirectToAction("ParkingSpots");

            return RedirectToAction("Error");
        }
        
        public IActionResult RemoveParkingSpotFromUser(string username, int parkingSpotId)
        {
            bool status = _parkingSpotRepository.RemoveParkingSpotFromUser(
                _userRepository.GetByUsername(username),
                _parkingSpotRepository.GetById(parkingSpotId));
            if (status)
                return RedirectToAction("ParkingSpots");
            return RedirectToAction("Error");
        }

        public IActionResult ReleaseParkingSpotFromUser(string username)
        {
            User user = _userRepository.GetByUsername(username);
            bool status = _parkingSpotRepository.RemoveParkingSpotFromUser(
                user, _parkingSpotRepository.GetById(user.ParkSpot.Id));
            if (status)
                return RedirectToAction("MyParkingSpot");
            return RedirectToAction("Error");
        }

        public IActionResult OccupyParkingSpot(string username)
        {
            _parkingSpotRepository.OccupyOwnParkingSpot(
                _userRepository.GetByUsername(username));
            return RedirectToAction("MyParkingSpot");
        }

        // GET /parkingspots
        public IActionResult ParkingSpots()
        {
            ParkingSpotsViewModel model = new ParkingSpotsViewModel();
            model.ParkingSpots = _parkingSpotRepository.GetAll().ToList();
            List<string> list1 = new List<string>();
            foreach (var user in _userRepository.GetAll().ToArray())
            {
                list1.Add(user.UserName);
            }
            model.UserList = list1;
            List<string> list2 = new List<string>();
            foreach (var parkingSpot in _parkingSpotRepository.GetAll().ToArray())
            {
                list2.Add(parkingSpot.Id.ToString());
            }

            model.ParkingSpotList = list2;
            return View(model);
        }

        // GET /findparkingspots
        public IActionResult FindParkingSpot()
        {
            return View(_userRepository.GetByUsername(User.Identity?.Name));
        }
        
        // GET /myparkingspot
        public IActionResult MyParkingSpot()
        {
            return View(_userRepository.GetByUsername(User.Identity?.Name));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}