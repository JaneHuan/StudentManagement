using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 根据Id查询学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);
        /// <summary>
        /// 获取所有学生
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudents();
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);
        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="uStudent"></param>
        /// <returns></returns>
        Student Update(Student uStudent);
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="dStudent"></param>
        /// <returns></returns>
        Student Delete(int id);
    }
}
