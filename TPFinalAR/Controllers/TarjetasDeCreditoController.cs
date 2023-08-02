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
    public class TarjetasDeCreditoController : Controller
    {
        private readonly MyContext _context;

        public TarjetasDeCreditoController(MyContext context)
        {
            _context = context;
        }

        // GET: TarjetasDeCredito
        public async Task<IActionResult> Index()
        {
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();
            var myContext = _context.tarjetas.Include(t => t._titular);

            if(User.IsInRole("True"))
            {
                return View(await myContext.ToListAsync());
            }
            else
            {
                return View(myContext.Where(t => t._titular._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList());
            }
        }

        // GET: TarjetasDeCredito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tarjetas == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetas
                .Include(t => t._titular)
                .FirstOrDefaultAsync(m => m._id_tarjeta == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaDeCredito);
        }

        // GET: TarjetasDeCredito/Create
        public IActionResult Create()
        {
            if(User.IsInRole("True"))
            {
                ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
            }
            
            return View();
        }

        // POST: TarjetasDeCredito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id_tarjeta,_id_usuario,_limite")] TarjetaDeCredito tarjetaDeCredito)
        {
            if (tarjetaDeCredito._id_usuario==0)
            {
                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault();
                tarjetaDeCredito._id_usuario = usuario._id_usuario;
            }
            else
            {
                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == tarjetaDeCredito._id_usuario).FirstOrDefault();
            }

            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));

            var random = new Random(seed);
            var value = random.NextInt64(100000000000000000, 999999999999999999);

            tarjetaDeCredito._numero = value.ToString();

            // Asignamos tarjeta de manera aleatoria
            while (_context.tarjetas.Where(t => t._numero == tarjetaDeCredito._numero).Count()>0)
            {
                value = random.NextInt64(100000000000000000, 999999999999999999);
                tarjetaDeCredito._numero = value.ToString();
            }

            var valueV = random.Next(1, 9);

            tarjetaDeCredito._codigoV = valueV;

            // Las tarjetas nacen con consumo 0
            tarjetaDeCredito._consumos = 0;

            if (ModelState.IsValid)
            {
                TempData["Message"] = "ok";
                _context.Add(tarjetaDeCredito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (User.IsInRole("True"))
            {
                ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
            }
            return View(tarjetaDeCredito);
        }

        // GET: TarjetasDeCredito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tarjetas == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetas.FindAsync(id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", tarjetaDeCredito._id_usuario);
            return View(tarjetaDeCredito);
        }

        // POST: TarjetasDeCredito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("_id_tarjeta,_id_usuario,_numero,_codigoV,_limite,_consumos")] TarjetaDeCredito tarjetaDeCredito)
        {
            if (id != tarjetaDeCredito._id_tarjeta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Message"] = "ok";
                    _context.Update(tarjetaDeCredito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetaDeCreditoExists(tarjetaDeCredito._id_tarjeta))
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
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", tarjetaDeCredito._id_usuario);
            return View(tarjetaDeCredito);
        }

        // GET: TarjetasDeCredito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tarjetas == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetas
                .Include(t => t._titular)
                .FirstOrDefaultAsync(m => m._id_tarjeta == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }
            
            return View(tarjetaDeCredito);
        }

        // POST: TarjetasDeCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int _id_tarjeta)
        {
            if (_context.tarjetas == null)
            {
                return Problem("Entity set 'MyContext.tarjetas'  is null.");
            }
            var tarjetaDeCredito = await _context.tarjetas.FindAsync(_id_tarjeta);
            if (tarjetaDeCredito != null)
            {
                if(tarjetaDeCredito._consumos == 0) {
                    TempData["Message"] = "ok";
                    _context.tarjetas.Remove(tarjetaDeCredito);
                } else
                {
                    ViewBag.Message = "nok";
                    tarjetaDeCredito = await _context.tarjetas
                    .Include(t => t._titular)
                    .FirstOrDefaultAsync(m => m._id_tarjeta == _id_tarjeta);
                    return View(tarjetaDeCredito);
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TarjetasDeCredito/Pagar/5
        public async Task<IActionResult> Pagar(int? id)
        {
            if (id == null || _context.tarjetas == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetas
                .Include(t => t._titular)
                .FirstOrDefaultAsync(m => m._id_tarjeta == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            _context.usuarios.Include(u => u.cajas).Load();
            if(User.IsInRole("True"))
            {
                ViewBag._cbu = new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
            }
            else
            {
                ViewBag._cbu = new SelectList(_context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault().cajas.ToList(), "_cbu", "_cbu");
            }
            
            return View(tarjetaDeCredito);
        }

        // POST: TarjetasDeCredito/Pagar/5
        [HttpPost, ActionName("Pagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pagar(int _id_tarjeta, string cbu)
        {
            if (_context.tarjetas == null)
            {
                return Problem("Entity set 'MyContext.tarjetas'  is null.");
            }

            CajaDeAhorro caja = await _context.cajas.FirstOrDefaultAsync(m => m._cbu == cbu);

            var tarjetaDeCredito = await _context.tarjetas.FindAsync(_id_tarjeta);
            if (tarjetaDeCredito != null)
            {
                if (tarjetaDeCredito._consumos > 0 && caja._saldo >= tarjetaDeCredito._consumos)
                {                    
                    caja._saldo = caja._saldo - tarjetaDeCredito._consumos;
                    Movimiento movimiento = new Movimiento(caja._id_caja, "Pago de tarjeta", tarjetaDeCredito._consumos, DateTime.Now);
                    _context.Add(movimiento);
                    tarjetaDeCredito._consumos = 0;
                    TempData["Message"] = "ok";
                    _context.tarjetas.Update(tarjetaDeCredito);
                    _context.cajas.Update(caja);
                }
                else
                {
                    ViewBag.Message = "nok";
                    tarjetaDeCredito = await _context.tarjetas
                    .Include(t => t._titular)
                    .FirstOrDefaultAsync(m => m._id_tarjeta == _id_tarjeta);
                    _context.usuarios.Include(u => u.cajas).Load();
                    if (User.IsInRole("True"))
                    {
                        ViewBag._cbu = new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                    }
                    else
                    {
                        ViewBag._cbu = new SelectList(_context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault().cajas.ToList(), "_cbu", "_cbu");
                    }
                    return View(tarjetaDeCredito);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TarjetasDeCredito/Limite/5
        public async Task<IActionResult> Limite(int? id)
        {
            if (id == null || _context.tarjetas == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetas
                .Include(t => t._titular)
                .FirstOrDefaultAsync(m => m._id_tarjeta == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaDeCredito);
        }

        // POST: TarjetasDeCredito/Limite/5
        [HttpPost, ActionName("Limite")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Limite(int _id_tarjeta, double nuevolimite)
        {
            if (_context.tarjetas == null)
            {
                return Problem("Entity set 'MyContext.tarjetas'  is null.");
            }

            var tarjetaDeCredito = await _context.tarjetas.FindAsync(_id_tarjeta);
            if (tarjetaDeCredito != null)
            {
                tarjetaDeCredito._limite = nuevolimite;
                TempData["Message"] = "ok";
                _context.tarjetas.Update(tarjetaDeCredito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetaDeCreditoExists(int id)
        {
          return (_context.tarjetas?.Any(e => e._id_tarjeta == id)).GetValueOrDefault();
        }
    }
}
