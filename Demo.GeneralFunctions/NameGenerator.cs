using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.GeneralFunctions
{
    /// <summary>
    /// General function class for picking a name at random. For demonstration purposes.
    /// </summary>
    public class NameGenerator
    {
        private readonly string[] SomeNames;

        public string PickAName()
        {
            var rnd = new Random();

            

            //return a random name from SomeNames
            return SomeNames[rnd.Next(0, 3)];
        }

        public NameGenerator()
        {
            SomeNames = new string[]
            {
                "Michael Scott",
                "Dwight Schrute",
                "Pam Halpert"
            };
        }
    }
}
