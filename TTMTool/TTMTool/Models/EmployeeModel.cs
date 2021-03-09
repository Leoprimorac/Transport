using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMTool
{
    public class EmployeeModel : EmployeeLoginModel
    {
        
        public int id { get; set; }
        public int personalNo { get; set; }
        public int PW { get; set; }
        public string name { get; set; }
        public string FM { get; set; }
        public string level { get; set; }
        public DateTime entryDate { get; set; }
        public string email { get; set; }



    }
}
