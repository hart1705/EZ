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
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings
        [AuthorizationService(true, "Settings - List")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Setting.ToListAsync());
        }

        // GET: Settings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Setting
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        [AuthorizationService(true, "New Settings")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizationService(true, "New Settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: Settings/Edit/5
        [AuthorizationService(true, "Update Settings")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Setting.SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizationService(true, "Update Settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Description")] Setting setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
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
            return View(setting);
        }

        // GET: Settings/Delete/5
        [AuthorizationService(true, "Delete Settings")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Setting
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuthorizationService(true, "Delete Settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var setting = await _context.Setting.SingleOrDefaultAsync(m => m.Id == id);
            _context.Setting.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SettingExists(string id)
        {
            return _context.Setting.Any(e => e.Id == id);
        }
    }
}
