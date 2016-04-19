using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class 計算Action執行時間Attribute :  ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            //if (filterContext.HttpContext.Session["ActionCount"] == null)
            //    filterContext.HttpContext.Session["ActionCount"] = 0;
            //int count = (int)filterContext.HttpContext.Session["ActionCount"];
            //    count++;
            //filterContext.HttpContext.Session["ActionCount"] = count;

            filterContext.Controller.ViewBag.ActionStartTime = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            DateTime startTime = (DateTime)filterContext.Controller.ViewBag.ActionStartTime;
            DateTime endTime = DateTime.Now;
            TimeSpan executeTs = endTime - startTime;
            filterContext.Controller.ViewBag.ActionEndTime = endTime;
            filterContext.Controller.ViewBag.ActionExecutedTimeSpan = executeTs;

            base.OnActionExecuted(filterContext);
        }


    }
}