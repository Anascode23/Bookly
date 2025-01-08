using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Implementation;
using Bookly.DataAccess.RepositoryPattern.Interface;
using Bookly.Models;

namespace Bookly.DataAccess.RepositoryPattern.Implementation
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly BooklyDbContext _bulkyDb;
        public OrderHeaderRepository(BooklyDbContext context) : base(context)
        {
            _bulkyDb = context;
        }

        public void Update(OrderHeader obj)
        {
            _bulkyDb.OrderHeaders.Update(obj);
        }
    }
}
