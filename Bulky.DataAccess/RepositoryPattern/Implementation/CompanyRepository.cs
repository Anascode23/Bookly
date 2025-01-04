using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Implementation;
using Bookly.DataAccess.RepositoryPattern.Interface;
using Bookly.Models;

namespace Bookly.DataAccess.RepositoryPattern.Implementation
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly BooklyDbContext _bulkyDb;
        public CompanyRepository(BooklyDbContext context) : base(context)
        {
            _bulkyDb = context;
        }

        public void Update(Company obj)
        {
            _bulkyDb.Companies.Update(obj);
        }
    }
}
