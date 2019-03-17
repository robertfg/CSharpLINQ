using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    public static class ContainsAnyExtension
    {
        public static bool ContainsAny(this string source, IEnumerable<string> stringsToMatch)
        {
            bool match = false;
            foreach(string str in stringsToMatch)
            {
                if (source.Contains(str))
                {
                    match = true;
                }
            }
            return match;
        }
    }
}
