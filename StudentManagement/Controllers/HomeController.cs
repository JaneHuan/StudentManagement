using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository _studentRepository;
        private HostingEnvironment _hostingEnvironment;

        public HomeController(IStudentRepository studentRepository, HostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var students = _studentRepository.GetAllStudents();
            return View(students);
        }
        public IActionResult Details(int? id)
        {
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                PageTitle = "学生详情信息",
                Student = _studentRepository.GetStudent(id ?? 1)
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    // wwwroot\images路径
                    string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    //生成图片唯一值
                    uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                    //新文件全路径
                    string newFileName = Path.Combine(uploadFile, uniqueFileName);
                    //将图片复制到新文件中
                    using (FileStream stream = new FileStream(newFileName, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                    }

                    Student newStudent = new Student
                    {
                        Name = model.Name,
                        Email = model.Email,
                        ClassName = model.ClassName,
                        PhotoPath = uniqueFileName
                    };
                    _studentRepository.Add(newStudent);
                    return RedirectToAction("Details", new { id = newStudent.Id });
                }

            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            var student = _studentRepository.GetStudent(id);
            StudentEditViewModel viewModel = new StudentEditViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                ClassName=student.ClassName,
                Email=student.Email,
                ExistPhotoPath=student.PhotoPath
            };

            return View(viewModel);
        }
    }
}
