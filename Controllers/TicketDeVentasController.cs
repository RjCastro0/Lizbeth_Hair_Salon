using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lisbeth_Hair_Salon.Models;

namespace Lisbeth_Hair_Salon.Controllers
{
    public class TicketDeVentasController : Controller
    {
        private readonly HairSalonContext _context;

        public TicketDeVentasController(HairSalonContext context)
        {
            _context = context;
        }

        // GET: TicketDeVentas
        public async Task<IActionResult> Index()
        {
            var hairSalonContext = _context.TicketDeVenta.Include(t => t.Surcursal);
            return View(await hairSalonContext.ToListAsync());
        }

        // GET: TicketDeVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVentum = await _context.TicketDeVenta
                .Include(t => t.Surcursal)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketDeVentum == null)
            {
                return NotFound();
            }

            return View(ticketDeVentum);
        }

        // GET: TicketDeVentas/Create
        public IActionResult Create()
        {
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId");
            return View();
        }

        // POST: TicketDeVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,SurcursalId,Empleada,ClienteNombre")] TicketDeVentum ticketDeVentum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketDeVentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", ticketDeVentum.SurcursalId);
            return View(ticketDeVentum);
        }

        // GET: TicketDeVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVentum = await _context.TicketDeVenta.FindAsync(id);
            if (ticketDeVentum == null)
            {
                return NotFound();
            }
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", ticketDeVentum.SurcursalId);
            return View(ticketDeVentum);
        }

        // POST: TicketDeVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,SurcursalId,Empleada,ClienteNombre")] TicketDeVentum ticketDeVentum)
        {
            if (id != ticketDeVentum.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketDeVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketDeVentumExists(ticketDeVentum.TicketId))
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
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", ticketDeVentum.SurcursalId);
            return View(ticketDeVentum);
        }

        // GET: TicketDeVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVentum = await _context.TicketDeVenta
                .Include(t => t.Surcursal)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketDeVentum == null)
            {
                return NotFound();
            }

            return View(ticketDeVentum);
        }

        // POST: TicketDeVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TicketDeVenta == null)
            {
                return Problem("Entity set 'HairSalonContext.TicketDeVenta'  is null.");
            }
            var ticketDeVentum = await _context.TicketDeVenta.FindAsync(id);
            if (ticketDeVentum != null)
            {
                _context.TicketDeVenta.Remove(ticketDeVentum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketDeVentumExists(int id)
        {
          return (_context.TicketDeVenta?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
