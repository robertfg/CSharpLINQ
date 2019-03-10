using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLINQ
{
    class Program1
    {
        static void Main1(string[] args)
        {
            List<int> numbers = new List<int> { 2, 4, 8, 16, 32, 64 };

            // foreach Loop:
            List<int> numbersGreaterThanTen = new List<int>();
            foreach (int number in numbers)
            {
                if (number > 10)
                {
                    numbersGreaterThanTen.Add(number);
                }
            }

            foreach (int number in numbersGreaterThanTen)
            {
                Console.Write(number + ", ");
            }
            Console.WriteLine();

            // LINQ Query:
            //IEnumerable<int> filterQuery = from number in numbers where number > 10 select number;
            IEnumerable<int> filterQuery =
                from number in numbers
                where number > 10
                select number;

            foreach (int number in filterQuery)
            {
                Console.Write(number + ", ");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
