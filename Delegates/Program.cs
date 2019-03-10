using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        delegate void SayGreeting(string name);

        static void Main(string[] args)
        {
            // Action
            Action<string> sayGreeting;

            // Lambda
            //Func<string, string> conversate = delegate (string message)
            Func<string, string> conversate = (message) =>
            {
                Console.Write(message + "  ");
                return Console.ReadLine();
            };

            string input = conversate("What's your name? ");

            // Lambda
            //sayGreeting = delegate (string greeting)
            sayGreeting = (greeting) =>
            {
                Console.WriteLine(string.Format(greeting, input));
            };

            sayGreeting("Hello, {0}!");
            conversate("Nice to see you.");
            conversate("Are you doing well?");
            sayGreeting("Goodbye, {0}!");

            Console.ReadKey();
        }
    }
}
