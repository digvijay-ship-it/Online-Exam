﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace OnlineExam.DataAccess.Repository
{
    public class UserSubjectRepositpry : Repository<UserSubject>, IUserSubjectRepositpry
	{
        private ApplicationDbContext _db;
        public UserSubjectRepositpry(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
		public void Update(UserSubject obj)
		{
			_db.UserSubjects.Update(obj);
		}
	}
}