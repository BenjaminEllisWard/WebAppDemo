using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.UI.Models;
using Demo.BLL;
using static Demo.GeneralFunctions.ValidationHelper;

namespace Demo.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DemoPage(string filter = null)
        {
            var pageModel = new PageModel();
            return View(pageModel);
        }

        [HttpGet]
        public ActionResult GetRecordsByOrg(string organization)
        {
            var service = new Services();
            var model = new PageModel();

            if (ValidateOrganization(organization))
            {
                model.Model = service.GetFilteredPageModel(organization);
            }

            return PartialView("DemoPagePartial", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}