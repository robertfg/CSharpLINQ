using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenges
{
    public static class BirdRepository
    {
        public static List<Bird> LoadBirds()
        {
            var birds = new List<Bird>();
            birds.Add(new Bird { Name = "Pelican", Color = "White", Sightings = 3 });
            /*var pelican = new Bird();
            pelican.Name = "Pelican";
            pelican.Color = "White";
            pelican.Sightings = 3;
            birds.Add(pelican);*/
            return birds;
        }
    }
}
