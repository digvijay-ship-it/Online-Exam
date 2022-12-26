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
    public class AdminRepositpry : Repository<Admin>, IAdminRepositpry
	{
        private ApplicationDbContext _db;
        public AdminRepositpry(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
