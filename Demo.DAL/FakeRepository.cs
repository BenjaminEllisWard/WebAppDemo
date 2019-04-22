using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DTOs;
using Demo.GeneralFunctions;

namespace Demo.DAL
{
    /// <summary>
    /// Hardcoded repository meant to mimic data access to a Db context for demonstration puroposes
    /// </summary>
    public class FakeRepository : IDisposable
    {
        private List<ItemModelDTO> FakeContext;

        /// <summary>
        /// Returns list of items
        /// </summary>
        public List<ItemModelDTO> GetItems()
        {
            //TODO this method doesn't do anything other than return FakeContext. A parameter for a filter model
            //can be added to add some logic if needed in the future.

            var fakeContext = new List<ItemModelDTO>();

            fakeContext = FakeContext.OrderBy(x => x.ItemId).ToList();

            return fakeContext;
        }

        private string PickANameForFakeContext()
        {
            var ng = new NameGenerator();

            //Sleep used to enforce random pick in the case that method is called in a loop.
            //TODO 5 milliseconds picked arbitrarily after 1 millisecond produced unexpected result. Test out shorter sleep time.
            System.Threading.Thread.Sleep(5);

            return ng.PickAName();
        }

        
                
        private void CreateFakeContext()
        {
            var fakeContext = new List<ItemModelDTO>();
            var nameGenerator = new NameGenerator();

            //Populates FakeContext with ItemModelDTOs that vary on id, name, and organization.
            for (int i = 0; i < 5; i++)
            {
                //Used to avoid price == zero
                decimal basePrice = 1000.00M;

                fakeContext.Add(new ItemModelDTO {
                    ItemId = i,
                    ItemName = "ThisDescription" + i.ToString(),
                    Price = basePrice + basePrice * i,
                    Organization = "Sales",
                    POCName = PickANameForFakeContext(),
                    DateEstablished = DateTime.Now.AddMonths(i - 12),
                    DateBegin = DateTime.Now.AddMonths(i - 2)
                });

                fakeContext.Add(new ItemModelDTO
                {
                    ItemId = i + 5,
                    ItemName = "ThatDescription" + i.ToString(),
                    Price = basePrice + basePrice * i,
                    Organization = "Accounting",
                    POCName = PickANameForFakeContext(),
                    DateEstablished = DateTime.Now.AddMonths(i - 12),
                    DateBegin = DateTime.Now.AddMonths(i - 2)
                });

                fakeContext.Add(new ItemModelDTO
                {
                    ItemId = i + 10,
                    ItemName = "OtherDescription" + i.ToString(),
                    Price = basePrice + basePrice * i,
                    Organization = "Marketing",
                    POCName = PickANameForFakeContext(),
                    DateEstablished = DateTime.Now.AddMonths(i - 12),
                    DateBegin = DateTime.Now.AddMonths(i - 2)
                });

                FakeContext = fakeContext;
            }
        }

        /// <summary>
        /// Contructor automatically populates FakeContext.
        /// </summary>
        public FakeRepository()
        {
            CreateFakeContext();
        }

        #region IDisposable Support
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
