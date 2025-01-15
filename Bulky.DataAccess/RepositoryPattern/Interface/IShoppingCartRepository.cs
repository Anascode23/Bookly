using Bookly.DataAccess.Repository.Interface;
using Bookly.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.RepositoryPattern.Interface
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {

        void Update(ShoppingCart obj);
    }

}
