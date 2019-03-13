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
            Console.WriteLine("Sparrows in list: " + !sparrowsExist);

            /*  *****   Elements    *****   */

            // Single
            var element = birds.Where(bird => bird.Name == "Crow").Single();
            Console.WriteLine();
            Console.WriteLine("Element: " + element.Name);

            element = birds.Single(bird => bird.Name == "Crow");
            Console.WriteLine("Element: " + element.Name);

            element = birds.SingleOrDefault(bird => bird.Name == "Eagle");

            // Default for strings is null
            if ( element == null)
            {
                Console.WriteLine("Element does not exist.");
            }
            else
            {
                Console.WriteLine("Element: " + element.Name);
            }

            // Default for integers is 0
            var numbers = new List<int> { 2, 4, 8, 26 };
            //var num = numbers.SingleOrDefault(n => n == 26);
            var num = numbers.SingleOrDefault(n => n == 99);

            if (num == 0)
            {
                Console.WriteLine("Number does not exist.");
            }
            else
            {
                Console.WriteLine("Number: " + num);
            }

            // ERROR: more than one bird in list
            //element = birds.Single();

            // First
            element = birds.First();
            Console.WriteLine("First Element: " + element.Name);

            // Last
            element = birds.Last();
            Console.WriteLine("Last Element: " + element.Name);

            // Last
            element = birds.ElementAt(2);
            Console.WriteLine("Element 2: " + element.Name);

            /*  *****   Elements    *****   */

            // Take
            //var subset = birds.Take(3);
            var subset = birds
                .OrderBy(bird => bird.Name)
                .Take(3);

            Console.WriteLine();
            Console.WriteLine("Take:");
            foreach (Bird bird in subset)
            {
                Console.WriteLine(bird.Name);
            }

            // Skip
            subset = birds
                .OrderBy(bird => bird.Name)
                .Skip(3)
                .Take(3);

            Console.WriteLine();
            Console.WriteLine("Skip:");
            foreach (Bird bird in subset)
            {
                Console.WriteLine(bird.Name);
            }

            // TakeWhile
            subset = birds
                // This produces no results:
                //.OrderBy(bird => bird.Name)
                .OrderBy(bird => bird.Name.Length)
                .TakeWhile(bird => bird.Name.Length < 6);

            Console.WriteLine();
            Console.WriteLine("TakeWhile:");
            foreach (Bird bird in subset)
            {
                Console.WriteLine(bird.Name);
            }

            // SkipWhile
            subset = birds
                .OrderBy(bird => bird.Name.Length)
                .SkipWhile(bird => bird.Name.Length < 6);

            Console.WriteLine();
            Console.WriteLine("SkipWhile:");
            foreach (Bird bird in subset)
            {
                Console.WriteLine(bird.Name);
            }

            /*  *****   Joins   *****   */

            // Join Query Syntax
            var colors = new List<string> { "Red", "Blue", "Purple" };

            var favoriteBirds =
                from bird in birds
                join color in colors
                on bird.Color equals color
                select bird;

            Console.WriteLine();
            Console.WriteLine("Join query syntax:");
            foreach (Bird bird in favoriteBirds)
            {
                Console.WriteLine(bird.Name + " " + bird.Color);
            }

            // Join Method Syntax
            favoriteBirds = birds.Join(colors,
                bird => bird.Color,
                color => color,
                (bird, color) => bird);

            Console.WriteLine();
            Console.WriteLine("Method syntax:");
            foreach (Bird bird in favoriteBirds)
            {
                Console.WriteLine(bird.Name + " " + bird.Color);
            }

            // Anonymous function
            // I couldn't use favoriteBirds, because I already defined as Bird, not this anonymous object
            var faveBirds = birds.Join(colors,
                bird => bird.Color,
                color => color,
                (bird, color) => new { Color = color, Bird = bird });

            Console.WriteLine();
            Console.WriteLine("Method syntax:");
            // Can't use Bird bird, since faveBirds is not a Bird object
            foreach (var bird in faveBirds)
            {
                Console.WriteLine(bird.Color + ", " + bird.Bird.Name + ", " + bird.Bird.Color + ", " + bird.Bird.Sightings);
            }

            // GroupJoin Method Syntax
            var groupedBirds = colors.GroupJoin(birds,
                color => color,
                bird => bird.Color,
                (color, bird) => new { Color = color, Birds = bird })
                .Select(grp => grp.Color);

            Console.WriteLine();
            Console.WriteLine("GroupJoin:");
            // Here bird is a string:
            foreach (var bird in groupedBirds)
            {
                Console.WriteLine(bird);
            }

            // Grouped birds
            var groupBirds = colors.GroupJoin(birds,
                color => color,
                bird => bird.Color,
                (color, bird) => new { Color = color, Birds = bird });

            // Create other variables to manipulate the list
            var gb1 = groupBirds.Select(grp => grp.Color);
            Console.WriteLine();
            foreach (var bird in gb1)
            {
                Console.WriteLine(bird);
            }

            // Group birds variation
            var gb2 = groupBirds
                //.Where(grp => grp.Color == "Red")
                .Where(grp => grp.Color == "Purple")
                //.Select(grp => grp.Color);
                .Select(grp => grp.Birds)
                .ElementAt(0);
            Console.WriteLine();
            //Console.WriteLine(gb2.ElementAt(0).Count());
            Console.WriteLine(gb2.Count());
            foreach (var bird in gb2)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            // GroupMany
            var gb3 = groupBirds.SelectMany(grp => grp.Birds);
            Console.WriteLine();
            foreach (var bird in gb3)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            /*  *****   Aggregates  *****   */

            // Count - I don't need "grp"; it could be just "bird"
            var birdCount = birds
                .GroupBy(bird => bird.Color)
                .Select(grp => new { Color = grp.Key, Count = grp.Count() });

            Console.WriteLine();
            Console.WriteLine("Aggregates");
            Console.WriteLine("Count:");
            foreach (var bird in birdCount)
            {
                Console.WriteLine(bird.Color + ": " + bird.Count);
            }

            // Sum
            var birdSum = birds.Sum(bird => bird.Sightings);
            Console.WriteLine();
            Console.WriteLine("Sum: " + birdSum);

            var birdSightings = birds
                .GroupBy(bird => bird.Color)
                .Select(grp => new { Color = grp.Key, Count = grp.Sum(bird => bird.Sightings) });

            Console.WriteLine();
            Console.WriteLine("Sum of sightings:");
            foreach (var bird in birdSightings)
            {
                Console.WriteLine(bird.Color + ": " + bird.Count);
            }

            // Average
            var birdAverage = birds.Average(bird => bird.Sightings);
            Console.WriteLine();
            Console.WriteLine("Average: " + birdAverage);

            // Min
            var birdMin = birds.Min(bird => bird.Sightings);
            Console.WriteLine();
            Console.WriteLine("Minimum: " + birdMin);

            // Max
            var birdMax = birds.Max(bird => bird.Sightings);
            Console.WriteLine();
            Console.WriteLine("Maximum: " + birdMax);

            /*  *****   Set Operations  *****   */

            // Distinct
            var distinctBirds = birds.Select(bird => bird.Color).Distinct();
            Console.WriteLine();
            Console.WriteLine("Set Operations");
            Console.WriteLine("Distinct:");
            foreach (var bird in distinctBirds)
            {
                Console.WriteLine(bird);
            }

            // Except
            var exceptColors = colors
                .Except(birds.Select(bird => bird.Color).Distinct());
            Console.WriteLine();
            Console.Write("Except:  ");
            foreach (var bird in exceptColors)
            {
                Console.Write(bird + ", ");
            }

            // Except
            var unionColors = colors
                .Union(birds.Select(bird => bird.Color).Distinct());
            Console.WriteLine();
            Console.Write("Union:  ");
            foreach (var bird in unionColors)
            {
                Console.Write(bird + ", ");
            }

            // Intersect
            var intColors = colors
                .Intersect(birds.Select(bird => bird.Color).Distinct());
            Console.WriteLine();
            Console.Write("Intersect:  ");
            foreach (var bird in intColors)
            {
                Console.Write(bird + ", ");
            }

            // Concat
            var concatColors = colors
                .Concat(birds.Select(bird => bird.Color));
            Console.WriteLine();
            Console.Write("Concat:  ");
            foreach (var bird in concatColors)
            {
                Console.Write(bird + ", ");
            }

            // Same as union
            unionColors = colors
                .Concat(birds.Select(bird => bird.Color)).Distinct();
            Console.WriteLine();
            Console.Write("Union using Concat:  ");
            foreach (var bird in unionColors)
            {
                Console.Write(bird + ", ");
            }

            /*  *****   Generation Operations  *****   */

            Console.WriteLine("\n\n");
            Console.WriteLine("Generation Operations");

            // Range
            IEnumerable<int> rangeNum = Enumerable.Range(10, 10);
            Console.Write("Range:  ");
            foreach (var rNum in rangeNum)
            {
                Console.Write(rNum + ", ");
            }

            // Repeat
            var repeatNum = Enumerable.Repeat("LINQ is awesome!", 10);
            Console.WriteLine();
            Console.Write("Repeat:  ");
            foreach (var rNum in repeatNum)
            {
                Console.Write(rNum + ", ");
            }

            var blankBirds = Enumerable.Repeat(new Bird { }, 5);
            Console.WriteLine();
            Console.Write("Repeat:  ");
            foreach (var bird in blankBirds)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            // Empty
            var emptyBirds = Enumerable.Empty<Bird>();
            Console.WriteLine();
            Console.Write("Empty:  ");
            foreach (var bird in emptyBirds)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            // DefaultIfEmpty

            var emptyNum = Enumerable.Empty<int>();
            Console.WriteLine();
            Console.Write("DefaultIfEmpty:  ");
            Console.Write(emptyNum.Count() + "  |  ");    // Count = 0
            // Doesn't work:
            //Console.Write(emptyNum.ElementAt(0) + "  |  ");
            Console.Write(emptyNum + "  |  ");
            Console.Write(emptyNum.DefaultIfEmpty() + "  |  ");

            int[] array = emptyNum.ToArray();
            Console.WriteLine(array.Length);        // 0

            /*  *****   Conversion Operations  *****   */

            Console.WriteLine();
            Console.WriteLine("Conversion Operations");

            // ToList
            Console.WriteLine();
            Console.WriteLine("ToList:  ");

            var tolBirds = birds
                .Where(bird => bird.Color == "Red")
                .ToList();

            foreach (var bird in tolBirds)
            {
                Console.WriteLine(bird.Name + ", " + bird.Color + ", " + bird.Sightings);
            }

            // Last example:
            numbers = new List<int> { 2, 4, 8, 16, 34 };
            numbers.Where(n => n > 10);

            Console.ReadKey();
        }
    }
}
