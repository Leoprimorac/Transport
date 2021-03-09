using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMTool
{
    public class EditTimeSheetModel
    {
        public int id { get; set; }
        public string breaks { get; set; }
        public string changedBy { get; set; }
        public string day { get; set; }
        public string endTime { get; set; }
        public string startTime { get; set; }
        public string timeSheetKey { get; set; }
        public int userId { get; set; }
    }
}
