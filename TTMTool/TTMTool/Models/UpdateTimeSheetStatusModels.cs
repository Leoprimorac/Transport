using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMTool
{
    public class UpdateTimeSheetStatusModels
    {
        public int userId { get; set; }
        public DateTime monthStart { get; set; }
        public DateTime monthEnd { get; set; }
        public string status { get; set; }
    }
}
