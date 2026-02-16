using Book.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category > 
    {
        void Update(Category obj);
        
    }
}
