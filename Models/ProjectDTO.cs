using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kisko.Models
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GradeGroup { get; set; }

        public string Img { get; set; }
    }
}
