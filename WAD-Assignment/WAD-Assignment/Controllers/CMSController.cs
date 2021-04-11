﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WAD_Assignment.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WAD_Assignment.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CMSController : Controller
    {
        private readonly ILogger<CMSController> _logger;

        private readonly ApplicationDbContext _context;

        public CMSController(ILogger<CMSController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Film> model = _context.Films.ToList();
            return View(model);
        }

        // Call to a View
        [HttpGet]
        public IActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFilm(FilmForm model)
        {

            if (ModelState.IsValid)
            {
                Film newFilm = new Film
                {
                    FilmTitle = model.FilmTitle,
                    FilmCertificate = model.FilmCertificate,
                    FilmDescription = model.FilmDescription,
                    FilmImage = model.FilmImage,
                    FilmPrice = model.FilmPrice,
                    RentPrice = model.RentPrice,
                    Stars = model.Stars,
                    ReleaseDate = DateTime.Now,
                };
                _context.Add(newFilm);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View();


            }




        }


        //action to update the film.
        [HttpGet]
        public IActionResult UpdateFilm(int id)
        {
            Film model = _context.Films.Find(id);
            FilmForm formModel = new FilmForm
            {
                FilmID = model.FilmID,
                FilmTitle = model.FilmTitle,
                FilmCertificate = model.FilmCertificate,
                FilmDescription = model.FilmDescription,
                FilmImage = model.FilmImage,
                FilmPrice = model.FilmPrice,
                RentPrice = model.RentPrice,
                Stars = model.Stars,
                ReleaseDate = model.ReleaseDate,
            };
            ViewBag.ImageName = model.FilmImage;
            return View(formModel);
        }

        [HttpPost]
        public IActionResult UpdateFilm(FilmForm model)
        {
            if (ModelState.IsValid)
            {
                Film editFilm = new Film
                {
                    FilmID = model.FilmID,
                    FilmTitle = model.FilmTitle,
                    FilmCertificate = model.FilmCertificate,
                    FilmDescription = model.FilmDescription,
                    FilmImage = model.FilmImage,
                    FilmPrice = model.FilmPrice,
                    RentPrice = model.RentPrice,
                    Stars = model.Stars,
                    ReleaseDate = model.ReleaseDate
                };
                _context.Films.Update(editFilm);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //Delete Film
        [HttpGet]
        public IActionResult DeleteFilm(int Id)
        {
            Film model = _context.Films.Find(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteFilm(Film model)
        {
            _context.Films.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
