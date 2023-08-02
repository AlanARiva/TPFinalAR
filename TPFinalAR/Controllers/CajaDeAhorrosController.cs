using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinalAR.Models;

namespace TPFinalAR.Controllers
{
    public class CajaDeAhorrosController : Controller
    {
        private readonly MyContext _context;

        public CajaDeAhorrosController(MyContext context)
        {
            _context = context;
        }

        // GET: CajaDeAhorros
        public async Task<IActionResult> Index()
        {
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();
            if(User.IsInRole("True")) { 
                return _context.cajas != null ? 
                              View(await _context.cajas.ToListAsync()) :
                              Problem("Entity set 'MyContext.cajas'  is null.");
            }
            else
            {
                _context.usuarios.Include(u => u.cajas).Load();
                return _context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault().cajas != null ?
                              View(_context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault().cajas.ToList()) :
                              Problem("Entity set 'MyContext.cajas'  is null.");
            } 
                
        }

        // GET: CajaDeAhorros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.cajas == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajas
                .FirstOrDefaultAsync(m => m._id_caja == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        [HttpGet]
        // GET: CajaDeAhorros/Despositar/5
        public async Task<IActionResult> Depositar(int? id)
        {
            if (id == null || _context.cajas == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajas
                .FirstOrDefaultAsync(m => m._id_caja == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        [HttpGet]
        // GET: CajaDeAhorros/Extraer/5
        public async Task<IActionResult> Extraer(int? id)
        {
            if (id == null || _context.cajas == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajas
                .FirstOrDefaultAsync(m => m._id_caja == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        [HttpGet]
        // GET: CajaDeAhorros/Transferir/5
        public async Task<IActionResult> Transferir(int? id)
        {
            if (id == null || _context.cajas == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajas
                .FirstOrDefaultAsync(m => m._id_caja == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        // GET: CajaDeAhorros/Create
        public IActionResult Create()
        {
            if (User.IsInRole("True"))
            {
                ViewBag.Usuarios = new SelectList(_context.usuarios, "_id_usuario", "_dni");
            }
            return View();
        }

        // POST: CajaDeAhorros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? idusuario)
        {
            CajaDeAhorro cajaDeAhorro = new CajaDeAhorro();
            Usuario usuario = new Usuario();
            if (idusuario!=null)
            {
                usuario = _context.usuarios.Where(u => u._id_usuario == idusuario).FirstOrDefault();
            }
            else
            {
                usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault();
            }


            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));

            var random = new Random(seed);
            var value = random.NextInt64(100000000000000000, 999999999999999999);

            cajaDeAhorro._cbu = value.ToString();

            // Asignamos CBU de manera aleatoria
            while (_context.cajas.Where(c => c._cbu == cajaDeAhorro._cbu).Count()>0)
            {
                value = random.NextInt64(100000000000000000, 999999999999999999);
                cajaDeAhorro._cbu = value.ToString();
            }

            // La caja de ahorro nace con saldo 0
            cajaDeAhorro._saldo = 0;

            cajaDeAhorro.titulares.Add(usuario);

            if (ModelState.IsValid)
            {
                 TempData["Message"] = "ok";
                 _context.Add(cajaDeAhorro);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            }
                
            if (User.IsInRole("True"))
            {
                ViewBag.Usuarios = new SelectList(_context.usuarios, "_id_usuario", "_dni");
            }
            return View(cajaDeAhorro);
        }

        [HttpPost, ActionName("Depositar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Depositar(int _id_caja, double importe)
        {
            CajaDeAhorro caja = _context.cajas.Where(u => u._id_caja == _id_caja).FirstOrDefault();
            
            if (caja != null)
            {
                caja._saldo = caja._saldo + importe;
                Movimiento movimiento = new Movimiento(caja._id_caja, "Deposito en cuenta", importe, DateTime.Now);
                _context.Add(movimiento);
                _context.Update(caja);
                await _context.SaveChangesAsync();
                TempData["Message"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            return View(caja);
        }

        [HttpPost, ActionName("Extraer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Extraer(int _id_caja, double importe)
        {
            CajaDeAhorro caja = _context.cajas.Where(u => u._id_caja == _id_caja).FirstOrDefault();

            if (caja != null)
            {
                if(caja._saldo >= importe) { 
                    caja._saldo = caja._saldo - importe;
                    Movimiento movimiento = new Movimiento(caja._id_caja, "Extracción en cuenta", importe, DateTime.Now);
                    _context.Add(movimiento);
                    _context.Update(caja);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "ok";
                    return RedirectToAction(nameof(Index));
                } else
                {
                    ViewBag.Message = "nok";
                    return View(caja);
                }
            }
            return View(caja);
        }

        [HttpPost, ActionName("Transferir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transferir(int _id_caja, string cbuDestino, double importe)
        {
            CajaDeAhorro caja = _context.cajas.Where(u => u._id_caja == _id_caja).FirstOrDefault();
            CajaDeAhorro cajaDestino = _context.cajas.Where(u => u._cbu == cbuDestino).FirstOrDefault();

            if (caja != null && cajaDestino != null)
            {
                if (caja._saldo >= importe)
                {
                    caja._saldo = caja._saldo - importe;
                    cajaDestino._saldo = cajaDestino._saldo + importe;
                    Movimiento movimiento = new Movimiento(caja._id_caja, "Transferencia emitida", importe, DateTime.Now);
                    _context.Add(movimiento);
                    Movimiento movimientoDest = new Movimiento(cajaDestino._id_caja, "Transferencia recibida", importe, DateTime.Now);
                    _context.Add(movimientoDest);
                    _context.Update(caja);
                    _context.Update(cajaDestino);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "ok";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "nok";
                    return View(caja);
                }
            } else { ViewBag.Message = "nok"; }
            return View(caja);
        }

        // GET: CajaDeAhorros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.cajas == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajas.FindAsync(id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }
            return View(cajaDeAhorro);
        }

        // POST: CajaDeAhorros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("_id_caja,_cbu,_saldo")] CajaDeAhorro cajaDeAhorro)
        {
            if (id != cajaDeAhorro._id_caja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cajaDeAhorro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajaDeAhorroExists(cajaDeAhorro._id_caja))
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
            return View(cajaDeAhorro);
        }

        // GET: CajaDeAhorros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.cajas == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajas
                .FirstOrDefaultAsync(m => m._id_caja == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        // POST: CajaDeAhorros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int _id_caja)
        {
            if (_context.cajas == null)
            {
                return Problem("Entity set 'MyContext.cajas'  is null.");
            }
            var cajaDeAhorro = await _context.cajas.FindAsync(_id_caja);
            if (cajaDeAhorro != null && cajaDeAhorro._saldo==0)
            {
                TempData["Message"] = "ok";
                _context.cajas.Remove(cajaDeAhorro);
            }
            else
            {
                ViewBag.Message = "nok-saldo";
                return View(cajaDeAhorro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CajaDeAhorroExists(int id)
        {
          return (_context.cajas?.Any(e => e._id_caja == id)).GetValueOrDefault();
        }
    }
}
