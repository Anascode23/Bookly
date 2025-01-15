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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BooklyDbContext _bulkyDb;

        public CategoryRepository(BooklyDbContext bulkyDb) : base(bulkyDb)
        {
            _bulkyDb = bulkyDb;
        }

        public void Update(Category obj)
        {
            _bulkyDb.Categories.Update(obj);
        }
    }
}
