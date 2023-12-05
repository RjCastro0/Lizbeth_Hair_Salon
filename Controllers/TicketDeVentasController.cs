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
            var hairSalonContext = _context.TicketDeVenta.Include(t => t.Servicio).Include(t => t.Surcursal);

            var sumOfPricesByEmployee = await _context.TicketDeVenta
                .GroupBy(t => t.Empleada)
                .Select(g => new
                {
                    EmployeeName = g.Key,
                    TotalPrice = g.Sum(t => t.Precio)
                })
                .ToListAsync();

            ViewBag.SumOfPricesByEmployee = sumOfPricesByEmployee;

            return View(await hairSalonContext.ToListAsync());
        }


        // GET: TicketDeVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVenta = await _context.TicketDeVenta
                .Include(t => t.Servicio)
                .Include(t => t.Surcursal)
                .Include(t => t.Ventas)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketDeVenta == null)
            {
                return NotFound();
            }

            return View(ticketDeVenta);
        }

        // GET: TicketDeVentas/Create
        public IActionResult Create()
        {
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "NombreServicio");
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "Nombre");
            return View();
        }

        // POST: TicketDeVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,SurcursalId,Empleada,ServicioId,Precio,ClienteNombre")] TicketDeVenta ticketDeVenta)
        {
            
            _context.Add(ticketDeVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "NombreServicio", ticketDeVenta.ServicioId);
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "Nombre", ticketDeVenta.SurcursalId);
            return View(ticketDeVenta);
        }

        // GET: TicketDeVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVenta = await _context.TicketDeVenta.FindAsync(id);
            if (ticketDeVenta == null)
            {
                return NotFound();
            }
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "NombreServicio", ticketDeVenta.ServicioId);
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "Nombre", ticketDeVenta.SurcursalId);
            return View(ticketDeVenta);
        }

        // POST: TicketDeVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,SurcursalId,Empleada,ServicioId,Precio,ClienteNombre")] TicketDeVenta ticketDeVenta)
        {
            if (id != ticketDeVenta.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketDeVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketDeVentaExists(ticketDeVenta.TicketId))
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
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "ServicioId", ticketDeVenta.ServicioId);
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "Nombre", ticketDeVenta.SurcursalId);
            return View(ticketDeVenta);
        }

        // GET: TicketDeVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVenta = await _context.TicketDeVenta
                .Include(t => t.Servicio)
                .Include(t => t.Surcursal)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketDeVenta == null)
            {
                return NotFound();
            }

            return View(ticketDeVenta);
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
            var ticketDeVenta = await _context.TicketDeVenta.FindAsync(id);
            if (ticketDeVenta != null)
            {
                _context.TicketDeVenta.Remove(ticketDeVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketDeVentaExists(int id)
        {
          return (_context.TicketDeVenta?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> SumOfPricesByEmployee()
        {
            var sumOfPricesByEmployee = await _context.TicketDeVenta
                .GroupBy(t => t.Empleada)
                .Select(g => new
                {
                    EmployeeName = g.Key,
                    TotalPrice = g.Sum(t => t.Precio)
                })
                .ToListAsync();

            return View(sumOfPricesByEmployee);
        }

        [HttpPost]
        public IActionResult AgregarVenta(int ticketId, Venta nuevaVenta)
        {
            var ticket = _context.TicketDeVenta.Find(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            // Asignar el TicketDeVentaId para establecer la relación
            nuevaVenta.TicketDeVentaId = ticketId;

            // Agregar la nueva venta al ticket
            ticket.Ventas.Add(nuevaVenta);

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = ticketId });
        }

        // Otras acciones del controlador
    }

}

