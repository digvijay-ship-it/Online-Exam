using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;

namespace OnlineExam.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            SubRepo =new SubjectRepositpry(_db);
            QuesRepo = new QuestionRepository(_db);
            OptionRepo = new OptionRepository(_db);
            UserRepo = new UserRepositpry(_db);
            AdminRepo= new AdminRepositpry(_db);
        }

        public ISubjectRepositpry SubRepo { get;private set; }

        public IQuestionRepository QuesRepo { get;private set; }

        public IOptionRepository OptionRepo { get;private set; }

        public IUserRepositpry UserRepo { get;private set; }

        public IAdminRepositpry AdminRepo { get;private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
