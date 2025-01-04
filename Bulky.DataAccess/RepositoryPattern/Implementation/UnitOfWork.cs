using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Interface;
using Bookly.DataAccess.RepositoryPattern.Implementation;
using Bookly.DataAccess.RepositoryPattern.Interface;
using Bookly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooklyDbContext _bulkyDb;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public UnitOfWork(BooklyDbContext bulkyDb)
        {
            _bulkyDb = bulkyDb;
            Category = new CategoryRepository(_bulkyDb);
            Product = new ProductRepository(_bulkyDb);
            Company = new CompanyRepository(_bulkyDb);
            ShoppingCart = new ShoppingCartRepository(_bulkyDb);
        }


        public void Save()
        {
            _bulkyDb.SaveChanges();
        }
    }
}
