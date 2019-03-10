using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    public class FuncActionLambda
    {
        // Lambda
        public Func<int, int> Square = (number) =>
        //public Func<int, int> Square = delegate (int number)
        {
            return number * number;
        };

        // Lambda
        public Action<int, Func<int, int>> DisplayResult = (result, operation) =>
        //public Action<int, Func<int, int>> DisplayResult = delegate (int result, Func<int, int> operation)
        {
            Console.WriteLine(operation(result));
        };
    }
}
