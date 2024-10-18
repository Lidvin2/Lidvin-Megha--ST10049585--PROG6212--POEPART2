using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_POEPART2.Data;
using PROG6212_POEPART2.Models;

namespace PROG6212_POEPART2.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var lectures = await _context.Lecturers.ToListAsync();
            return View(lectures);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
               _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lecturer);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Lecturer lecturer)
        {
            if (id != lecturer.LecturerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }
    }
}
