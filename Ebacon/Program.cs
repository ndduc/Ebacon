using Ebacon.BL;
using Ebacon.BL.Interfaces;
using Ebacon.Model.View;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;

string path = "C:\\Users\\ndduc\\Desktop\\Ebacon\\Ebacon\\Ebacon\\Resources\\data1.json";
string output = "C:\\Users\\ndduc\\Desktop\\Ebacon\\Ebacon\\Ebacon\\Resources\\Output\\output.json";
IDataReader reader = new DataReader(path);
ITimePunchCalculation timePunchCalculation = new TimePunchCalculation();

var data = reader.GetEmployeeCollectionData(); ;
var processedData = timePunchCalculation.GetEmployeeCalculatedWageResult(data);
var processedDataVm = new Dictionary<string, EmployeeWageResultVm>();


foreach (var kvp in processedData)
{
    processedDataVm[kvp.Key] = (EmployeeWageResultVm)kvp.Value;
}

string dataString = JsonConvert.SerializeObject(processedDataVm, Formatting.Indented);
Console.WriteLine(dataString);

try
{
    File.WriteAllText(output, dataString);
}
catch (Exception ex)
{
    Console.WriteLine("Error occurred: " + ex.Message);
}