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
    public class SubjectRepositpry : Repository<Subject>, ISubjectRepositpry
    {
        private ApplicationDbContext _db;
        public SubjectRepositpry(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Subject obj)
        {
            _db.Subjects.Update(obj);
        }
    }
}
