using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program_Delegates
    {
        delegate void SayGreeting(string name);

        // Clunky:
        //public static void SayHello(string name)
        //{
        //    Console.WriteLine(string.Format("Hello, {0}!", name));
        //}

        public static void SayGoodbye(string name)
        {
            Console.WriteLine(string.Format("Goodbye, {0}!", name));
        }

        static void Main_Delegates(string[] args)
        {
            //SayGreeting sayGreeting = new SayGreeting(SayHello);

            // Anonymous method:
            SayGreeting sayGreeting = delegate (string name)
            {
                Console.WriteLine(string.Format("Hello, {0}!", name));
            };

            Console.Write("What's your name? ");
            string input = Console.ReadLine();
            sayGreeting(input);

            Console.ReadLine();
            sayGreeting = new SayGreeting(SayGoodbye);
            sayGreeting(input);

            Console.ReadKey();
        }
    }
}
