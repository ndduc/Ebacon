using Ebacon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.BL.Interfaces
{
    public interface IDataReader
    {
        public EmployeeCollection GetEmployeeCollectionData();
    }
}
