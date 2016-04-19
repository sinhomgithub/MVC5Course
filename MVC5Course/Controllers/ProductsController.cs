using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{

    [計算Action執行時間]
    public class ProductsController : BaseController
    {
       

       
       
        // GET: Products
        public ActionResult Index()
        {
            var data = repoProduct.All().Take(5);
            return View(data);
        }


        [HttpPost]
        public ActionResult Index(IList<BatchUpdateProduct> data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var row = repoProduct.Find(item.ProductId);
                    row.Price = item.Price;
                    row.Active = item.Active;
                    row.Stock = item.Stock;
                }

                repoProduct.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            
            ViewData.Model = repoProduct.All().Take(5);

            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = repoProduct.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult OrderLines(int productId)
        {
            var data = repoProduct.Find(productId);
            if (data == null)
                return HttpNotFound();
            return PartialView(data.OrderLine);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = repoProduct.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection unusedForm)
        {
            var product = repoProduct.Find(id);  // get product row from EntityFramework

            // fill form data to EF row
            if (this.TryUpdateModel(product, new string[] { "ProductId", "ProductName", "Price", "Stock" }))
            {
                //var dbProduct = (FabricsEntities)repoProduct.UnitOfWork.Context;
                //dbProduct.Entry(product).State = EntityState.Modified;

                repoProduct.UnitOfWork.Commit();

                TempData["UpdateMessage"] = product.ProductName + " 更新成功 !";

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var product = repoProduct.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repoProduct.Find(id);

            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

   



    }
}
