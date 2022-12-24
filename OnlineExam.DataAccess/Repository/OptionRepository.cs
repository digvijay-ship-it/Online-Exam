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
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        private ApplicationDbContext _db;

        public OptionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Option obj)
        {
            _db.Update(obj);
        }
    }
}
