using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinalAR.Models;

namespace TPFinalAR.Controllers
{
    public class PagosController : Controller
    {
        private readonly MyContext _context;

        public PagosController(MyContext context)
        {
            _context = context;
        }

        // GET: Pagos
        public async Task<IActionResult> Index()
        {
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();
            var myContext = _context.pagos.Include(p => p._usuario);

            if(User.IsInRole("True"))
            {
                return View(await myContext.ToListAsync());
            }
            else
            {
                return View(myContext.Where(t => t._usuario._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList());
            }
        }

        // GET: Pagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.pagos
                .Include(p => p._usuario)
                .FirstOrDefaultAsync(m => m._id_pago == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // GET: Pagos/Create
        public IActionResult Create()
        {
            if (User.IsInRole("True")) 
            { 
                ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
            }

            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id_pago,_id_usuario,_monto,_pagado,_metodo,_detalle,_id_metodo")] Pago pago)
        {
            if(!User.IsInRole("True"))
            {
                pago._id_usuario = Int32.Parse(User.FindFirstValue(ClaimTypes.Sid));
            }
            if (ModelState.IsValid)
            {
                TempData["Message"] = "ok";
                pago._metodo = "Pendiente";
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("True"))
            {
                ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
            }
            return View(pago);
        }

        // GET: Pagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", pago._id_usuario);
            return View(pago);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("_id_pago,_id_usuario,_monto,_pagado,_metodo,_detalle,_id_metodo")] Pago pago)
        {
            if (id != pago._id_pago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Message"] = "ok";
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago._id_pago))
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
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", pago._id_usuario);
            return View(pago);
        }

        // GET: Pagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.pagos
                .Include(p => p._usuario)
                .FirstOrDefaultAsync(m => m._id_pago == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int _id_pago)
        {
            if (_context.pagos == null)
            {
                return Problem("Entity set 'MyContext.pagos'  is null.");
            }
            var pago = await _context.pagos.FindAsync(_id_pago);
            if (pago != null)
            {
                if(!pago._pagado) { 
                    TempData["Message"] = "ok";
                    _context.pagos.Remove(pago);
                }
                else
                {
                    ViewBag.Message = "nok";
                    return View(pago);
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Pagos/Pagar/5
        public async Task<IActionResult> Pagar(int? id)
        {
            if (id == null || _context.pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.pagos
                .Include(p => p._usuario)
                .FirstOrDefaultAsync(m => m._id_pago == id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.usuarios.Include(u => u.cajas).Load();
            _context.usuarios.Include(u => u._tarjetas).Load();

            Usuario usuario = _context.usuarios.Where(u => u._id_usuario == pago._id_usuario).FirstOrDefault();

            if(User.IsInRole("True")) 
            {
                ViewBag.cbu= new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                ViewBag.tarjeta = new SelectList(_context.tarjetas.ToList(), "_numero", "_numero");
            }
            else
            {
                ViewBag.cbu= new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");
                ViewBag.tarjeta = new SelectList(usuario._tarjetas.ToList(), "_numero", "_numero");
            }

            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Pagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pagar(int _id_pago, string cbu, string tarjeta)
        {
            if (_context.pagos == null)
            {
                return Problem("Entity set 'MyContext.pagos'  is null.");
            }
            var pago = await _context.pagos.FindAsync(_id_pago);
            if (pago != null)
            {
                if (!pago._pagado)
                {
                    if (cbu!="" && cbu!=null && tarjeta!="" && tarjeta!=null) {
                        ViewBag.Message = "nokm";
                        _context.usuarios.Include(u => u.cajas).Load();
                        _context.usuarios.Include(u => u._tarjetas).Load();
                        Usuario usuario = _context.usuarios.Where(u => u._id_usuario == pago._id_usuario).FirstOrDefault();

                        if (User.IsInRole("True"))
                        {
                            ViewBag.cbu= new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                            ViewBag.tarjeta = new SelectList(_context.tarjetas.ToList(), "_numero", "_numero");
                        }
                        else
                        {
                            ViewBag.cbu= new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");
                            ViewBag.tarjeta = new SelectList(usuario._tarjetas.ToList(), "_numero", "_numero");
                        }
                        return View(pago);
                    } 
                    else
                    {
                        if (cbu!="" && cbu!=null)
                        {
                            // selecciono una CA
                            CajaDeAhorro caja = _context.cajas.Where(t => t._cbu == cbu).FirstOrDefault();
                            if (caja!=null && caja._saldo >= pago._monto)
                            {
                                caja._saldo = caja._saldo - pago._monto;
                                pago._pagado = true;
                                pago._metodo = "Caja de ahorro";
                                _context.cajas.Update(caja);
                                _context.pagos.Update(pago);
                                Movimiento movimiento = new Movimiento(caja._id_caja, "Pago de servicios con caja de ahorro", pago._monto, DateTime.Now);
                                _context.Add(movimiento);
                                await _context.SaveChangesAsync();
                                TempData["Message"] = "ok";
                                return RedirectToAction(nameof(Index));
                            } 
                            else
                            {
                                _context.usuarios.Include(u => u.cajas).Load();
                                _context.usuarios.Include(u => u._tarjetas).Load();
                                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == pago._id_usuario).FirstOrDefault();

                                if (User.IsInRole("True"))
                                {
                                    ViewBag.cbu= new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                                    ViewBag.tarjeta = new SelectList(_context.tarjetas.ToList(), "_numero", "_numero");
                                }
                                else
                                {
                                    ViewBag.cbu= new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");
                                    ViewBag.tarjeta = new SelectList(usuario._tarjetas.ToList(), "_numero", "_numero");
                                }
                                ViewBag.Message = "nok";
                                return View(pago);
                            }
                        } 
                        // selecciono una tarjeta
                        else
                        {
                            TarjetaDeCredito tc = _context.tarjetas.Where(t => t._numero == tarjeta).FirstOrDefault();
                            if (tc!=null && (tc._limite-tc._consumos) >= pago._monto)
                            {
                                tc._consumos = tc._consumos + pago._monto;
                                pago._pagado = true;
                                pago._metodo = "Tarjeta de crédito";
                                _context.tarjetas.Update(tc);
                                _context.pagos.Update(pago);
                                await _context.SaveChangesAsync();
                                TempData["Message"] = "ok";
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                _context.usuarios.Include(u => u.cajas).Load();
                                _context.usuarios.Include(u => u._tarjetas).Load();
                                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == pago._id_usuario).FirstOrDefault();

                                if (User.IsInRole("True"))
                                {
                                    ViewBag.cbu= new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                                    ViewBag.tarjeta = new SelectList(_context.tarjetas.ToList(), "_numero", "_numero");
                                }
                                else
                                {
                                    ViewBag.cbu= new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");
                                    ViewBag.tarjeta = new SelectList(usuario._tarjetas.ToList(), "_numero", "_numero");
                                }
                                ViewBag.Message = "nok";
                                return View(pago);
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "nokp";
                    return View(pago);
                }
            }
            
            return RedirectToAction(nameof(Index));

        }

        private bool PagoExists(int id)
        {
          return (_context.pagos?.Any(e => e._id_pago == id)).GetValueOrDefault();
        }
    }
}
