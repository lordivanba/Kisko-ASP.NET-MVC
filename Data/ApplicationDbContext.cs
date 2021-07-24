using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kisko.Entities;


namespace kisko.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
    }
}
