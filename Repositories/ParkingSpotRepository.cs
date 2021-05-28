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


        public ParkingSpot GetParkingSpot(string row, int col)
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

        public List<ParkingSpot> GetParkingSpotsByRow(string row)
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

        public List<string> GetRowList()
        {
            HashSet<string> list = new HashSet<string>();
            foreach (var ps in GetAll())
            {
                list.Add(ps.Row);
            }

            return list.ToList();
        }

        public List<string> GetColList()
        {
            HashSet<string> list = new HashSet<string>();
            foreach (var ps in GetAll())
            {
                list.Add(ps.Col.ToString());
            }

            return list.ToList();
        }
        
        public bool AddParkingSpotToUser(User user, ParkingSpot parkingSpot)
        {
            if (user == null || parkingSpot == null)
                return false;
            if (user.ParkSpot != null && user.ParkSpot != parkingSpot)
            {
                GetByUser(user).UserName = "";
                GetByUser(user).Status = 0;
                user.ParkSpot = null;
            }

            if (user.ParkSpot == null)
            {
                parkingSpot.Status = 1;
                parkingSpot.UserName = user.UserName;
                user.ParkSpot = parkingSpot;
                _context.SaveChanges();
            }
            else return false;
            return true;
        }

        public bool RemoveParkingSpotFromUser(User user, ParkingSpot parkingSpot)
        {
            if (parkingSpot != null)
            {
                parkingSpot.UserName = "";
                parkingSpot.Status = 0;
            }
            else 
                return false;

            if (user != null) 
                user.ParkSpot = null;
            else 
                return false;
            
            _context.SaveChanges();
            return true;
        }

        public void OccupyOwnParkingSpot(User user)
        {
            user.ParkSpot.Status = 2;
            _context.SaveChanges();
        }
    }
}