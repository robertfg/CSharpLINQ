using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    class Delegates
    {
        delegate int MathOperation(int number);

        public static int Add(int number)
        {
            return number + number;
        }

        public static int Square(int number)
        {
            return number * number;
        }

        static void MainDel(string[] args)
        {
            MathOperation add = new MathOperation(Add);
            MathOperation square = new MathOperation(Square);
        }
    }
}
