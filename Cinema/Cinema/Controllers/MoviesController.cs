using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaContext _context;

        public MoviesController(CinemaContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.Movie.Include(m => m.Category).Include(m => m.Director);
            return View(await cinemaContext.ToListAsync());
        }

        [Authorize]
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Category)
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize]
        // GET: Movies/Create
        public IActionResult Create()
        {
            var directorsSet = from x in _context.Director
                               select new
                               {
                                   x.Id,
                                   x.Name,
                                   x.Surname,
                                   DisplayField = String.Format("{0} {1}", x.Name, x.Surname)
                               };
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["DirectorId"] = new SelectList(directorsSet, "Id", "DisplayField");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Duration,DirectorId,CategoryId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var directorsSet = from x in _context.Director
                               select new
                               {
                                   x.Id,
                                   x.Name,
                                   x.Surname,
                                   DisplayField = String.Format("{0} {1}", x.Name, x.Surname)
                               };

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", movie.CategoryId);
            ViewData["DirectorId"] = new SelectList(directorsSet, "Id", "DisplayField", movie.DirectorId);
            return View(movie);
        }

        [Authorize]
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var directorsSet = from x in _context.Director
                               select new
                               {
                                   x.Id,
                                   x.Name,
                                   x.Surname,
                                   DisplayField = String.Format("{0} {1}", x.Name, x.Surname)
                               };

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", movie.CategoryId);
            ViewData["DirectorId"] = new SelectList(directorsSet, "Id", "DisplayField", movie.DirectorId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Duration,DirectorId,CategoryId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
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
                return RedirectToAction(nameof(Index));
            }

            var directorsSet = from x in _context.Director
                               select new
                               {
                                   x.Id,
                                   x.Name,
                                   x.Surname,
                                   DisplayField = String.Format("{0} {1}", x.Name, x.Surname)
                               };

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", movie.CategoryId);
            ViewData["DirectorId"] = new SelectList(directorsSet, "Id", "DisplayField", movie.DirectorId);
            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Category)
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Top3Movies()
        {
            var top3Movies = _context.Showing
            .GroupBy(s => s.MovieId)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key)
            .ToList();

            var result = _context.Movie
                .Where(m => top3Movies.Contains(m.Id))
                .ToList();

            return View(result);
        }

    }
}
