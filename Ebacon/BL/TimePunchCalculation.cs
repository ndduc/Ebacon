using Ebacon.BL.Interfaces;
using Ebacon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebacon.BL
{
    public class TimePunchCalculation : ITimePunchCalculation
    {

        private readonly double _RegRate = 1;
        private readonly double _OTRate = 1.5;
        private readonly double _DTRate = 2;
        public TimePunchCalculation() { }  

        public Dictionary<string, EmployeeWageResult> GetEmployeeCalculatedWageResult(EmployeeCollection employeeCollection)
        {
            var empCol = TimePuchProcessing(employeeCollection);
            var empData = empCol.EmployeeData; ;

            Dictionary<string, EmployeeWageResult> empResult = empData.ToDictionary(
                x => x.Employee,
                x => new EmployeeWageResult()
                {
                    Employee = x.Employee,
                    Regular = x.TotalRegHrs,
                    OverTime = x.TotalOTHrs,
                    DoubleTime = x.TotalDTHrs,
                    WageTotal = x.WageTotal,
                    BenefitTotal = x.BenefitTotal
                }
            );

            return empResult;
        }

        private EmployeeCollection TimePuchProcessing(EmployeeCollection employeeCollection)
        {
            var jobData = employeeCollection.JobMeta;
            var employeeData = employeeCollection.EmployeeData;
            for (int i = 0; i < employeeData.Count; i++)
            {
                // Sorting timepunch
                employeeData[i].TimePunch = employeeData[i].TimePunch.OrderBy(x => x.Start).ThenBy(x => x.End).ToList();

                double totalHrs = 0;
                double totalRegHour = 0;
                double totalOTHour = 0;
                double totalDTHour = 0;

                double regWage = 0;
                double otWage = 0;
                double dtWage = 0;

                double benefit = 0;

                for (int j = 0; j < employeeData[i].TimePunch.Count; j++)
                {
                    var workedHrs = employeeData[i].TimePunch[j].TotalWorkedHours;
                    var currentJobId = employeeData[i].TimePunch[j].Job;
                    Occupation currentJob = jobData.FirstOrDefault(x => x.Job == currentJobId);

                    totalHrs += workedHrs;

                    double remainedHrs = 0;
                    /**
                        totalHrs = 42
                        totalRegHrs = 39
                        workedHrs = 3 
                     */
                    if (totalHrs <= 40)
                    {
                        var model = CalculatingWageAndHour(totalRegHour, regWage, benefit, workedHrs, currentJob, _RegRate);
                        totalRegHour = model.TotalHour;
                        regWage = model.Wage;
                        benefit = model.Benefit;
                    } 
                    else if (totalHrs > 40 && totalRegHour < 40) 
                    {
                        var reg = 40 - totalRegHour;
                        var model = CalculatingWageAndHour(totalRegHour, regWage, benefit, reg, currentJob, _RegRate);
                        totalRegHour = model.TotalHour;
                        regWage = model.Wage;
                        benefit = model.Benefit;

                        remainedHrs = workedHrs - reg;
                    }

                    /**
                      totalHrs = 49
                      total Ot = 7
                      worked hrs = 2
                    */
                    if (totalHrs > 40 && totalHrs <= 48)
                    {
                        if (remainedHrs > 0)
                        {
                            var model = CalculatingWageAndHour(totalOTHour, otWage, benefit, remainedHrs, currentJob, _OTRate);
                            totalOTHour = model.TotalHour;
                            otWage = model.Wage;
                            benefit = model.Benefit;
                            remainedHrs = 0;
                        } 
                        else 
                        {
                            var model = CalculatingWageAndHour(totalOTHour, otWage, benefit, workedHrs, currentJob, _OTRate);
                            totalOTHour = model.TotalHour;
                            otWage = model.Wage;
                            benefit = model.Benefit;
                        }
                    } 
                    else if (totalHrs > 48 && totalOTHour < 8)
                    {
                        var ot = 8 - totalOTHour;
                        var model = CalculatingWageAndHour(totalOTHour, otWage, benefit, ot, currentJob, _OTRate);
                        totalOTHour = model.TotalHour;
                        otWage = model.Wage;
                        benefit = model.Benefit;
                        remainedHrs = workedHrs - ot;
                    }

                    if (totalHrs > 48 && remainedHrs == 0)
                    {
                        var model = CalculatingWageAndHour(totalDTHour, dtWage, benefit, workedHrs, currentJob, _DTRate);
                        totalDTHour = model.TotalHour;
                        dtWage = model.Wage;
                        benefit = model.Benefit;
                    } 
                    else if (remainedHrs > 0)
                    {
                        var model = CalculatingWageAndHour(totalDTHour, dtWage, benefit, remainedHrs, currentJob, _DTRate);
                        totalDTHour = model.TotalHour;
                        dtWage = model.Wage;
                        benefit = model.Benefit;
                        remainedHrs = 0;
                    }
                }

                employeeData[i].TotalRegHrs = Math.Round(totalRegHour, 4);
                employeeData[i].TotalOTHrs = Math.Round(totalOTHour, 4);
                employeeData[i].TotalDTHrs = Math.Round(totalDTHour, 4);
                employeeData[i].WageTotal = Math.Round(regWage + otWage + dtWage, 4);
                employeeData[i].BenefitTotal = Math.Round(benefit, 4);

            }

            return employeeCollection;
        }

        private CalculationModel CalculatingWageAndHour(double totalHrs, double totalWage, double totalBenefit, double hrs, Occupation job, double extraRate)
        {
            CalculationModel model = new();
            model.TotalHour = totalHrs + hrs;
            model.Wage = totalWage + (hrs * (job.rate * extraRate));
            model.Benefit = totalBenefit + (hrs * job.benefitsRate);
            return model;
        }


    }
}
