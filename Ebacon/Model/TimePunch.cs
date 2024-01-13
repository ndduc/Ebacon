using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.Model
{
    public class TimePunch
    {
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public string Job {  get; set; }


        // calculate total hours for each obj
        // then rounding to nearest whole hour
        public double TotalWorkedHours => (End - Start).TotalHours;

    }
}
