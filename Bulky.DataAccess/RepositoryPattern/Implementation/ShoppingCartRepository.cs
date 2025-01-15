using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Implementation;
using Bookly.DataAccess.RepositoryPattern.Interface;
using Bookly.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.RepositoryPattern.Implementation
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly BooklyDbContext _bulkyDb;
        public ShoppingCartRepository(BooklyDbContext context) : base(context)
        {
            _bulkyDb = context;
        }

        public void Update(ShoppingCart obj)
        {
            _bulkyDb.ShoppingCarts.Update(obj);
        }
    }

}
