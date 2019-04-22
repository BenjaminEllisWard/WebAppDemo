using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.BLL;
using Demo.DTOs;

namespace Demo.UI.Models
{
    public class PageModel
    {
        public PageModelDTO Model { get; set; }

        public PageModel()
        {
            var services = new Services();
            Model = services.GetPageModel();
        }
    }
}