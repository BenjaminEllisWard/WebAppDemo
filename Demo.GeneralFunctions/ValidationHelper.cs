using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL;

namespace Demo.GeneralFunctions
{
    public class ValidationHelper
    {
        /// <summary>
        /// Uses whitelist approach to validate input against organizations found in FakeRepository.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static bool ValidateOrganization(string organization)
        {
            var organizations = new List<string>();

            using (var repository = new FakeRepository())
            {
                organizations = repository.GetOrganizations();
            }

            return organizations.Contains(organization);
        }
    }
}
