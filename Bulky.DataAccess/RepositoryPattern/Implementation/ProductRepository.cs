using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Implementation;
using Bookly.DataAccess.Repository.Interface;
using Bookly.DataAccess.RepositoryPattern.Interface;
using Bookly.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.RepositoryPattern.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly BooklyDbContext _bulkyDb;

        public ProductRepository(BooklyDbContext bulkyDb) : base(bulkyDb)
        {
            _bulkyDb = bulkyDb;
        }

        public void Update(Product obj)
        {
            // _bulkyDb.Update(obj);
            var objFromDb = _bulkyDb.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                obj.Author = obj.Author;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
