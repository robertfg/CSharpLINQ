using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program_Actions
    {
        delegate void SayGreeting(string name);

        static void Main_Actions(string[] args)
        {
            // Action
            Action<string> sayGreeting;
            sayGreeting = delegate (string name)
            {
                Console.WriteLine(string.Format("Hello, {0}!", name));
            };

            Console.Write("What's your name? ");
            string input = Console.ReadLine();
            sayGreeting(input);

            Console.ReadLine();
            sayGreeting = delegate (string name)
            {
                Console.WriteLine(string.Format("Goodbye, {0}!", name));
            };
            sayGreeting(input);

            Console.ReadKey();
        }
    }
}
