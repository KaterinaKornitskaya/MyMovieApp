using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMovieApp.Models;

namespace MyMovieApp.Controllers
{
    public class FilmsController : Controller
    {
        private readonly FilmContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public FilmsController(FilmContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            return View(await _context.Films.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("ID,Name,Director,Genre,Year,Actors,Description,Image")] Film film, 
            IFormFile uploadedFile)
        {
           
            if (FilmExists3(film.Name, film.Director))
                ModelState.AddModelError("", "Комбинация такого названия и режиссера уже есть!" +
                    " Введите другое название, или другого режиссера.");

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/img/" + uploadedFile.FileName;
                    using (FileStream? fs = new FileStream
                        (_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fs);
                        film.Image = path;
                    }                  
                }
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(film);
        }


        //[AcceptVerbs("Get", "Post")]
        //public IActionResult CheckName(string name)
        //{
        //    if (FilmExists2(name))
        //        return Json(false);
        //    return Json(true);
        //}

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("ID,Name,Director,Genre,Year,Actors,Description,Image")] Film film,
            IFormFile? uploadedFile)
        {
            if (id != film.ID)
            {
                return NotFound();
            }

            if (FilmExists3(film.Name, film.Director))
                ModelState.AddModelError("", "Комбинация такого названия и режиссера уже есть!" +
                    " Введите другое название, или другого режиссера.");

            if (ModelState.IsValid)
            {

                if (uploadedFile != null)
                {
                    string path = "/img/" + uploadedFile.FileName;
                    using (FileStream? fs = new FileStream
                        (_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fs);
                        film.Image = path;
                    }
                }
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.ID == id);
        }

        // проверка, что существует фильм с такой комбинацией имени и режиссера
        private bool FilmExists3(string name, string direct)
        {
            return _context.Films.Any(e => e.Name == name && e.Director == direct);
        }
    }
}
