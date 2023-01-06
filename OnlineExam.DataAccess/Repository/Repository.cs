using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;

namespace OnlineExam.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //why used this here
        }

        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        public void AddRange(IList<T> Entities)
        {
            dbSet.AddRange(Entities);
        }

        public void Delete(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public void DeleteRange(T[] Entity)
        {
            dbSet.RemoveRange(Entity);
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> a = dbSet;
            if(includeProperties != null)
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    a = a.Include(property);
                }
            }
            return a.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filtercondition,string? includeProperties = null)
        {
            IQueryable<T> quary = dbSet;
            quary= quary.Where(filtercondition);
            if(includeProperties!=null)
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    quary = quary.Include(property);
                }
            }
            return quary.FirstOrDefault();
        }

        public bool GetFirstOrDefaultBool(Expression<Func<T, bool>> filtercondition)
        {
            IQueryable<T> found= dbSet;
            found = found.Where(filtercondition);
            if (found.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
