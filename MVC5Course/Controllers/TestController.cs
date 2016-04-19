using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {

        FabricsEntities db = new FabricsEntities();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EDE()
        {
            return View();
        }

 
        [HttpPost]
        public ActionResult EDE(EDEViewModel data)
        {
            
            ViewData["ABC"] = DateTime.Now + " " + data.CompanyName;
           
            return View(data);
        }
     

        public ActionResult CreateProduct()
        {
           
            var product = new MVC5Course.Models.Product()
            {
                ProductName = "TestProduct",
                Active = true,
                Stock = 2,
                Price = 2010
            };

            db.Product.Add(product);
            db.SaveChanges();

            return View(product);
        }
        
        public ActionResult ReadProduct(bool ?active)
        {
            // var data = db.Product.Where(row => row.ProductId > 1550).OrderByDescending(row => row.Price);

            var data = db.Product.AsQueryable();
            data = data.Where(row => row.ProductId > 1550);
            if(active.HasValue)
            {
                data = data.Where(row => row.Active == active);
            }

            data = data.OrderByDescending(schema => schema.Price);

            return View(data);
        }
        
        public ActionResult OneProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }

        public ActionResult UpdateProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            if( data == null)
            {
                return HttpNotFound("Data not found, id=" + id); 
            }


            data.Price = data.Price * 2;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityError in ex.EntityValidationErrors)
                {
                    foreach (var validError in entityError.ValidationErrors)
                    {
                        return Content("Exception !:<BR>\r\n" + validError.PropertyName + ":" + validError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("ReadProduct");
        }

        public ActionResult DeleteProduct(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            if (data == null)
            {
                return HttpNotFound("Data not found, id=" + id);
            }

            //foreach (var item in data.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}

            db.OrderLine.RemoveRange(data.OrderLine);

            db.Product.Remove(data);
            db.SaveChanges();
            
            return RedirectToAction("ReadProduct");
        }

        
        public ActionResult ProductView(bool? active)
        {
            var data =  db.Database.SqlQuery<Product>(@"SELECT * FROM dbo.Product WHERE Active=@p0 ",active);

            return View(data);
        }

        
        public ActionResult ProductSP()
        {
            var data = db.GetProduct(true, "%Yellow%");
            return View(data);
        }


    }
}