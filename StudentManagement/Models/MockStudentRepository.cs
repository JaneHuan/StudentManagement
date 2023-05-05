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
                new Student(){Id=1,Name="张三",ClassName=ClassNameEnum.FirstGrade,Email="zhangsan@163.com"},
                new Student(){Id=2,Name="李四",ClassName=ClassNameEnum.SecondGrade,Email="lisi@163.com"},
                new Student(){Id=3,Name="王五",ClassName=ClassNameEnum.GradeThree,Email="王五@163.com"},
            };
        }

        public Student Add(Student student)
        {
            student.Id = _students.Max(s => s.Id)+1;
            _students.Add(student);
            return student;
        }

        public Student Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }
        public Student Update(Student uStudent)
        {
            throw new NotImplementedException();
        }
    }
}
