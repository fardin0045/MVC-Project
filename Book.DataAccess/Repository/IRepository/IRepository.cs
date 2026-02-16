using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Book.DataAccess.Repository.IRepository
{
    internal interface IRepository<T> where T : class
    {
        // T - Category  or any other generic model we want to
        //perform CRUD operations 

        //Suppose T - Category
        IEnumerable<T> GetAll();

        //Link operator u=> u.id ==id
        T Get(Expression<Func<T, bool>> filter);

        //Add Method 
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange (IEnumerable<T> entity);

    }
}
