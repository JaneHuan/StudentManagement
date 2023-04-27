using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _students;
        public MockStudentRepository()
        {
            _students = new List<Student>() { 
                new Student(){Id=1,Name="刘玄德",ClassName="三年级",Email="liubei@163.com"},
                new Student(){Id=2,Name="关云长",ClassName="二年级",Email="guanyu@163.com"},
                new Student(){Id=3,Name="张翼德",ClassName="一年级",Email="张飞@163.com"},
            };
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }
    }
}
