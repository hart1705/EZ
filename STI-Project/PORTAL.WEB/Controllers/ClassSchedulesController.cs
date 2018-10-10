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
    public class ClassSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassSchedules
        [AuthorizationService(true, "Class Schedule - List")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClassSchedule.ToListAsync());
        }

        // GET: ClassSchedules/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedule
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }

        // GET: ClassSchedules/Create
        [AuthorizationService(true, "New Class Schedule")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizationService(true, "New Class Schedule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Schedule")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classSchedule);
        }

        // GET: ClassSchedules/Edit/5
        [AuthorizationService(true, "Update Class Schedule")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedule.SingleOrDefaultAsync(m => m.Id == id);
            if (classSchedule == null)
            {
                return NotFound();
            }
            return View(classSchedule);
        }

        // POST: ClassSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizationService(true, "Update Class Schedule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Schedule")] ClassSchedule classSchedule)
        {
            if (id != classSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassScheduleExists(classSchedule.Id))
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
            return View(classSchedule);
        }

        // GET: ClassSchedules/Delete/5
        [AuthorizationService(true, "Delete Class Schedule")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedule
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }

        // POST: ClassSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuthorizationService(true, "Delete Class Schedule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var classSchedule = await _context.ClassSchedule.SingleOrDefaultAsync(m => m.Id == id);
            _context.ClassSchedule.Remove(classSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassScheduleExists(string id)
        {
            return _context.ClassSchedule.Any(e => e.Id == id);
        }
    }
}
