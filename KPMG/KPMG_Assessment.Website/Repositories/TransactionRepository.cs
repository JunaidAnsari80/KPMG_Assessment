using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KPMG_Assessment.Website.Data;
using KPMG_Assessment.Website.Models;
using Microsoft.EntityFrameworkCore;

namespace KPMG_Assessment.Website.Repositories
{
    public class TransactionRepository<T> : IRepository<T> where T : AccountTransaction
    {
        protected TransactionsContext _context { get; set; }

        public TransactionRepository(TransactionsContext context)
        {
            this._context = context;
        }

        public async Task<IQueryable<T>> FindAll()
        {
            return  this._context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await this._context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        public async Task Create(IList<T> entities)
        {
            await this._context.Set<T>().AddRangeAsync(entities);
        }

        public IQueryable<T> GetAllWithPaging(int page, int pageSize, out int totalRecords)
        {
            var source = _context.Set<T>().AsNoTracking<T>();

            totalRecords = source.Count();

            int skipRows = (page - 1) * pageSize;

            return source.Skip(skipRows).Take(pageSize);           
        }
    }
}
