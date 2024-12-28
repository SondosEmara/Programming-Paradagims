using System.Numerics;
using System.Xml.Linq;

namespace Closure_Session_7
{
    internal class Program
    {
        #region Simple Example
            public static Func<int, int> Add(int factor)
            {
                int addingfactor = factor * 2;
                return (num) => num + addingfactor;
            }

        #endregion

        #region Real World Example
           
        public static double CalGrossSalary_WithoutClosure(double basicSalary,double bonus)
        {
            double tax =basicSalary*0.5;
            return basicSalary+ tax + bonus;
        }
        public static class SalaryCalculation
        {
            public static List<(string segement, double basicSalary)> SalarySegements = new();
            public static List<(string segement, Func<double,double> GrossSalarryFunc)> SegemstsWithGrossSalary=new();
            public static Func<double, double> CalGrossSalary_WithoClosure(double basicSalary)
            {
                double tax = basicSalary * 0.5;
                return bonus=> basicSalary + tax + bonus;
            }
            static SalaryCalculation()
            {
                SalarySegements = new() 
                { 
                  ("A",1000),
                  ("B",2000)
                };

                SegemstsWithGrossSalary = SalarySegements.Select(tuple =>(segement:tuple.segement, GrossSalarryFunc:CalGrossSalary_WithoClosure(tuple.basicSalary) )).ToList() ;
            }
        }
       
        #endregion

        static void Main(string[] args)
        {
            #region Simple Exmaple
                var addingFunc=Add(5);
                Console.WriteLine(addingFunc(10));
            #endregion

            #region Without Closure
            Console.WriteLine(CalGrossSalary_WithoutClosure(1000, 20));
            CalGrossSalary_WithoutClosure(1000, 30);
            CalGrossSalary_WithoutClosure(1000, 40);
            CalGrossSalary_WithoutClosure(1000, 50);
            CalGrossSalary_WithoutClosure(1000, 90);
            CalGrossSalary_WithoutClosure(1000, 100);
            //Note Can Exist a  devloper problem 
            //the fixed amout always is tax , the diferent value between employee and anotehr bonus value...
            //Every Call Will Calculate the Tax ..
            #endregion

            #region With Closure
            var value=SalaryCalculation.SegemstsWithGrossSalary.FirstOrDefault(tuple => tuple.segement == "A").GrossSalarryFunc(20);
            Console.WriteLine(value);
            #endregion

        }
    }
}
