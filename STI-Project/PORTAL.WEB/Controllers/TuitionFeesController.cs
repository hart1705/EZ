using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PORTAL.DAL.EF;
using PORTAL.DAL.EF.Models;
using PORTAL.WEB.Filters;

namespace PORTAL.WEB.Controllers
{
    public class TuitionFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TuitionFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TuitionFees
        [AuthorizationService(true, "Tuition Fee - List")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TuitionFee.ToListAsync());
        }

        // GET: TuitionFees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuitionFee = await _context.TuitionFee
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tuitionFee == null)
            {
                return NotFound();
            }

            return View(tuitionFee);
        }

        // GET: TuitionFees/Create
        [AuthorizationService(true, "New Tuition Fee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TuitionFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizationService(true, "New Tuition Fee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount")] TuitionFee tuitionFee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tuitionFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tuitionFee);
        }

        // GET: TuitionFees/Edit/5
        [AuthorizationService(true, "Update Tuition Fee")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuitionFee = await _context.TuitionFee.SingleOrDefaultAsync(m => m.Id == id);
            if (tuitionFee == null)
            {
                return NotFound();
            }
            return View(tuitionFee);
        }

        // POST: TuitionFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizationService(true, "Update Tuition Fee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Amount")] TuitionFee tuitionFee)
        {
            if (id != tuitionFee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tuitionFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TuitionFeeExists(tuitionFee.Id))
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
            return View(tuitionFee);
        }

        // GET: TuitionFees/Delete/5
        [AuthorizationService(true, "Delete Tuition Fee")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuitionFee = await _context.TuitionFee
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tuitionFee == null)
            {
                return NotFound();
            }

            return View(tuitionFee);
        }

        // POST: TuitionFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuthorizationService(true, "Delete Tuition Fee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tuitionFee = await _context.TuitionFee.SingleOrDefaultAsync(m => m.Id == id);
            _context.TuitionFee.Remove(tuitionFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TuitionFeeExists(string id)
        {
            return _context.TuitionFee.Any(e => e.Id == id);
        }
    }
}
