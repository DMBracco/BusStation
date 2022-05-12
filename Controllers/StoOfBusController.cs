#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusStation.Data;
using BusStation.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BusStation.Controllers
{
    [Authorize]
    public class StoOfBusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoOfBusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StoOfBus
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoOfBuses.ToListAsync());
        }

        // GET: StoOfBus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoOfBus = await _context.StoOfBuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stoOfBus == null)
            {
                return NotFound();
            }

            return View(stoOfBus);
        }

        // GET: StoOfBus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoOfBus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phone")] StoOfBus stoOfBus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stoOfBus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stoOfBus);
        }

        // GET: StoOfBus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoOfBus = await _context.StoOfBuses.FindAsync(id);
            if (stoOfBus == null)
            {
                return NotFound();
            }
            return View(stoOfBus);
        }

        // POST: StoOfBus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phone")] StoOfBus stoOfBus)
        {
            if (id != stoOfBus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stoOfBus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoOfBusExists(stoOfBus.Id))
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
            return View(stoOfBus);
        }

        // GET: StoOfBus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoOfBus = await _context.StoOfBuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stoOfBus == null)
            {
                return NotFound();
            }

            return View(stoOfBus);
        }

        // POST: StoOfBus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stoOfBus = await _context.StoOfBuses.FindAsync(id);
            _context.StoOfBuses.Remove(stoOfBus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoOfBusExists(int id)
        {
            return _context.StoOfBuses.Any(e => e.Id == id);
        }
    }
}
