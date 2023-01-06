using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace OnlineExam.DataAccess.Repository
{
    public class ResultRepositpry : Repository<Result>, IResultRepositpry
	{
        private ApplicationDbContext _db;
        public ResultRepositpry(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Result obj)
        {
            _db.Results.Update(obj);
        }
    }
}
