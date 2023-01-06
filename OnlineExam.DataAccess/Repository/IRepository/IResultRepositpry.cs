using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.Models;

namespace OnlineExam.DataAccess.Repository.IRepository
{
    public interface IResultRepositpry : IRepository<Result>
    {
        void Update(Result obj);
    }
}
