using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Parkyou.Models
{
    public class ParkingSpotsViewModel
    {
        public List<ParkingSpot> ParkingSpots;
        public List<string> UserList;
        public List<string> ParkingSpotList;
        public string SelectedUser { get; set; }
        public int SelectedParkingSpot { get; set; }
    }
}