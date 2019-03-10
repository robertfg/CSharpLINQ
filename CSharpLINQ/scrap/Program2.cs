using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLINQ
{
    class Program2
    {
        static void Main2(string[] args)
        {
            // Object Initialization
            List<Bird> birds = new List<Bird>
            {
                new Bird { Name = "Cardinal", Color = "Red", Sightings = 3},
                new Bird { Name = "Dove", Color = "White", Sightings = 2}
            };

            // Add a bird
            birds.Add(new Bird { Name = "Robin", Color = "Red", Sightings = 5});

            // Add another bird (old way):
            Bird blueJay = new Bird();
            blueJay.Name = "Blue Jay";
            blueJay.Color = "Blue";
            blueJay.Sightings = 1;

            birds.Add(blueJay);

            Console.WriteLine("List of birds:");
            foreach (Bird bird in birds)
            {
                Console.WriteLine(bird.Name);
            }
 
            // Add another with implicit typing:
            var canary = new Bird { Name = "Canary", Color = "Yellow", Sightings = 0};
            birds.Add(canary);

            Console.WriteLine();
            Console.WriteLine("Count, more birds:");
            Console.WriteLine(birds.Count());
            foreach (var bird in birds)
            {
                Console.WriteLine(bird.Name);
            }

            IEnumerable<string> redBirds =
                from bird in birds
                where bird.Color == "Red"
                select bird.Name;

            Console.WriteLine();
            Console.WriteLine("redBirds:");
            foreach (var bird in redBirds)
            {
                Console.Write(bird + ", ");
            }

            /* Doesn't print out recognizable data:
            IEnumerable<object> rb =
                from b in birds
                where b.Color == "Red"
                select b;

            Console.WriteLine();
            foreach (var bird in rb)
            {
                Console.Write(bird + ", ");
            }*/

            //Projection:
            IEnumerable<object> rb =
                from b in birds
                where b.Color == "Red"
                select new { b.Name, b.Color };

            Console.WriteLine();
            Console.WriteLine("redBirds projection:");
            foreach (var bird in rb)
            {
                Console.Write(bird + ", ");
            }


            rb = from b in birds
                where b.Color == "Red"
                select new { BirdName = b.Name, BirdColor = b.Color };

            Console.WriteLine();
            Console.WriteLine("redBird objects:");
            foreach (var bird in rb)
            {
                Console.Write(bird + ", ");
            }

            // Create anonymous variables:
            var anonymousPigeon = new { Name = "Pigeon", Color = "White", Sightings = 10 };
            var anonymousCrow = new { Name = "Crow", Color = "Black", Sightings = 11 };

            // These are the same type, so C# sees them as the same
            Console.WriteLine();
            if (anonymousCrow.GetType() == anonymousPigeon.GetType())
            {
                Console.WriteLine("Same type");
            }

            // To add them to the Bird class, however, you must do this:
            birds.Add(new Bird {
                Name = anonymousCrow.Name,
                Color = anonymousCrow.Color,
                Sightings = anonymousCrow.Sightings
            });

            birds.Add(new Bird
            {
                Name = anonymousPigeon.Name,
                Color = anonymousPigeon.Color,
                Sightings = anonymousPigeon.Sightings
            });

            // Order
            //IEnumerable<string> orderedBirds =
            //    from b in birds
            //    //orderby b.Name
            //    orderby b.Name descending
            //    select b.Name;

            IEnumerable<object> orderedBirds =
                from b in birds
                orderby b.Color, b.Sightings descending
                select new { b.Name, b.Sightings };

            Console.WriteLine();
            Console.WriteLine("orderedBirds:");
            foreach (var bird in orderedBirds)
            {
                Console.WriteLine(bird);
            }

            var birdsByColor =
                from b in birds
                group b by b.Color;

            Console.WriteLine();
            Console.WriteLine("birdsByColor:");
            foreach (var bird in birdsByColor)
            {
                Console.WriteLine(bird.Key + " " + bird.Count());
            }

            var birdsByColor2 =
                from b in birds
                group b by b.Color into birdsByColor3
                where birdsByColor3.Count() > 1
                select new { Color = birdsByColor3.Key, Count = birdsByColor3.Count() };

            Console.WriteLine();
            Console.WriteLine("birdsByColor2:");
            foreach (var bird in birdsByColor2)
            {
                Console.WriteLine(bird);
            }

            Console.ReadKey();
        }
    }
}
