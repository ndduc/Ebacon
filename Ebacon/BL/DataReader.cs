using Ebacon.BL.Interfaces;
using Ebacon.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.BL
{
    public class DataReader : IDataReader
    {
        private string dataPath;
        public DataReader(string dataPath)
        {
            this.dataPath = dataPath;
        }

        public EmployeeCollection GetEmployeeCollectionData()
        {
            string dataString = File.ReadAllText(dataPath);
            /***
             Flaw: if date time string is messed up the date time type in related object will won't work!
             */
            EmployeeCollection dataCollection = JsonConvert.DeserializeObject<EmployeeCollection>(dataString);
            return dataCollection;
        }
    }
}
