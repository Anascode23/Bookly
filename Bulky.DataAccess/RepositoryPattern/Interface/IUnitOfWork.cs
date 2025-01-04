using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Interface;
using Bookly.DataAccess.RepositoryPattern.Interface;

namespace Bookly.DataAccess.Repository.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        void Save();
    }
}
