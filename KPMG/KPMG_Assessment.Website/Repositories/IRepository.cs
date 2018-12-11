using KPMG_Assessment.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KPMG_Assessment.Website.Repositories
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> FindAll();
        IQueryable<T> GetAllWithPaging(int page, int pageSize, out int totalRecords);
        Task Create(T entity);
        Task Create(IList<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
   

}
