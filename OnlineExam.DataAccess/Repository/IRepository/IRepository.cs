using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T Entity);
        void AddRange(IList<T> Entities);
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filtercondition, string? includeProperties = null);
        bool GetFirstOrDefaultBool(Expression<Func<T, bool>> filtercondition);
        void Delete(T Entity);
    }
}