using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            var birds = new[]
            {
                new { Name = "Pelican", Color = "White" },
                new { Name = "Swan", Color = "White" },
                new { Name = "Crow", Color = "Black" }
            };

            var mysteryBird = new { Color = "White", Sightings = 3 };

            var matchingBirds =
                from bird in birds
                where bird.Color == mysteryBird.Color
                select new { BirdName = bird.Name };

            Console.ReadKey();
        }
    }
}
