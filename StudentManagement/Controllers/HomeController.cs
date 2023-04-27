using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            var students = _studentRepository.GetAllStudents();
            return View(students);
        }
        public IActionResult Details(int id)
        {
            //Student model = _studentRepository.GetStudent(1);
            //ViewData["PageTitle"] = "学生详情页面";
            //ViewData["Student"] = model;
            //ViewBag.PageTitle= "学生详情页面";
            //ViewBag.Student =model;
            //return View(model);
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel() 
            { 
                PageTitle="学生详情信息",
                Student = _studentRepository.GetStudent(id)
            };
            return View(viewModel);
        }
    }
}
