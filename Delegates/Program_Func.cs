using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program_Func
    {
        delegate void SayGreeting(string name);

        static void Main_Func(string[] args)
        {
            // Action
            Action<string> sayGreeting;

            // Func
            Func<string, string> conversate = delegate (string message)
            {
                Console.Write(message + "  ");
                return Console.ReadLine();
            };

            string input = conversate("What's your name? ");

            sayGreeting = delegate (string greeting)
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
