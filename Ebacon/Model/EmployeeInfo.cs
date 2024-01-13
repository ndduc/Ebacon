using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.Model
{
    public class EmployeeInfo
    {
        public string Employee { get; set; }
        public IList<TimePunch> TimePunch { get; set; }

        public double TotalRegHrs { get; set; } 
        public double TotalOTHrs {  get; set; }
        public double TotalDTHrs {  get; set; }
        public double WageTotal {  get; set; }
        public double BenefitTotal { get;set; }
    }
}
