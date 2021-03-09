using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMTool
{
    public class TimeSheetModel
    {
        public int id { get; set; }
        public string type { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string breaks { get; set; }
        public string workTime { get; set; }
        public int? m3 { get; set; }
        public int? kmStand { get; set; }
        public int? privat { get; set; }
        public int? fuel { get; set; }
        public int? adblue { get; set; }
        public string notes { get; set; }
        public int UserId { get; set; }
        public DateTime entryDate { get; set; }
        public string timeSheetKey { get; set; }
        public string timeSheetStatus { get; set; }
    }
}
