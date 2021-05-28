using System.Collections.Generic;

namespace Parkyou.Models
{
    public class ReportModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Row { get; set; }
        public string Col { get; set; }
        public string ReportedBy { get; set; }
        
        public List<string> RowList { get; set; }
        public List<string> ColList { get; set; }
    }
}