using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLStudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Student Add(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChangesAsync();
            return student;
        }

        public Student Delete(int id)
        {
            var student=_dbContext.Students.Find(id);
            if (student != null)
                _dbContext.Students.Remove(student);
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _dbContext.Students;
        }

        public Student GetStudent(int id)
        {
            return _dbContext.Students.Find(id);
        }

        public Student Update(Student uStudent)
        {
            var stu = _dbContext.Students.Attach(uStudent); ;
            stu.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return uStudent;
        }
    }
}
