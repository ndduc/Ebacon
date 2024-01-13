using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.Model
{
    public class EmployeeWageResult
    {
        public string Employee { get; set; }
        public double Regular { get; set; }
        public double OverTime { get; set; }
        public double DoubleTime {  get; set; }
        public double WageTotal { get; set; }
        public double BenefitTotal { get; set; }
    }
}
