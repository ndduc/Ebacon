using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.Model.View
{
    public class EmployeeWageResultVm
    {
        public string employee { get; set; }
        public string regular {  get; set; }    
        public string overtime { get; set; }
        public string doubletime { get; set; }
        public string wageTotal { get; set; }
        public string benefitTotal { get; set; }

        public static explicit operator EmployeeWageResultVm(EmployeeWageResult model)
        {
            return new EmployeeWageResultVm()
            {
                employee = model.Employee,
                regular = model.Regular.ToString("0.0000"),
                overtime = model.OverTime.ToString("0.0000"),
                doubletime = model.DoubleTime.ToString("0.0000"),
                wageTotal = model.WageTotal.ToString("0.0000"),
                benefitTotal = model.BenefitTotal.ToString("0.0000")
            };
        }
    }
}
