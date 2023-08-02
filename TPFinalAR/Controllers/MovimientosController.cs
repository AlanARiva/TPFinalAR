using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinalAR.Models;

namespace TPFinalAR.Controllers
{
    public class MovimientosController : Controller
    {
        private readonly MyContext _context;

        public MovimientosController(MyContext context)
        {
            _context = context;
        }

        // GET: Movimientos
        public async Task<IActionResult> Index()
        {
            Usuario usuario = _context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).Include(m => m.cajas).FirstOrDefault();
            var myContext = _context.movimientos.Include(m => m._cajaDeAhorro);

            List<Movimiento> movimientos = new List<Movimiento>();
            List<Movimiento> movimientosTotal = new List<Movimiento>();

            foreach (CajaDeAhorro caja in usuario.cajas)
            {
                movimientos = myContext.Where(t => t._id_CajaDeAhorro == caja._id_caja).ToList();
                foreach(Movimiento mov in movimientos)
                {
                    movimientosTotal.Add(mov);
                }

            }

            return View(movimientosTotal);
        }

        // GET: Movimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.movimientos == null)
            {
                return NotFound();
            }

            var movimiento = await _context.movimientos
                .Include(m => m._cajaDeAhorro)
                .FirstOrDefaultAsync(m => m._id_Movimiento == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimientos/Create
        public IActionResult Create()
        {
            ViewData["_id_CajaDeAhorro"] = new SelectList(_context.cajas, "_id_caja", "_cbu");
            return View();
        }

        // GET: Movimientos/Buscar
        public IActionResult Buscar()
        {
            ViewData["_id_CajaDeAhorro"] = new SelectList(_context.cajas, "_id_caja", "_cbu");
            return View();
        }

        // POST: Movimientos/Buscar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar(int cbu, string detalle, double monto, DateTime? fecha)
        {
            var myContext = _context.movimientos.Include(m => m._cajaDeAhorro);
            var myContextBkp = myContext.ToList();

            if (cbu!=0 && (detalle!=null || monto!=0 || fecha!=null)) { 
                myContextBkp = myContext.Where(t => t._id_CajaDeAhorro == cbu).ToList();
                if (detalle!="")
                    myContextBkp = myContextBkp.Where(t => t._detalle == detalle).ToList();
                if (monto!=0)
                    myContextBkp = myContextBkp.Where(t => t._monto == monto).ToList();
                if (fecha!=null)
                    myContextBkp = myContextBkp.Where(t => t._fecha == fecha).ToList();
            } 
            else
            {
                ViewBag.Message = "nok";
                ViewData["_id_CajaDeAhorro"] = new SelectList(_context.cajas, "_id_caja", "_cbu");
                return View();

            }

            return View(myContextBkp.ToList());
             
        }

        // POST: Movimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id_CajaDeAhorro,_id_Movimiento,_detalle,_monto,_fecha")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["_id_CajaDeAhorro"] = new SelectList(_context.cajas, "_id_caja", "_cbu", movimiento._id_CajaDeAhorro);
            return View(movimiento);
        }

        // GET: Movimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.movimientos == null)
            {
                return NotFound();
            }

            var movimiento = await _context.movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            ViewData["_id_CajaDeAhorro"] = new SelectList(_context.cajas, "_id_caja", "_cbu", movimiento._id_CajaDeAhorro);
            return View(movimiento);
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("_id_CajaDeAhorro,_id_Movimiento,_detalle,_monto,_fecha")] Movimiento movimiento)
        {
            if (id != movimiento._id_Movimiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoExists(movimiento._id_Movimiento))
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
            ViewData["_id_CajaDeAhorro"] = new SelectList(_context.cajas, "_id_caja", "_cbu", movimiento._id_CajaDeAhorro);
            return View(movimiento);
        }

        // GET: Movimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.movimientos == null)
            {
                return NotFound();
            }

            var movimiento = await _context.movimientos
                .Include(m => m._cajaDeAhorro)
                .FirstOrDefaultAsync(m => m._id_Movimiento == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.movimientos == null)
            {
                return Problem("Entity set 'MyContext.movimientos'  is null.");
            }
            var movimiento = await _context.movimientos.FindAsync(id);
            if (movimiento != null)
            {
                _context.movimientos.Remove(movimiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(int id)
        {
          return (_context.movimientos?.Any(e => e._id_Movimiento == id)).GetValueOrDefault();
        }
    }
}
