using kisko.Controllers;
using kisko.Data;
using kisko.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        // CP001 Agregar alumno con los campos vacios. -- Isai
        public void AddStudent_ShouldReturnFalse()
        {
            //Arrange
            var student = new StudentDTO
            {
                Name = null,
                Lastname = null,
                Email = null
            };
            var expected = false;

            //Act
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Data Source=DESKTOP-SHTCNF0\\SQLEXPRESS; Initial Catalog=Kisko; Persist Security Info=False; User ID=sa; Password=12345").Options;
            var dbContext = new ApplicationDbContext(options);


            var controller = new GestionAlumnosController(dbContext);
            var actual = controller.AddAlumno(student);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // CP002 Agregar alumno llenando todos los campos. *Todos los campos son requeridos* -- Ivan
        public void AddStudent_ShouldReturnTrue() {
            //Arrange
            var student = new StudentDTO {
                Name = "Sebastian",
                Lastname = "Rulli",
                Email = "rulli@gmail.com"
            };
            var expected = true;

            //Act
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Data Source=DESKTOP-SHTCNF0\\SQLEXPRESS; Initial Catalog=Kisko; Persist Security Info=False; User ID=sa; Password=12345").Options;
            var dbContext = new ApplicationDbContext(options);

            var controller = new GestionAlumnosController(dbContext);
            var actual = controller.AddAlumno(student);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // CP008 Iniciar sesión llenando todos los campos y usando una contraseña correcta. -- Isai
        public void LoginAsAdmin_ShouldReturnTrue() 
        {
            //Arrange
            var admin = new AdminDTO
            {
                Email = "ivanba@gmail.com",
                Password = "12345"
            };
            var expected = true;

            //Act
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Data Source=DESKTOP-SHTCNF0\\SQLEXPRESS; Initial Catalog=Kisko; Persist Security Info=False; User ID=sa; Password=12345").Options;
            var dbContext = new ApplicationDbContext(options);

            var controller = new LoginController(dbContext);
            var actual = controller.LoginAsAdmin(admin);

            //Assert
            Assert.AreEqual(expected , actual);
        }

        [TestMethod]
        // CP009 Iniciar sesión dejando todos los campos vacios -- Ivan
        public void LoginAsAdmin_ShouldReturnFalse_EmptyFields()
        {
            //Arrange
            var admin = new AdminDTO
            {
                Email = null,
                Password = null
            };
            var expected = false;

            //Act
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Data Source=DESKTOP-SHTCNF0\\SQLEXPRESS; Initial Catalog=Kisko; Persist Security Info=False; User ID=sa; Password=12345").Options;
            var dbContext = new ApplicationDbContext(options);

            var controller = new LoginController(dbContext);
            var actual = controller.LoginAsAdmin(admin);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // CP010 Iniciar sesión llenando todos los campos y usando una contraseña incorrecta. -- Juan Carlos
        public void LoginAsAdmin_ShouldReturnFalse()
        {
            //Arrange
            var admin = new AdminDTO
            {
                Email = "ivanba@gmail.com",
                Password = "chayanne"
            };
            var expected = false;

            //Act
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Data Source=DESKTOP-SHTCNF0\\SQLEXPRESS; Initial Catalog=Kisko; Persist Security Info=False; User ID=sa; Password=12345").Options;
            var dbContext = new ApplicationDbContext(options);

            var controller = new LoginController(dbContext);
            var actual = controller.LoginAsAdmin(admin);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
