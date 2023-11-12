using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConsoleApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class VigeneresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VigeneresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vigeneres
        public async Task<IActionResult> Index()
        {
            var res = await _context
                .Vigeneres
                .Where(v => v.AppUserId == GetUserId())
                .Select(v => new VigenereIndexViewModel()
                {
                    Id = v.Id,
                    Plaintext = v.Plaintext,
                    CypherKey = v.CypherKey,
                    Cyphertext = v.Cyphertext,
                })
                .ToListAsync();
            return View(res);
        }

        // GET: Vigeneres/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Vigeneres == null)
            {
                return NotFound();
            }

            var vigenere = await _context.Vigeneres
                .Include(v => v.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vigenere == null)
            {
                return NotFound();
            }

            return View(vigenere);
        }

        // GET: Vigeneres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vigeneres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VigenereCreateViewModel vigenereVM)
        {
            if (!IsValidPlaintext(vigenereVM.Plaintext))
            {
                ModelState.AddModelError("Plaintext", "Invalid Plaintext");
            }

            if (!IsValidCypherKey(vigenereVM.CypherKey))
            {
                ModelState.AddModelError("CypherKey", "Invalid CypherKey");
            }
            
            if (ModelState.IsValid)
            {
                var vigenere = new Vigenere()
                {
                    Plaintext = vigenereVM.Plaintext,
                    CypherKey = vigenereVM.CypherKey,
                    Cyphertext = Vigener.DoVigenerEncrypt(vigenereVM.Plaintext, vigenereVM.CypherKey),
                    AppUserId = GetUserId()
                };
                _context.Add(vigenere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vigenereVM);
        }
        
        public string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: Vigeneres/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vigeneres == null)
            {
                return NotFound();
            }

            var vigenere = await _context.Vigeneres.FindAsync(id);
            if (vigenere == null)
            {
                return NotFound();
            }
            return View(vigenere);
        }

        // POST: Vigeneres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Vigenere vigenere)
        {
            if (id != vigenere.Id)
            {
                return NotFound();
            }

            if (!IsValidPlaintext(vigenere.Plaintext))
            {
                ModelState.AddModelError("Plaintext", "Invalid Plaintext");
            }

            if (!IsValidCypherKey(vigenere.CypherKey))
            {
                ModelState.AddModelError("CypherKey", "Invalid CypherKey");
            }

            
            if (ModelState.IsValid)
            {
                try
                {
                    vigenere.AppUserId = GetUserId();
                    vigenere.Cyphertext = Vigener.DoVigenerEncrypt(vigenere.Plaintext, vigenere.CypherKey);
                    _context.Update(vigenere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VigenereExists(vigenere.Id))
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
            return View(vigenere);
        }

        // GET: Vigeneres/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vigeneres == null)
            {
                return NotFound();
            }

            var vigenere = await _context.Vigeneres
                .Include(v => v.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vigenere == null)
            {
                return NotFound();
            }

            return View(vigenere);
        }

        // POST: Vigeneres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vigeneres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vigeneres'  is null.");
            }
            var vigenere = await _context.Vigeneres.FindAsync(id);
            if (vigenere != null)
            {
                _context.Vigeneres.Remove(vigenere);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VigenereExists(Guid id)
        {
          return (_context.Vigeneres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        private bool IsValidPlaintext(string plaintext)
        {
            return !string.IsNullOrEmpty(plaintext) && plaintext.Length <= 255; 
        }

        private bool IsValidCypherKey(string cypherKey)
        {
            return !string.IsNullOrEmpty(cypherKey) && cypherKey.Length <= 255; 
        }
    }
}
