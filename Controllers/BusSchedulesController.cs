
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusStation.Data;
using BusStation.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BusStation.Controllers
{
    [Authorize]
    public class BusSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: BusSchedules
        public async Task<IActionResult> Index(DateTime? DepartureTimeFilter, DateTime? ArrivalTimeFilter, int RouteId)
        {
            var dataView = new List<BusSchedule>();
            var applicationDbContext = await _context.BusSchedules.Include(b => b.Buses).Include(b => b.Platforms).Include(b => b.Routes).ToListAsync();
            dataView = applicationDbContext;
            if (null != DepartureTimeFilter)
            {
                dataView = dataView.Where(s => s.DepartureTime >= DepartureTimeFilter).ToList();
            }
            if (null != ArrivalTimeFilter)
            {
                dataView = dataView.Where(s => s.ArrivalTime <= ArrivalTimeFilter).ToList();
            }
            if (0 != RouteId)
            {
                dataView = dataView.Where(s => s.RouteId == RouteId).ToList();
            }

            return View(dataView);
        }

        // GET: BusSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busSchedule = await _context.BusSchedules
                .Include(b => b.Buses)
                .Include(b => b.Platforms)
                .Include(b => b.Routes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busSchedule == null)
            {
                return NotFound();
            }

            return View(busSchedule);
        }

        // GET: BusSchedules/Create
        public IActionResult Create()
        {
            ViewData["BusId"] = new SelectList(_context.Buses, "Id", "GosNumber");
            ViewData["PlatformId"] = new SelectList(_context.Platforms, "Id", "Name");
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id");
            return View();
        }

        // POST: BusSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartureTime,ArrivalTime,BusId,RouteId,PlatformId")] BusSchedule busSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusId"] = new SelectList(_context.Buses, "Id", "Id", busSchedule.BusId);
            ViewData["PlatformId"] = new SelectList(_context.Platforms, "Id", "Name", busSchedule.PlatformId);
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id", busSchedule.RouteId);
            return View(busSchedule);
        }

        // GET: BusSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busSchedule = await _context.BusSchedules.FindAsync(id);
            if (busSchedule == null)
            {
                return NotFound();
            }
            ViewData["BusId"] = new SelectList(_context.Buses, "Id", "Id", busSchedule.BusId);
            ViewData["PlatformId"] = new SelectList(_context.Platforms, "Id", "Name", busSchedule.PlatformId);
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id", busSchedule.RouteId);
            return View(busSchedule);
        }

        // POST: BusSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartureTime,ArrivalTime,BusId,RouteId,PlatformId")] BusSchedule busSchedule)
        {
            if (id != busSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusScheduleExists(busSchedule.Id))
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
            ViewData["BusId"] = new SelectList(_context.Buses, "Id", "Id", busSchedule.BusId);
            ViewData["PlatformId"] = new SelectList(_context.Platforms, "Id", "Name", busSchedule.PlatformId);
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id", busSchedule.RouteId);
            return View(busSchedule);
        }

        // GET: BusSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busSchedule = await _context.BusSchedules
                .Include(b => b.Buses)
                .Include(b => b.Platforms)
                .Include(b => b.Routes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busSchedule == null)
            {
                return NotFound();
            }

            return View(busSchedule);
        }

        // POST: BusSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var busSchedule = await _context.BusSchedules.FindAsync(id);
            _context.BusSchedules.Remove(busSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusScheduleExists(int id)
        {
            return _context.BusSchedules.Any(e => e.Id == id);
        }
    }
}
