using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAggregator.Database;
using DataAggregator.Entities;

namespace DataAggregator.Controllers
{
    public class FiltersController : Controller
    {
        private readonly AppDbContext _context;

        public FiltersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Filters.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters.FirstOrDefaultAsync(m => m.Id == id);

            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Value")] FilterEntity filter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filter);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(filter);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters.FindAsync(id);

            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Value")] FilterEntity filter)
        {
            if (id != filter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filter);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceEntityExists(filter.Id))
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

            return View(filter);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters.FirstOrDefaultAsync(m => m.Id == id);

            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filter = await _context.Filters.FindAsync(id);

            _context.Filters.Remove(filter);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SourceEntityExists(int id)
        {
            return _context.Filters.Any(e => e.Id == id);
        }
    }
}