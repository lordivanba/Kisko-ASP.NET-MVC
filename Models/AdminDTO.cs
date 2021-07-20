using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kisko.Models
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
