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
        void AddRange(T[] Entities);
        IEnumerable<T> GetAll();
        T GetFirstOrDefault(Expression<Func<T,bool>> filtercondition);
        bool GetFirstOrDefaultBool(Expression<Func<T, bool>> filtercondition);
        void Delete(T Entity);
    }
}
