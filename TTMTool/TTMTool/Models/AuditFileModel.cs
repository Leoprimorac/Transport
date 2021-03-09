using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMTool
{
    public class AuditFileModel
    {
        public string timeSheetKey { get; set; }
        public string day { get; set; }
        public string field { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public DateTime dateOfChange { get; set; }
        public string changedBy { get; set; }

    }
}
