using System.Runtime.CompilerServices;

namespace Programming_Paradagim_C_
{
    public static class EnumerableExtension
    {
        public static IEnumerable<int> SelectExtension<TSource>(this IEnumerable<TSource> source, Func<TSource, int> targetFunction)
        {
            //Function SelectExtension Take in Paramter another function() --> Higher Order Function
            foreach (var item in source)
            {
                yield return targetFunction(item);
            }
        }
        public static IEnumerable<TSource> WhereExtension<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> targetFunction)
        {
            foreach(var item in source)
            {
                if(targetFunction(item))
                {
                    yield return item;
                }
            }
        }
    }
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
        public static bool filter_2(int x)
        {
            return x > 3;
        }
        public static bool filter_1(int x)
        {
            return x > 0;
        }

        public static IEnumerable<int> FP_Test2(List<int> numbers)
        {
            //1. addone 
            //2. Filter By values >0
            //3. Sub 2
            //4. Filter By values >3
            //5. Multiple By 2
            Console.WriteLine("*************Using Functional Programming(ExtensionMethod)**************");

            return   numbers.SelectExtension(AddOne)
                            .WhereExtension(filter_1)
                            .SelectExtension(Sub)
                            .WhereExtension(filter_2)
                            .SelectExtension(Mult);

        }
        public static IEnumerable<int> FP_Test1(List<int> numbers)
        {
            //1. addone 
            //2. Filter By values >0
            //3. Sub 2
            //4. Filter By values >3
            //5. Multiple By 2
            Console.WriteLine("*************Using Functional Programming(using Linq)**************");

            return numbers.Select(AddOne)
                            .Where(filter_1)
                            .Select(Sub)
                            .Where(filter_2)
                            .Select(Mult);

        }
        public static List<int> Proc_Test1(List<int> numbers)
        {
            Console.WriteLine("*************Using Procdural Programming**************");
            List<int> result = new List<int>();
            //1. addone 
            //2. Filter By values >0
            //3. Sub 2
            //4. Filter By values >3
            //5. Multiple By 2
            foreach (int i in numbers)
            {
                var addingResult = AddOne(i);
                var firstFilter = filter_1(addingResult);
                if (firstFilter)
                {
                    var subResult = Sub(addingResult);
                    var secondFilter = filter_2(subResult);
                    if (secondFilter)
                    {
                        result.Add(Mult(subResult));
                    }
                }
            }
            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>() {-1,0,1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            #region Imperative Programming
                #region Procedural Programming
                        var result = Calculation.Proc_Test1(numbers);
                        foreach (int i in result)
                        {
                             Console.WriteLine(i);
                        }
                 #endregion
            #endregion


            #region Declartive Programming
                #region Functional Programming

                        //1. addone 
                        //2. Filter By values >0
                        //3. Sub 2
                        //4. Filter By values >3
                        //5. Multiple By 2
                        Calculation.FP_Test1(numbers).ToList().ForEach(x=>Console.WriteLine(x));
                        
                        Calculation.FP_Test2(numbers).ToList().ForEach(x => Console.WriteLine(x));

            #endregion
            #endregion
        }
    }
}
