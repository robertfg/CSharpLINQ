using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLINQ_OLd
{
    class Program3
    {
        static void Main3(string[] args)
        {
            // Object Initialization
            List<Bird> birds = new List<Bird>
            {
                new Bird { Name = "Cardinal", Color = "Red", Sightings = 3},
                new Bird { Name = "Dove", Color = "White", Sightings = 2},
                new Bird { Name = "Robin", Color = "Red", Sightings = 5},
                new Bird { Name = "Blue Jay", Color = "Blue", Sightings = 1},
                new Bird { Name = "Canary", Color = "Yellow", Sightings = 0},
                new Bird { Name = "Pigeon", Color = "White", Sightings = 10 },
                new Bird { Name = "Crow", Color = "Black", Sightings = 11 }
            };

            Console.WriteLine("List of birds:");
            foreach (Bird bird in birds)
            {
                Console.WriteLine(bird.Name);
            }

            // Standard
            var redBirds =
                from bird in birds
                where bird.Color == "Red"
                select bird;

            Console.WriteLine();
            Console.WriteLine("Red birds:");
            foreach (Bird bird in redBirds)
            {
                Console.WriteLine(bird.Name);
            }

            // Using Lambda
            //redBirds = birds.Where((bird) =>
            //{
            //    return bird.Color == "Red";
            //});

            //redBirds = birds.Where((bird) => bird.Color == "Red");

            redBirds = birds.Where(bird => bird.Color == "Red");

            Console.WriteLine();
            Console.WriteLine("Lambda Red birds:");
            foreach (Bird bird in redBirds)
            {
                Console.WriteLine(bird.Name);
            }

            // Birds in order
            var orderedBirds = birds.OrderBy(bird => bird.Name);

            Console.WriteLine();
            Console.WriteLine("Ordered birds:");
            foreach (Bird bird in orderedBirds)
            {
                Console.WriteLine(bird.Name);
            }

            // Birds in reverse order
            orderedBirds = birds.OrderByDescending(bird => bird.Name);

            Console.WriteLine();
            Console.WriteLine("Descending birds:");
            foreach (Bird bird in orderedBirds)
            {
                Console.WriteLine(bird.Name);
            }

            // Method chaining
            var chainedBirds = birds
                .Where(bird => bird.Color == "Red")
                .OrderByDescending(bird => bird.Name);

            Console.WriteLine();
            Console.WriteLine("Descending red birds:");
            foreach (Bird bird in chainedBirds)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            // Then by
            chainedBirds = birds
                .OrderBy(bird => bird.Name)
                .ThenBy(bird => bird.Sightings);

            Console.WriteLine();
            Console.WriteLine("Then by chaining:");
            foreach (Bird bird in chainedBirds)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            // Anonymous object
            var anonBirds = birds.Select(bird => new { bird.Name, bird.Color });

            Console.WriteLine();
            Console.WriteLine("Anonymous objects:");
            foreach (var bird in anonBirds)
            {
                Console.WriteLine(bird);
            }

            Console.ReadKey();
        }
    }
}
