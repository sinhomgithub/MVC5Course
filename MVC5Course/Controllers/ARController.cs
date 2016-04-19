using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    { 
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return this.PartialView("Index");
        }

        public ActionResult FileTest(int? dl)
        {
            FileResult r = null;
            if (dl.HasValue && dl == 1)
                r = this.File(Server.MapPath("~/Content/ok.png"), "image/png", "ok.png");
            else
                r = this.File(Server.MapPath("~/Content/ok.png"), "image/png");
            return r;
        }

        public ActionResult JsonTest(int id)
        {
            repoProduct.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;

            //repoProduct.UnitOfWork.LazyLoadingEnabled = false;

            Product p = repoProduct.Find(id);

            return this.Json(p, JsonRequestBehavior.AllowGet);

        }

    }
}