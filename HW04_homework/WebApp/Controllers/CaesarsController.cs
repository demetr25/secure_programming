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
    public class CaesarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaesarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Caesars
        public async Task<IActionResult> Index()
        {
            var res = await _context
                .Caesars
                .Where(c=> c.AppUserId == GetUserId())
                .Select(c=>new CaesarIndexViewModel()
                {
                    Id = c.Id,
                    Plaintext = c.Plaintext,
                    CypherKey = c.CypherKey,
                    Cyphertext = c.Cyphertext,
                })
                .ToListAsync();
            return View(res);
        }

        // GET: Caesars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Caesars == null)
            {
                return NotFound();
            }

            var caesar = await _context.Caesars
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caesar == null)
            {
                return NotFound();
            }

            return View(caesar);
        }

        // GET: Caesars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caesars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaesarCreateViewModel caesarVM)
        {
            if (!IsValidPlaintext(caesarVM.Plaintext))
            {
                ModelState.AddModelError("Plaintext", "Invalid Plaintext");
            }

            if (!IsValidCypherKey(caesarVM.CypherKey))
            {
                ModelState.AddModelError("CypherKey", "Invalid CypherKey");
            }
            
            if (ModelState.IsValid)
            { 
                var caesar = new Caesar()
                {
                    Plaintext = caesarVM.Plaintext,
                    CypherKey = caesarVM.CypherKey,
                    Cyphertext = Cesar.DoCesarEncrypt(caesarVM.Plaintext, caesarVM.CypherKey),
                    AppUserId = GetUserId()
                };
                _context.Add(caesar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caesarVM);
        }

        public string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: Caesars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Caesars == null)
            {
                return NotFound();
            }

            var caesar = await _context.Caesars.FindAsync(id);
            if (caesar == null)
            {
                return NotFound();
            }
            return View(caesar);
        }

        // POST: Caesars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Caesar caesar)
        {
            if (id != caesar.Id)
            {
                return NotFound();
            }
            
            if (!IsValidPlaintext(caesar.Plaintext))
            {
                ModelState.AddModelError("Plaintext", "Invalid Plaintext");
            }

            if (!IsValidCypherKey(caesar.CypherKey))
            {
                ModelState.AddModelError("CypherKey", "Invalid CypherKey");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    caesar.AppUserId = GetUserId();
                    caesar.Cyphertext = Cesar.DoCesarEncrypt(caesar.Plaintext, caesar.CypherKey);
                    _context.Update(caesar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaesarExists(caesar.Id))
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
            return View(caesar);
        }

        // GET: Caesars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Caesars == null)
            {
                return NotFound();
            }

            var caesar = await _context.Caesars
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caesar == null)
            {
                return NotFound();
            }

            return View(caesar);
        }

        // POST: Caesars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Caesars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Caesars'  is null.");
            }
            var caesar = await _context.Caesars.FindAsync(id);
            if (caesar != null)
            {
                _context.Caesars.Remove(caesar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaesarExists(Guid id)
        {
          return (_context.Caesars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        private bool IsValidPlaintext(string plaintext)
        {
            return !string.IsNullOrEmpty(plaintext) && plaintext.Length <= 255; // Example: Max length is 255 characters
        }

        private bool IsValidCypherKey(int cypherKey)
        {
            return cypherKey != 0;
        }
    }
}
