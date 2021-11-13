﻿using kisko.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kisko.Models;
using kisko.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace kisko.Controllers
{
    public class GestionAlumnosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public int Id = 0;
        public GestionAlumnosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            var students = _dbContext.Students
                .Select(student => new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Lastname = student.Lastname,
                    Email = student.Email
                }).ToList();
            ViewBag.AdminId = Id;
            return View(students);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(StudentDTO dto)
        {
            var result = AddAlumno(dto);
            if(result == false)
                return BadRequest();
            else
                return RedirectToAction("Index", new { Id = 1 });
        }

        public IActionResult Delete(int Id)
        {
            var student = new Student()
            {
                Id = Id
            };

            _dbContext.Students.Attach(student);
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { Id = 1 });
        }

        public IActionResult Edit(int Id)
        {
            var students = _dbContext.Students
                .Select(student => new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Lastname = student.Lastname,
                    Email = student.Email
                }).ToList();
            var student = students.Where(s => s.Id == Id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentDTO dto)
        {

            var student = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = dto.Name;
            student.Lastname = dto.Lastname;
            student.Email = dto.Email;

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { Id = 1 });
        }

        public bool AddAlumno(StudentDTO dto) {
            bool isSaved;

            //Si los campos vienen vacios, retorna false y no se registra nada en la bd
            if (dto.Name == null && dto.Lastname == null && dto.Email == null)
                return isSaved = false;

            //Si existen los datos se crea un nuevo alumno
            var student = new Student
            {
                Name = dto.Name,
                Lastname = dto.Lastname,
                Email = dto.Email
            };

            //Realizamos el registro del nuevo alumno en la bd y regresamos true en caso de que no exista ninguna excepcion
            try
            {
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return isSaved = true;
            }
            catch (Exception e) //En caso de haber una excepcion regresamos false;
            {
                Console.WriteLine(e);
                return isSaved = false;
            }
        }
    }
}
