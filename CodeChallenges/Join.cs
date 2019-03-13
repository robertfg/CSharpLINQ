using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    class Join
    {
        public void Joins()
        {
            var myBirds = new List<Bird>
            {
                new Bird { Name = "Cardinal", Color = "Red", Sightings = 3 },
                new Bird { Name =  "Dove", Color = "White", Sightings = 2 },
                new Bird { Name =  "Robin", Color = "Red", Sightings = 5 }
            };

            var yourBirds = new List<Bird>
            {
                new Bird { Name =  "Dove", Color = "White", Sightings = 2 },
                new Bird { Name =  "Robin", Color = "Red", Sightings = 5 },
                new Bird { Name =  "Canary", Color = "Yellow", Sightings = 0 }
            };

            var ourBirds = myBirds.Join(yourBirds,
                myBird => myBird.Name,
                yourBird => yourBird.Name,
                (myBird, yourBird) => myBird);

            var sumOfSightings = ourBirds.Sum(bird => bird.Sightings);

            var averageSightings = ourBirds.Average(bird => bird.Sightings);
        }
    }
}
