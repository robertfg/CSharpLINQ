using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    class Partitioning
    {
        public void Partitions()
        {
            var birds = new List<Bird>
            {
                new Bird { Name = "Cardinal", Color = "Red", Sightings = 3 },
                new Bird { Name =  "Dove", Color = "White", Sightings = 2 },
                new Bird { Name =  "Robin", Color = "Red", Sightings = 5 },
                new Bird { Name =  "Canary", Color = "Yellow", Sightings = 0 },
                new Bird { Name =  "Blue Jay", Color = "Blue", Sightings = 1 },
                new Bird { Name =  "Crow", Color = "Black", Sightings = 11 },
                new Bird { Name =  "Pidgeon", Color = "White", Sightings = 10 }
            };

            var firstThreeBirds = birds.Take(3);
            var nextThreeBirds = birds.Skip(3).Take(3);
        }
    }
}
