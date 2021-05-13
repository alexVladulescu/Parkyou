using System.Collections.Generic;
using System.Linq;
using Parkyou.Data;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Repositories
{
    public partial class ParkingSpotRepository : GenericRepository<ParkingSpot>, IParkingSpotRepository
    {
        public ParkingSpotRepository(ApplicationDbContext context) : base(context)
        {
        }


        public ParkingSpot GetParkingSpot(int row, int col)
        {
            return _context.ParkingSpots.FirstOrDefault(ps => ps.Row == row && ps.Col == col);
        }

        public List<ParkingSpot> GetFreeParkingSpots()
        {
            return _context.ParkingSpots.Where(ps => ps.Status == 0).ToList();
        }

        public List<ParkingSpot> GetReservedParkingSpots()
        {
            return _context.ParkingSpots.Where(ps => ps.Status == 1).ToList();
        }

        public List<ParkingSpot> GetOccupiedParkingSpots()
        {
            return _context.ParkingSpots.Where(ps => ps.Status == 2).ToList();
        }

        public List<ParkingSpot> GetParkingSpotsByRow(int row)
        {
            return _context.ParkingSpots.Where(ps => ps.Row == row).ToList();
        }

        public List<ParkingSpot> GetParkingSpotsByColumn(int col)
        {
            return _context.ParkingSpots.Where(ps => ps.Col == col).ToList();
        }

        public ParkingSpot GetByUser(User user)
        {
            return _context.AppUsers.FirstOrDefault(u => u.Email == user.Email)?.ParkSpot;
        }

        public ParkingSpot GetByUser(string userEmail)
        {
            return _context.AppUsers.FirstOrDefault(u => u.Email == userEmail)?.ParkSpot;
        }

        public bool AddParkingSpotToUser(User user, ParkingSpot parkingSpot)
        {
            if (user == null || parkingSpot == null || this.GetByUser(user) != null || user.ParkSpot != parkingSpot)
                return false;
            user.ParkSpot = parkingSpot;
            _context.SaveChanges();
            return true;
        }

        public bool RemoveParkingSpotFromUser(User user, ParkingSpot parkingSpot)
        {
            if (user == null || parkingSpot == null || this.GetByUser(user) != null || user.ParkSpot != parkingSpot)
                return false;
            user.ParkSpot = null;
            _context.SaveChanges();
            return true;
        }
    }
}