using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace FRF_Function_Return_Function
{
 
    public static class Calculation
    {
        public static int AddOne(int x)
        {
            return x + 1;
        }
        public static int Sub(int x)
        {
            return x - 2;
        }

        public static int Mult(int x)
        {
            return x * 2;
        }
    }
    public static class FRF
    {
        #region FRF_Test1
            public static Func<double, double> FRFTest = Test_1();

            public static Func<double, double> Test_1()
            {
                return input => input * 10.5;
            }
        #endregion

        #region V1
        public static IEnumerable<int> FP_V1(List<int> numbers)
        {
            Console.WriteLine("*************Using Functional Programming Multi Select**************");

            return numbers.Select(Calculation.AddOne)
                            .Select(Calculation.Sub)
                            .Select(Calculation.Mult);

        }
        #endregion

        #region V2
            public static IEnumerable<int> FP_V2(List<int> numbers)
            {
                Console.WriteLine("\n*************Using Functional Programming (Composed Function)**************");

                return numbers.Select(ComposedFunction);
            }

            public static Func<int, int> ComposedFunction = CreateComposedFunction(Calculation.AddOne, Calculation.Sub, Calculation.Mult);
            public static Func<int, int> CreateComposedFunction(Func<int, int> AddOne, Func<int, int> Sub, Func<int, int> Mult)
            {
                return input => Mult(Sub(AddOne(input)));
            }

        #endregion

        #region V3
            public static IEnumerable<int> FP_V3(List<int> numbers)
            {
                Console.WriteLine("\n*************Using Functional Programming (Composed Function V2)**************");

                return numbers.Select(CreateComposedFunction_V2());
            }
            public static Func<int, int> CreateComposedFunction_V2()
            {
                //Apply First the add one then sub then multi
                Func<int, int> Add=Calculation.AddOne;
                Func<int, int> Sub = Calculation.Sub;
                Func<int, int> Multi = Calculation.Mult;
                return Add.Compose(Sub).Compose(Multi);
            }
            public static Func<T1, T3> Compose<T1, T2, T3>( this Func<T1, T2> First, Func<T2, T3> Second)
            {
                return input => Second(First(input));
            }
        #endregion

    }

    internal class Program  
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>() { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };



            #region Declartive Programming
            #region Functional Programming
            FRF.FRFTest(5); //Test FRF

            //V1-Problem Exist Multi Select 
            FRF.FP_V1(numbers).ToList().ForEach(x => Console.WriteLine(x));

            //V2-Solve Problem Multi Select 
            //But exist another probelm --> not genric can exist multi functions not take int and return int..
            FRF.FP_V2(numbers).ToList().ForEach(x => Console.WriteLine(x));


            //V3 More Generic Using Extension Method
            FRF.FP_V3(numbers).ToList().ForEach(x => Console.WriteLine(x));

            #endregion
            #endregion
        }
    }
}
