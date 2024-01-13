using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.Model
{
    public class EmployeeCollection
    {
        public List<Occupation> JobMeta { get; set; }
        public List<EmployeeInfo> EmployeeData { get; set; }
    }
}
