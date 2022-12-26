using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ISubjectRepositpry SubRepo { get; }
        public IQuestionRepository QuesRepo { get; }
        public IOptionRepository OptionRepo { get; }
        public IUserRepositpry UserRepo { get; }
        public IAdminRepositpry AdminRepo { get; }
        void Save();
    }
}
