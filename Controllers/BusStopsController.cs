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
    public class BusStopsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusStopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BusStops
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusStops.ToListAsync());
        }

        // GET: BusStops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busStop = await _context.BusStops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busStop == null)
            {
                return NotFound();
            }

            return View(busStop);
        }

        // GET: BusStops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusStops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BusStop busStop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busStop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(busStop);
        }

        // GET: BusStops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busStop = await _context.BusStops.FindAsync(id);
            if (busStop == null)
            {
                return NotFound();
            }
            return View(busStop);
        }

        // POST: BusStops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BusStop busStop)
        {
            if (id != busStop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busStop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusStopExists(busStop.Id))
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
            return View(busStop);
        }

        // GET: BusStops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busStop = await _context.BusStops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busStop == null)
            {
                return NotFound();
            }

            return View(busStop);
        }

        // POST: BusStops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var busStop = await _context.BusStops.FindAsync(id);
            _context.BusStops.Remove(busStop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusStopExists(int id)
        {
            return _context.BusStops.Any(e => e.Id == id);
        }
    }
}
