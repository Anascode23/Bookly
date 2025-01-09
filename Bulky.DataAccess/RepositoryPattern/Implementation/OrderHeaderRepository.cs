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

        public void UpdateStatus(int id, string orderStatus, string? paymentstatus = null)
        {
            var orderFromDb = _bulkyDb.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentstatus))
                {
                    orderFromDb.PaymentStatus = paymentstatus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionid, string paymentIntentId)
        {
            var orderFromDb = _bulkyDb.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionid))
            {
                orderFromDb.SessionId = sessionid;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
