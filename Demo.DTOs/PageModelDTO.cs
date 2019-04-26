using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    /// <summary>
    /// Data transfer object for individual item
    /// </summary>
    public class ItemModelDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Organization { get; set; }
        public string POCName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public DateTime DateEstablished { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public DateTime DateBegin { get; set; }
    }

    /// <summary>
    /// Data transfer object for the page model's filter model
    /// </summary>
    public class FilterModelDTO
    {
        public List<string> OrganizationOptions { get; set; }
        public List<string> POCNameOptions { get; set; }
        public List<DateTime> DateBeginOptions { get; set; }
    }

    /// <summary>
    /// DataTransferObject for the main page model
    /// </summary>
    public class PageModelDTO
    {
        public List<ItemModelDTO> ItemList { get; set; }
        public FilterModelDTO FilterModel { get; set; }
    }
}
