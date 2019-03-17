using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bird> birds = BirdRepository.LoadBirds();

            Console.WriteLine("List of birds:");
            foreach (Bird bird in birds)
            {
                Console.WriteLine(bird.CommonName);
            }

            /*  **********  How much data do we have?   **********  */

            // Number of birds
            Console.WriteLine();
            Console.WriteLine("Total birds: " + birds.Count());

            // Number of sightings of all birds
            Console.WriteLine();
            Console.WriteLine("Total sightings: " + birds.SelectMany(bird => bird.Sightings).Count());

            // Average number of sightings of birds
            Console.WriteLine();
            Console.WriteLine("Average sightings: " + birds.Select(bird => bird.Sightings.Count()).Average());

            // List of countries:
            var countries = birds.SelectMany(bird => bird.Sightings).Select(sighting => sighting.Place.Country);
            Console.WriteLine();
            Console.WriteLine("Countries:");
            foreach(var country in countries)
            {
                Console.Write(country + ", ");
            }

            // Distinct list of countries:
            countries = birds.SelectMany(bird => bird.Sightings).Select(sighting => sighting.Place.Country).Distinct();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Distinct Countries:");
            foreach (var country in countries)
            {
                Console.Write(country + ", ");
            }

            // Group
            var grpSightings = birds
                .SelectMany(bird => bird.Sightings)
                .GroupBy(sighting => sighting.Place.Country)
                .Select(grp => new { Country = grp.Key, Sightings = grp.Count() });
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Group:");
            foreach (var grpSighting in grpSightings)
            {
                Console.Write(grpSighting.Country + ": " + grpSighting.Sightings + ", ");
            }

            /*  **********  Sightings of Endangered Birds   **********  */

            // Conservation statuses
            var statuses = birds
                .Select(bird => bird.ConservationStatus)
                .Distinct();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Conservation statuses:");
            foreach (var status in statuses)
            {
                Console.Write(status + ", ");
            }

            statuses = statuses
                .Where(bird => bird != "LeastConcern" &&  bird != "NearThreatened");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Our conservation statuses:");
            foreach (var status in statuses)
            {
                Console.Write(status + ", ");
            }

            // Endangered sightings
            var endangeredBirds = birds
                .Join(statuses,
                bird => bird.ConservationStatus,
                status => status,
                (bird, status) => new { Status = status, Birds = bird});
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Endangered birds:");
            foreach (var endBird in endangeredBirds)
            {
                Console.WriteLine(endBird.Status + ": " + endBird.Birds.CommonName);
            }

            var endangeredSightings = birds.Join(
                statuses,
                bird => bird.ConservationStatus,
                status => status,
                (bird, status) => new { Status = status, Sightings = bird.Sightings })
                .GroupBy(bird => bird.Status)
                .Select(bird => new { Status = bird.Key, Sightings = bird.Sum(status => status.Sightings.Count()) });
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Endangered sightings:");
            foreach (var endSi in endangeredSightings)
            {
                Console.Write(endSi.Status + ": " + endSi.Sightings + ", ");
            }

            /*  **********  Bird Importing  **********  */

            // Import birds
            var importedBirds = BirdRepository.LoadImportedBirds();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("List of imported birds:");
            foreach (Bird bird in importedBirds)
            {
                Console.WriteLine(bird.CommonName);
            }

            // Get common birds (INNER JOIN)
            var newBirds = importedBirds
                .Join(birds,
                ib => ib.CommonName,
                b => b.CommonName,
                (ib, b) => new { ImportedBirds = ib, Birds = b });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("List of duplicate birds:");
            foreach (var bird in newBirds)
            {
                Console.WriteLine(bird.ImportedBirds.CommonName + ", " + bird.Birds.CommonName);
            }

            // Get all unique birds (LEFT OUTER JOIN)
            var newBirds2 = importedBirds
                .GroupJoin(birds,
                ib => ib.CommonName,
                b => b.CommonName,
                (ib, b) => new { ImportedBird = ib, Birds = b })
                .SelectMany(gb => gb.Birds.DefaultIfEmpty(),
                (gb, b) => new { ImportedBird = gb.ImportedBird, Bird = b });

            Console.WriteLine();
            Console.WriteLine("List of all birds:");
            foreach (var bird in newBirds2)
            {
                if (bird.Bird != null)
                {
                    Console.WriteLine(bird.ImportedBird.CommonName + ", " + bird.Bird.CommonName);
                }
                else
                {
                    Console.WriteLine(bird.ImportedBird.CommonName);
                }
            }

            // Get all unique birds (LEFT OUTER JOIN), couldn't use newBirds2
            var newBirds3 = importedBirds
                .GroupJoin(birds,
                ib => ib.CommonName,
                b => b.CommonName,
                (ib, b) => new { ImportedBird = ib, Birds = b })
                .SelectMany(gb => gb.Birds.DefaultIfEmpty(),
                (gb, b) => new { ImportedBird = gb.ImportedBird, Bird = b })
                .Where(nb => nb.Bird == null)
                .Select(nb => nb.ImportedBird);

            Console.WriteLine();
            Console.WriteLine("List of new birds:");
            foreach (var bird in newBirds3)
            {
                Console.WriteLine(bird.CommonName);
            }

            // Import into list and get count:
            var newImportedBirds = newBirds3.ToList();
            birds.AddRange(newImportedBirds);
            Console.WriteLine("Total birds: " + birds.Count());

            /*  **********  Bird Search Extensions  **********  */
            var searchParameters = new BirdSearch
            {
                Size = "Medium",
                Country = "United States",
                Colors = new List<string> { "White", "Brown", "Black" },
                Page = 0,
                PageSize = 5
            };

            Console.WriteLine("Type any key to begin search.");
            while(Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine($"Page: {searchParameters.Page}");
                birds.Search(searchParameters).ToList().ForEach(b =>
                {
                    Console.WriteLine($"Common Name: {b.CommonName}");
                });

                searchParameters.Page++;
            }

            /*  **********  Which is Faster?  Union vs. Concat  **********  */
            var listA = Enumerable.Range(0, 100000);
            var listB = Enumerable.Range(50000, 100000);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var listC = listA.Union(listB);
            stopwatch.Stop();
            var unionTicks = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            var listD = listA.Concat(listB).Distinct();
            stopwatch.Stop();
            var concatTicks = stopwatch.ElapsedTicks;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Union vs. Concat");
            Console.WriteLine(string.Format("Union took {0} ticks.", unionTicks));
            Console.WriteLine(string.Format("Concat took {0} ticks.", concatTicks));

            if (unionTicks > concatTicks)
            {
                Console.WriteLine("Concat is faster by {0}", (unionTicks - concatTicks));
            }
            else if (concatTicks > unionTicks)
            {
                Console.WriteLine("Union is faster by {0}", (concatTicks - unionTicks));
            }

            Console.ReadKey();
        }
    }
}
