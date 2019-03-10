using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    class NumberAnalysis2
    {
        private List<int> _numbers;

        public NumberAnalysis2()
        {
            _numbers = new List<int> { 2, 4, 6, 8, 10 };
        }

        //public IEnumerable<int> NumbersGreaterThanFive2()
        //{
        //    return from n in _numbers where n > 5 select n;
        //}

        public IEnumerable<int> NumbersGreaterThanFive2()
        {
            return _numbers.Where(n => n > 5);
        }

        // THE NEXT TWO DO NOT WORK:
        //public IEnumerable<int> NumbersGreaterThanFive2 =
        //  from n in _numbers
        //  where n > 5
        //  select n;

        //public IEnumerable<int> NumbersGreaterThanFive = _numbers.Where(n => n > 5);

        public IEnumerable<int> ReverseNumbers()
        {
            return _numbers.OrderByDescending(n => n);
        }
    }
}
