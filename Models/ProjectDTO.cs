using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace kisko.Models
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GradeGroup { get; set; }
        public string Img { get; set; }
        public IFormFile Photo { get; set; }
        public string Img2 { get; set; }
        public IFormFile Photo2 { get; set; }
        public string Img3 { get; set; }
        public IFormFile Photo3 { get; set; }
        public string Video { get; set; }
        public int? StudentId { get; set; }
        public string Student { get; set; }
        public int? SecondStudentId { get; set; }
        public string SecondStudent { get; set; }

    }
}
