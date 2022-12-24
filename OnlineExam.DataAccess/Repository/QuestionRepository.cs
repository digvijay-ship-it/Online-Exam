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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private ApplicationDbContext _db;

        public QuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Question obj)
        {
            _db.Questions.Add(obj);
        }
    }
}
