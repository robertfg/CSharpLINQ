using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    class NumberAnalysis
    {
        private List<int> _numbers;
        public NumberAnalysis()
        {
            _numbers = new List<int> { 2, 4, 6, 8, 10 };
        }

        public IEnumerable<int> NumbersGreaterThanFive()
        {
            return from number in _numbers
                where number > 5
                select number;
        }
    }
}
