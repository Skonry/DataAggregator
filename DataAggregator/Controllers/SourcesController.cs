using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAggregator.Database;
using DataAggregator.Entities;

namespace DataAggregator.Controllers
{
    public class SourcesController : Controller
    {
        private readonly AppDbContext _context;

        public SourcesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sources.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Sources.FirstOrDefaultAsync(m => m.Id == id);

            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url")] SourceEntity source)
        {
            if (ModelState.IsValid)
            {
                _context.Add(source);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(source);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourceEntity = await _context.Sources.FindAsync(id);

            if (sourceEntity == null)
            {
                return NotFound();
            }

            return View(sourceEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url")] SourceEntity source)
        {
            if (id != source.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(source);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceEntityExists(source.Id))
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

            return View(source);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Sources.FirstOrDefaultAsync(m => m.Id == id);

            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var source = await _context.Sources.FindAsync(id);

            _context.Sources.Remove(source);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SourceEntityExists(int id)
        {
            return _context.Sources.Any(e => e.Id == id);
        }
    }
}
