using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesStore.Data;
using MoviesStore.Models;

namespace MoviesStore.Controllers
{
    
    public class MoviesController : Controller
    {
        private const int pageSize = 3;   // количество элементов на странице
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MoviesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Movies/Index
        public IActionResult Index(int page = 1)
        {
            return View(new MoviesViewModel(_context.Movies, page, pageSize));
        }

        // GET: Movies/My
        [Authorize]
        public IActionResult My(int page = 1)
        {           
            return View(
                new MoviesViewModel(
                    _context.Movies.Where(movie => movie.UserId == _userManager.GetUserId(User)),
                    page,
                    pageSize)
                );
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/GetImage/5
        public async Task<IActionResult> GetImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.FindAsync<Movie>(id);
            if (movie?.Poster == null)
            {
                return NotFound();
            }

            return File(movie.Poster, "image/jpeg");
        }

        // GET: Movies/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new Movie() { ReliseYear = DateTime.Now.Year } /*Задаём значение по-умолчанию для поля*/);
        }

        // POST: Movies/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ReliseYear,Director,PosterImg")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.UserId = _userManager.GetUserId(User);
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(My));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ReliseYear,Director,PosterImg,Poster,UserId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (_userManager.GetUserId(User) != movie.UserId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(My));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (_userManager.GetUserId(User) != movie.UserId)
            {
                return Forbid();
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(My));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
