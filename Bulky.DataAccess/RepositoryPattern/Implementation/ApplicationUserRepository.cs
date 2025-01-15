using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Implementation;
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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly BooklyDbContext _bulkyDb;

        public ApplicationUserRepository(BooklyDbContext bulkyDb) : base(bulkyDb)
        {
            _bulkyDb = bulkyDb;
        }
    }
}
