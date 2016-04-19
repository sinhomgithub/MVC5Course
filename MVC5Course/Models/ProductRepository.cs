using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public Product Find(int id)
        {
            var data = this.All().Where(p => p.ProductId == id).FirstOrDefault();

            return data;
        }

        public IQueryable<Product> All(bool isTrueAll)
        {
            if (isTrueAll)
                return base.All();
            else
                return this.All();
        }

        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }

        public override void Delete(Product entity)
        {
            entity.IsDeleted = false;
        }

    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}