using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DTOs;

namespace Demo.DAL
{
    /// <summary>
    /// Repository that mimics data access to a Db context. Hardcoded for demonstration puroposes
    /// </summary>
    public class FakeRepository : IDisposable
    {
        //Structured this way so that usage within repository will mimic context/entity usage in entity framework.
        private class FakeDbContext
        {
            public List<ItemModelDTO> FakeEntity;
        }

        //Context to be used throughout the repository.
        private FakeDbContext FakeContext;
        
        #region Fake context creation

        /// <summary>
        /// Contructor populates FakeContext.
        /// </summary>
        /// <param name="recordCount">Optional parameter. Intended to allow the user
        /// to test performance by varying page size.</param>
        public FakeRepository(int recordCount = 100)
        {
            var fakeContext = new FakeDbContext();

            if (recordCount > 0)
            {
                fakeContext.FakeEntity = CreateRecordsForContextList(recordCount);
            }
            else
            {
                throw new Exception("recordCount must be a positive, non-zero integer.");
            }

            FakeContext = fakeContext;
        }

        /// <summary>
        /// Selects a name at random from a bank.
        /// </summary>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private string PickANameForSingleRecord(Random rnd)
        {
            if(rnd !=  null)
            {
                var ng = new NamePicker();
                return ng.PickAName(rnd);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Creates a list of records.
        /// </summary>
        /// <param name="recordCount">Sets the number of records to be created for FakeContext</param>
        /// <returns></returns>
        private List<ItemModelDTO> CreateRecordsForContextList(int recordCount)
        {
            var recordList = new List<ItemModelDTO>();

            if (recordCount > 0)
            {
                var rnd = new Random();

                //Populates record with ItemModelDTOs that vary on id, name, and organization.
                for (int i = 0; i < recordCount / 3; ++i)
                {
                    recordList.Add(CreateSingleRecord(3 * i, "This Description", "Sales", rnd));
                    recordList.Add(CreateSingleRecord(3 * i + 1, "That Description", "Accounting", rnd));
                    recordList.Add(CreateSingleRecord(3 * i + 2, "Other Description", "Marketing", rnd));
                }

                //The following blocks resolve cases where recordCount is not divisible by three, meaning
                //the block above has not finished creating the last one or two records.
                if (recordCount % recordList.Count() != 0)
                {
                    recordList.Add(CreateSingleRecord(recordCount - (recordCount % recordList.Count()), "ThisDescription", "Sales", rnd));
                }
                if (recordCount % recordList.Count() == 1)
                {
                    recordList.Add(CreateSingleRecord(recordCount - 1, "ThatDescription", "Accounting", rnd));
                }
            }
            else
            {
                throw new Exception("recordCount must be a positive integer.");
            }
            
            return recordList;
        }

        /// <summary>
        /// Creates a single ItemModelDTO
        /// </summary>
        /// <param name="counter">Used to set ItemId</param>
        /// <param name="description"></param>
        /// <param name="org">organization</param>
        /// <param name="basePrice">Used to avoid Price == 0</param>
        /// <param name="rnd">Allows random variation between records.</param>
        /// <returns></returns>
        private ItemModelDTO CreateSingleRecord(int counter, string description, string org, Random rnd)
        {
            if (counter > -1 && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(org) && rnd != null)
            {
                var record = new ItemModelDTO
                {
                    ItemId = counter + 1,
                    ItemName = description + " " + (counter + 1).ToString(),
                    Price = 1000.00M * rnd.Next(5, 21),
                    Organization = org,
                    POCName = PickANameForSingleRecord(rnd),
                    DateEstablished = DateTime.Now.AddMonths(-rnd.Next(6, 25)),
                    DateBegin = DateTime.Now.AddMonths(rnd.Next(-6, 13))
                };

                return record;
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }

        #endregion

        #region Data access

        /// <summary>
        /// Gets a list of records filtered by organization.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public List<ItemModelDTO> GetFilteredItems(string organization)
        {
            var filteredItems = new List<ItemModelDTO>();

            if (!string.IsNullOrWhiteSpace(organization))
            {
                filteredItems = FakeContext.FakeEntity.Where(x => x.Organization.ToLower() == organization.ToLower())
                                                      .OrderBy(x => x.ItemId)
                                                      .ToList();
            }

            return filteredItems;
        }

        /// <summary>
        /// Returns list of ItemModelDTO's
        /// </summary>
        public List<ItemModelDTO> GetItems()
        {
            //TODO this method doesn't do anything other than return FakeEntity. A parameter for a filter model
            //can be added if needed in the future.
            return FakeContext.FakeEntity.OrderBy(x => x.ItemId).ToList();
        }

        /// <summary>
        /// Gets a list of all organizations.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOrganizations()
        {
            var organizations = FakeContext.FakeEntity.Select(x => x.Organization).Distinct().ToList();
            return organizations;
        }

        /// <summary>
        /// Gets a list of all product representatives.
        /// </summary>
        /// <returns></returns>
        public List<string> GetRepresentatives()
        {
            var representatives = FakeContext.FakeEntity.Select(x => x.POCName).Distinct().ToList();
            return representatives;
        }

        /// <summary>
        /// Gets a list of records filtered by date.
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <param name="dateCategory">Determines whether records are sorted by DateEstablished or DateBegin</param>
        /// <returns>List of records sorted by date</returns>
        public List<ItemModelDTO> GetRecordsByDate(DateTime minDate, DateTime maxDate, string dateCategory)
        {
            minDate = minDate == null ? DateTime.MinValue : minDate;
            maxDate = maxDate == null ? DateTime.MaxValue : maxDate;

            var records = new List<ItemModelDTO>();

            if (minDate >= maxDate)
            {
                if (!string.IsNullOrWhiteSpace(dateCategory))
                {
                    switch (dateCategory.ToLower())
                    {
                        case "dateestablished":
                            records = FakeContext.FakeEntity.Where(x => x.DateEstablished >= minDate
                                                                        && x.DateEstablished <= maxDate)
                                                                    .ToList();
                            break;

                        case "datebegin":
                            records = FakeContext.FakeEntity.Where(x => x.DateBegin >= minDate
                                                                        && x.DateBegin <= maxDate)
                                                                    .ToList();
                            break;

                        default:
                            throw new Exception("Invalid input");
                    }
                }
                else
                {
                    throw new Exception("dateCategory cannot be null");
                }
            }
            else
            {
                throw new Exception("minDate cannot be greater than maxDate.");
            }

            return records;
        }

        /// <summary>
        /// Gets a list of records filtered by price.
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        public List<ItemModelDTO> GetRecordsByPrice(decimal minPrice, decimal maxPrice)
        {
            var records = new List<ItemModelDTO>();

            if (minPrice >= maxPrice)
            {
                records = FakeContext.FakeEntity.Where(x => x.Price >= minPrice
                                                            && x.Price <= maxPrice)
                                                .ToList();
            }
            else
            {
                throw new Exception("minPrice cannot be greater than maxPrice");
            }

            return records;
        }

        /// <summary>
        /// Utility class for selecting a name at random from a hard-coded list.
        /// </summary>
        private class NamePicker
        {
            private readonly string[] SomeNames;

            public string PickAName(Random rnd)
            {
                //return a random name from SomeNames
                return SomeNames[rnd.Next(0, 8)];
            }

            public NamePicker()
            {
                SomeNames = new string[]
                {
                "Michael Scott",
                "Dwight Schrute",
                "Pam Beasley",
                "Kevin Malone",
                "Andrew Bernard",
                "Stanley Hudson",
                "Todd Packer",
                "Phyllis Vance"
                };
            }
        }

        #endregion

        #region IDisposable Support
        //Disposable support is added here so that repository can be used similar to how
        //a business-sized repository might.

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FakeRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
