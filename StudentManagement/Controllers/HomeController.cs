using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _hostingEnvironment;

        public HomeController(IStudentRepository studentRepository, IWebHostEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var students = _studentRepository.GetAllStudents();
            return View(students);
        }
        public ViewResult Details(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if (student == null)
                throw new Exception($"没有查询到Id为{id}的学生信息");
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                PageTitle = "学生详情信息",
                Student = student
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
                //string uniqueFileName = null;
                if (model.Photo != null)
                {
                    //// wwwroot\images路径
                    //string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    ////生成图片唯一值
                    //uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                    ////新文件全路径
                    //string newFileName = Path.Combine(uploadFile, uniqueFileName);
                    ////将图片复制到新文件中
                    //using (FileStream stream = new FileStream(newFileName, FileMode.Create))
                    //{
                    //    model.Photo.CopyTo(stream);
                    //}

                    Student newStudent = new Student
                    {
                        Name = model.Name,
                        Email = model.Email,
                        ClassName = model.ClassName,
                        PhotoPath = SavePhoto(model.Photo)
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
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _studentRepository.GetStudent(model.Id);
                if (model.Photo != null&& student.PhotoPath!=null)
                {
                    student.Name = model.Name;
                    student.Email = model.Email;
                    student.ClassName = model.ClassName;
                    student.PhotoPath = SavePhoto(model.Photo);
                }
                string oldPhoto = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistPhotoPath);
                if (System.IO.File.Exists(oldPhoto))
                    System.IO.File.Delete(oldPhoto);
                _studentRepository.Update(student);
                return RedirectToAction("index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var student = _studentRepository.Delete(id);
            return RedirectToAction("index");
        }

        private string SavePhoto(Microsoft.AspNetCore.Http.IFormFile file)
        {
            // wwwroot\images路径
            string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            //生成图片唯一值
            string uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            //新文件全路径
            string newFileName = Path.Combine(uploadFile, uniqueFileName);
            //将图片复制到新文件中
            using (FileStream stream = new FileStream(newFileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return uniqueFileName;
        }
    }
}
