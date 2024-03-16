using Bookly.DataAccess.Repository.Interface;
using Bookly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.RepositoryPattern.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
