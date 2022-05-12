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
using BusStation.Models;
using Microsoft.AspNetCore.Authorization;

namespace BusStation.Controllers
{
    [Authorize]
    public class RoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Routes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Routes.Include(r => r.RouteDay).Include(r => r.BusStops);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.RouteDay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["RouteDayId"] = new SelectList(_context.RouteDays, "Id", "Day");
            ViewData["BusStopsId"] = new SelectList(_context.RouteDays, "Id", "Day");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RouteDayId")] Data.Entities.Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteDayId"] = new SelectList(_context.RouteDays, "Id", "Day", route.RouteDayId);
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.BusStops)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (route == null)
            {
                return NotFound();
            }
            ViewData["RouteDayId"] = new SelectList(_context.RouteDays, "Id", "Day", route.RouteDayId);

            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RouteDayId")] Data.Entities.Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
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
            ViewData["RouteDayId"] = new SelectList(_context.RouteDays, "Id", "Day", route.RouteDayId);

            return View(route);
        }

        public async Task<IActionResult> EditRoutesBusStops(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.BusStops)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (route == null)
            {
                return NotFound();
            }
            PopulateAssignedBusStopsData(route);

            return View(route);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoutesBusStops(int? id, string[] selectedBusStops)
        {
            if (id == null || selectedBusStops == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.BusStops)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (route == null)
            {
                return NotFound();
            }

            var selectedBusStopsHS = new HashSet<string>(selectedBusStops);
            var routeBusStops = new HashSet<int>
                (route.BusStops.Select(c => c.Id));

            foreach (var busStop in _context.BusStops)
            {
                if (selectedBusStopsHS.Contains(busStop.Id.ToString()))
                {
                    if (!routeBusStops.Contains(busStop.Id))
                    {
                        route.BusStops.Add(busStop);
                    }
                }
                else
                {

                    if (routeBusStops.Contains(busStop.Id))
                    {
                        var busStopToRemove = route.BusStops.FirstOrDefault(i => i.Id == busStop.Id);
                        route.BusStops.Remove(busStopToRemove);
                    }
                }
            }
            await _context.SaveChangesAsync();

            PopulateAssignedBusStopsData(route);

            return RedirectToAction("Index");
        }

        private void PopulateAssignedBusStopsData(Data.Entities.Route route)
        {
            var allBusStops = _context.BusStops;
            var busStopsId = new HashSet<int>(route.BusStops.Select(c => c.Id));
            var viewModel = new List<AssignedDataViewModel>();
            foreach (var busStops in allBusStops)
            {
                viewModel.Add(new AssignedDataViewModel
                {
                    Id = busStops.Id,
                    Title = busStops.Name,
                    Assigned = busStopsId.Contains(busStops.Id)
                });
            }
            ViewData["BusStops"] = viewModel;
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.RouteDay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }
    }
}
