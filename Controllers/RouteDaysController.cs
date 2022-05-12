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
    public class RouteDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RouteDays
        public async Task<IActionResult> Index()
        {
            return View(await _context.RouteDays.ToListAsync());
        }

        // GET: RouteDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeDay = await _context.RouteDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routeDay == null)
            {
                return NotFound();
            }

            return View(routeDay);
        }

        // GET: RouteDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RouteDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day")] RouteDay routeDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routeDay);
        }

        // GET: RouteDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeDay = await _context.RouteDays.FindAsync(id);
            if (routeDay == null)
            {
                return NotFound();
            }
            return View(routeDay);
        }

        // POST: RouteDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day")] RouteDay routeDay)
        {
            if (id != routeDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteDayExists(routeDay.Id))
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
            return View(routeDay);
        }

        // GET: RouteDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeDay = await _context.RouteDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routeDay == null)
            {
                return NotFound();
            }

            return View(routeDay);
        }

        // POST: RouteDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeDay = await _context.RouteDays.FindAsync(id);
            _context.RouteDays.Remove(routeDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteDayExists(int id)
        {
            return _context.RouteDays.Any(e => e.Id == id);
        }
    }
}
