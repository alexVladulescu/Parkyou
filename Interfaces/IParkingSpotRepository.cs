using System.Collections.Generic;
using Parkyou.Models;

namespace Parkyou.Interfaces
{
    public interface IParkingSpotRepository : IGenericRepository<ParkingSpot>
    {
        public ParkingSpot GetParkingSpot(string row, int col);
        public List<ParkingSpot> GetFreeParkingSpots();
        public List<ParkingSpot> GetReservedParkingSpots();
        public List<ParkingSpot> GetOccupiedParkingSpots();
        public List<ParkingSpot> GetParkingSpotsByRow(string row);
        public List<ParkingSpot> GetParkingSpotsByColumn(int col);
        public ParkingSpot GetByUser(User user);
        public ParkingSpot GetByUser(string userEmail);
        public List<string> GetRowList();
        public List<string> GetColList();
        public bool AddParkingSpotToUser(User user, ParkingSpot parkingSpot);
        public bool RemoveParkingSpotFromUser(User user, ParkingSpot parkingSpot);
        public void OccupyOwnParkingSpot(User user);
    }
}