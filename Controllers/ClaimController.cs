using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_POEPART2.Data;
using PROG6212_POEPART2.Models;

namespace PROG6212_POEPART2.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }

        public IActionResult ApproveClaim() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(claim);
        }

        

        public async Task<IActionResult> RejectClaim(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Claim claim)
        {
            if (id != claim.ClaimID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile UploadFile)
        {
            if (UploadFile != null && UploadFile.Length > 0)
            {
                // Validate file type and size
                var permittedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var ext = Path.GetExtension(UploadFile.FileName).ToLowerInvariant();

                if (!permittedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("UploadedFile", "Invalid file type. Only PDF, DOCX, and XLSX are allowed.");
                    return View(claim); // Return the form with validation error
                }

                if (UploadFile.Length > 10 * 1024 * 1024) // Limit to 10MB
                {
                    ModelState.AddModelError("UploadedFile", "File size limit exceeded. Maximum allowed size is 10MB.");
                    return View(claim);
                }

                // Create a unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(UploadFile.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadFile.CopyToAsync(stream);
                }

                // Link the uploaded file to the claim
                claim.UploadFilePath = fileName;
            }

            // Save claim to the database
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction("ClaimSubmitted");
        }
    }
}

