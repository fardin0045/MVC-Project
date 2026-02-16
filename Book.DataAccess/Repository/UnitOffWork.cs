using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DataAccess.Repository
{
    public class UnitOffWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        public ICategoryRepository Category { get; private set; }
        public UnitOffWork(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
