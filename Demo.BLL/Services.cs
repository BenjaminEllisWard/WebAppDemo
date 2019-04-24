using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL;
using Demo.DTOs;

namespace Demo.BLL
{
    /// <summary>
    /// Currently just a pass-through from UI to DAL for demonstration purposes.
    /// Logic could be added here if needed in the future.
    /// </summary>
    public class Services
    {
        /// <summary>
        /// Get items from repository
        /// </summary>
        /// <returns></returns>
        private List<ItemModelDTO> GetItems()
        {
            var items = new List<ItemModelDTO>();

            using (var repo = new FakeRepository())
            {
                items = repo.GetItems();
            }

            return items;
        }

        /// <summary>
        /// Filter model is created here instead of in DAL from a db context for demonstration purposes.
        /// </summary>
        /// <param name="items">Item list from page model</param>
        /// <returns>filter model for page model</returns>
        private FilterModelDTO GetFilterOptionsForPageModel(List<ItemModelDTO> items)
        {
            var filterModel = new FilterModelDTO()
            {
                OrganizationOptions = items.Select(x => x.Organization).Distinct().ToList(),
                POCNameOptions = items.Select(x => x.POCName).Distinct().ToList(),
                DateBeginOptions = items.Select(x => x.DateBegin).Distinct().ToList()
            };

            return filterModel;
        }

        public PageModelDTO GetPageModel()
        {
            var pageModel = new PageModelDTO()
            {
                ItemList = GetItems(),
            };

            pageModel.FilterModel = GetFilterOptionsForPageModel(pageModel.ItemList);

            return pageModel;
        }

        public PageModelDTO GetFilteredPageModel(string organization)
        {
            var pageModel = new PageModelDTO()
            {
                ItemList = GetFilteredItems(organization)
            };

            pageModel.FilterModel = GetFilterOptionsForPageModel(pageModel.ItemList);

            return pageModel;
        }

        private List<ItemModelDTO> GetFilteredItems(string organization)
        {
            var items = new List<ItemModelDTO>();

            using (var repo = new FakeRepository())
            {
                items = repo.GetFilteredItems(organization);
            }

            return items;
        }
    }
}
