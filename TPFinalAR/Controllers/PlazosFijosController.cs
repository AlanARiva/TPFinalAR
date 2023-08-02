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
    public class PlazosFijosController : Controller
    {
        private readonly MyContext _context;

        public PlazosFijosController(MyContext context)
        {
            _context = context;
        }

        // GET: PlazosFijos
        public async Task<IActionResult> Index()
        {

            verificarEstadoPlazosFijos();
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();

            var myContext = _context.PlazoFijo.Include(p => p._titular);
            //return View(await myContext.ToListAsync());
            if (User.IsInRole("True"))
            {
                return View(await myContext.ToListAsync());
            }
            else
            {
                return View(myContext.Where(t => t._titular._id_usuario ==Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList());
            }
        }

        public void verificarEstadoPlazosFijos()
        {
            var myContext = _context.PlazoFijo.Include(p => p._titular);
            var plazosFijosUsuario = myContext.Where(u => u._titular._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList();

            foreach (PlazoFijo pf in plazosFijosUsuario)
            {
                if(pf._fechaFin <= DateTime.Now && !pf._pagado)
                {
                    DateTime fechaFin = pf._fechaFin;
                    TimeSpan ts = fechaFin.Subtract(pf._fechaIni);
                    double diff = ts.TotalDays;
                    double montoCalculado = pf._monto + (pf._monto * (pf._tasa/365) * diff);
                    var myContextCajas = _context.cajas.Include(c => c.plazosFijos);

                    CajaDeAhorro caja = myContextCajas.Where(c => c.plazosFijos.Any(p => p._id_plazoFijo == pf._id_plazoFijo)).FirstOrDefault();
                    caja._saldo = caja._saldo + montoCalculado;
                    pf._pagado = true;

                    _context.PlazoFijo.Update(pf);
                    _context.cajas.Update(caja);
                    Movimiento movimiento = new Movimiento(caja._id_caja, "Pago de plazo fijo", montoCalculado, DateTime.Now);
                    _context.Add(movimiento);
                    _context.SaveChanges();
                }
            }

        }

        // GET: PlazosFijos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.PlazoFijo
                .Include(p => p._titular)
                .FirstOrDefaultAsync(m => m._id_plazoFijo == id);
            if (plazoFijo == null)
            {
                return NotFound();
            }

            return View(plazoFijo);
        }

        // GET: PlazosFijos/Create
        public IActionResult Create()
        {
            _context.usuarios.Include(u => u.cajas).Load();

            if (User.IsInRole("True"))
            { 
                ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
                ViewBag.cbu = new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
            }
            else
            {
                Usuario usuario = _context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault();
                ViewBag.cbu = new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");

            }
            
            return View();
        }

        // POST: PlazosFijos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id_plazoFijo,_id_usuario,_monto,_fechaIni,_fechaFin,_tasa,_pagado=false")] PlazoFijo plazoFijo, string cbu)
        {
            Usuario usuario = new Usuario();
            if (plazoFijo._id_usuario==0) { 
                usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault();
                plazoFijo._id_usuario = usuario._id_usuario;
            } 
            else
            {
                usuario = _context.usuarios.Where(u => u._id_usuario == plazoFijo._id_usuario).FirstOrDefault();
            }

            if (ModelState.IsValid)
            {
                if(plazoFijo._monto >= 1000 && plazoFijo._fechaFin> plazoFijo._fechaIni) { 
                    CajaDeAhorro caja = _context.cajas.Where(t => t._cbu == cbu).FirstOrDefault();
                    if(caja!=null && caja._saldo >= plazoFijo._monto)
                    {
                        TempData["Message"] = "ok";
                        caja._saldo = caja._saldo - plazoFijo._monto;
                        caja.plazosFijos.Add(plazoFijo);
                        _context.cajas.Update(caja);
                        _context.Add(plazoFijo);
                        Movimiento movimiento = new Movimiento(caja._id_caja, "Constitución de plazo fijo", plazoFijo._monto, DateTime.Now);
                        _context.Add(movimiento);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    } 
                    else
                    {
                        ViewBag.Message = "nokp";
                        _context.usuarios.Include(u => u.cajas).Load();
                        if (User.IsInRole("True"))
                        {
                            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
                            ViewBag.cbu = new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                        }
                        else
                        {
                            usuario = _context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault();
                            ViewBag.cbu = new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");

                        }
                        return View(plazoFijo);
                    }
                } 
                else
                {
                    ViewBag.Message = "noki";
                    _context.usuarios.Include(u => u.cajas).Load();
                    if (User.IsInRole("True"))
                    {
                        ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario");
                        ViewBag.cbu = new SelectList(_context.cajas.ToList(), "_cbu", "_cbu");
                    }
                    else
                    {
                        usuario = _context.usuarios.Where(t => t._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).FirstOrDefault();
                        ViewBag.cbu = new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");

                    }
                    return View(plazoFijo);
                }
            }

            ViewBag.Message = "nok";
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", plazoFijo._id_usuario);
            return View(plazoFijo);
        }

        // GET: PlazosFijos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.PlazoFijo.FindAsync(id);
            if (plazoFijo == null)
            {
                return NotFound();
            }
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", plazoFijo._id_usuario);
            return View(plazoFijo);
        }

        // POST: PlazosFijos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("_id_plazoFijo,_id_usuario,_monto,_fechaIni,_fechaFin,_tasa,_pagado")] PlazoFijo plazoFijo)
        {
            if (id != plazoFijo._id_plazoFijo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plazoFijo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlazoFijoExists(plazoFijo._id_plazoFijo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            ViewData["_id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", plazoFijo._id_usuario);
            return View(plazoFijo);
        }

        // GET: PlazosFijos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.PlazoFijo
                .Include(p => p._titular)
                .FirstOrDefaultAsync(m => m._id_plazoFijo == id);
            if (plazoFijo == null)
            {
                return NotFound();
            }

            return View(plazoFijo);
        }

        // POST: PlazosFijos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int _id_plazoFijo)
        {
            if (_context.PlazoFijo == null)
            {
                return Problem("Entity set 'MyContext.PlazoFijo'  is null.");
            }
            var plazoFijo = await _context.PlazoFijo.FindAsync(_id_plazoFijo);
            if (plazoFijo != null)
            {
                DateTime now = DateTime.Now;

                TimeSpan ts = now.Subtract(plazoFijo._fechaFin);
                double diff = ts.TotalDays;

                if(diff>30 && plazoFijo._pagado) { 
                    _context.PlazoFijo.Remove(plazoFijo);
                } else
                {
                    ViewBag.Message = "nok";
                    plazoFijo = await _context.PlazoFijo
                    .Include(p => p._titular)
                    .FirstOrDefaultAsync(m => m._id_plazoFijo == _id_plazoFijo);
                    return View(plazoFijo);
                }
            }

            TempData["Message"] = "ok";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PlazosFijos/Pagar/5
        public async Task<IActionResult> Pagar(int? id)
        {
            if (id == null || _context.PlazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.PlazoFijo
                .Include(p => p._titular)
                .FirstOrDefaultAsync(m => m._id_plazoFijo == id);
            if (plazoFijo == null)
            {
                return NotFound();
            }

            _context.usuarios.Include(u => u.cajas).Load();
            Usuario usuario = _context.usuarios.Where(t => t._id_usuario == plazoFijo._id_usuario).FirstOrDefault();

            ViewBag.cbu = new SelectList(usuario.cajas.ToList(), "_cbu", "_cbu");

            return View(plazoFijo);
        }

        // POST: PlazosFijos/Pagar/5
        [HttpPost, ActionName("Pagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pagar(int _id_plazoFijo, string cbu)
        {
            if (_context.PlazoFijo == null)
            {
                return Problem("Entity set 'MyContext.PlazoFijo'  is null.");
            }
            var plazoFijo = await _context.PlazoFijo.FindAsync(_id_plazoFijo);
            if (plazoFijo != null)
            {
                if (!plazoFijo._pagado) {                
                    DateTime fechaFin = plazoFijo._fechaFin;
                    TimeSpan ts = fechaFin.Subtract(plazoFijo._fechaIni);
                    double diff = ts.TotalDays;
                    double montoCalculado = plazoFijo._monto + (plazoFijo._monto * (plazoFijo._tasa/365) * diff);
                    var myContextCajas = _context.cajas.Include(c => c.plazosFijos);

                    CajaDeAhorro caja = myContextCajas.Where(c => c.plazosFijos.Any(p => p._id_plazoFijo == plazoFijo._id_plazoFijo)).FirstOrDefault();
                    caja._saldo = caja._saldo + montoCalculado;
                    plazoFijo._pagado = true;

                    _context.PlazoFijo.Update(plazoFijo);
                    _context.cajas.Update(caja);
                    Movimiento movimiento = new Movimiento(caja._id_caja, "Pago de plazo fijo", montoCalculado, DateTime.Now);
                    _context.Add(movimiento);
                    _context.SaveChanges();

                }
                else 
                {
                    ViewBag.Message = "nok";
                    plazoFijo = await _context.PlazoFijo
                    .Include(p => p._titular)
                    .FirstOrDefaultAsync(m => m._id_plazoFijo == _id_plazoFijo);
                    return View(plazoFijo);
                }
            }

            TempData["Message"] = "ok";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlazoFijoExists(int id)
        {
          return (_context.PlazoFijo?.Any(e => e._id_plazoFijo == id)).GetValueOrDefault();
        }
    }
}
