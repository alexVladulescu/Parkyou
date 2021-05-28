using System.Collections.Generic;
using Parkyou.Models;

namespace Parkyou.Interfaces
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        public List<Report> GetReportsByUsername(string username);
        public Report GetReportById(string id);
        public List<Report> GetReportsByRow(string row);
        public List<Report> GetReportsByCol(string col);
        public List<Report> GetReportsByRowAndCol(string row, string col);
        public List<Report> GetAllReports();
        public List<Report> GetAllSolvedReports();
        public List<Report> GetAllUnsolvedReports();
        public bool EditReport(string id, string title, string description, string row, string col);
        public bool SolveReport(Report report, string resolution);
        public bool AddResolution(string id, string resolution);
        public bool CloseReport(Report report);
        public bool CreateNewReport(Report report);
        
    }
}