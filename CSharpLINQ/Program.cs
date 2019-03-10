using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //BirdRepository birdRepo = new BirdRepository();
            List<Bird> birds = new BirdRepository().birdRepo;

            Console.WriteLine("List of birds:");
            //foreach (Bird bird in birdRepo.birds)
            foreach (Bird bird in birds)
            {
                Console.WriteLine(bird.Name);
            }

            /*  *****   Quantifiers *****   */

            // Any
            var crowsExist = birds.Any(bird => bird.Name == "Crow");
            Console.WriteLine();
            Console.WriteLine("Crows in list: " + crowsExist);

            if (!birds.Any(bird => bird.Name == "Crow"))
            {
                birds.Add(new Bird { Name = "Crow", Color = "Black", Sightings = 11 });
            }
            Console.WriteLine("Birds in list: " + birds.Any());

            // Contains
            var sparrow = new Bird { Name = "Sparrow", Color = "Brown" };
            if ( !birds.Contains(sparrow))
            {
                birds.Add(sparrow);
            }

            Console.WriteLine();
            Console.WriteLine("List of birds:");
            foreach (Bird bird in birds)
            {
                Console.WriteLine(bird.Name);
            }

            // All
            var sparrowsExist = birds.All(bird => bird.Name != "Sparrow");
            Console.WriteLine();
            Console.WriteLine("Sparrows in list: " + sparrowsExist);

            Console.ReadKey();
        }
    }
}
