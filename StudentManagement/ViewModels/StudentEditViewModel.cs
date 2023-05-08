using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels
{
    public class StudentEditViewModel:StudentViewModel
    {
        public int Id { get; set; }
        public string ExistPhotoPath { get; set; }
    }
}
