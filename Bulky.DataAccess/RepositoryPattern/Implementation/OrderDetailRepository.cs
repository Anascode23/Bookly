using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Implementation;
using Bookly.DataAccess.RepositoryPattern.Interface;
using Bookly.Models.Models;

namespace Bookly.DataAccess.RepositoryPattern.Implementation
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly BooklyDbContext _bulkyDb;
        public OrderDetailRepository(BooklyDbContext context) : base(context)
        {
            _bulkyDb = context;
        }

        public void Update(OrderDetail obj)
        {
            _bulkyDb.OrderDetails.Update(obj);
        }
    }
}
