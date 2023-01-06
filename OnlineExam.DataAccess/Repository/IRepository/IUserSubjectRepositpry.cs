using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.Models;

namespace OnlineExam.DataAccess.Repository.IRepository
{
    public interface IUserSubjectRepositpry : IRepository<UserSubject>
    {
		void Update(UserSubject obj);
	}
}
