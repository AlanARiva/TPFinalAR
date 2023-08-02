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
    public class UsuariosCajasDeAhorroController : Controller
    {
        private readonly MyContext _context;

        public UsuariosCajasDeAhorroController(MyContext context)
        {
            _context = context;
        }

        // GET: UsuariosCajasDeAhorro
        public async Task<IActionResult> Index()
        {
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();
            var myContext = _context.UsuarioCajaDeAhorro.Include(u => u.caja).Include(u => u.user);

            if(User.IsInRole("True"))
            {
                return View(await myContext.ToListAsync());
            }
            else
            {
                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).Include(c => c.cajas).Include(r => r.usuarioCajas).FirstOrDefault();

                List<UsuarioCajaDeAhorro> userCaja = new List<UsuarioCajaDeAhorro>();
                List<UsuarioCajaDeAhorro> userCajaTotal = new List<UsuarioCajaDeAhorro>();

                foreach (CajaDeAhorro u in usuario.cajas)
                {
                    userCaja = myContext.Where(t => t.id_caja == u._id_caja).ToList();
                    foreach (UsuarioCajaDeAhorro ut in userCaja)
                    {
                        userCajaTotal.Add(ut);
                    }
                }

                return View(userCajaTotal);
            }

        }

        // GET: UsuariosCajasDeAhorro/Details/5
        public async Task<IActionResult> Details(int? id_usuario, int? id_caja)
        {
            if (id_usuario == null || id_caja == null ||_context.UsuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro
                .Include(u => u.caja)
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.id_caja == id_caja && m.id_usuario == id_usuario);
            if (usuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(usuarioCajaDeAhorro);
        }

        // GET: UsuariosCajasDeAhorro/Create
        public IActionResult Create()
        {
            if (User.IsInRole("True"))
            {
                ViewData["id_caja"] = new SelectList(_context.cajas, "_id_caja", "_cbu");
                ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_dni");
            }
            else
            {
                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).Include(c => c.cajas).Include(r => r.usuarioCajas).FirstOrDefault();

                ViewData["id_caja"] = new SelectList(usuario.cajas, "_id_caja", "_cbu");
                ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_dni");
            }

            return View();
        }

        // POST: UsuariosCajasDeAhorro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_caja,id_usuario")] UsuarioCajaDeAhorro usuarioCajaDeAhorro)
        {
            if (ModelState.IsValid)
            {
                UsuarioCajaDeAhorro relacion = _context.UsuarioCajaDeAhorro.Where(t => t.id_caja == usuarioCajaDeAhorro.id_caja && t.id_usuario == usuarioCajaDeAhorro.id_usuario).FirstOrDefault();
                if (relacion==null)
                {
                    _context.Add(usuarioCajaDeAhorro);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "ok";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "nok";
                    if (User.IsInRole("True"))
                    {
                        ViewData["id_caja"] = new SelectList(_context.cajas, "_id_caja", "_cbu");
                        ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_dni");
                    }
                    else
                    {
                        Usuario usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).Include(c => c.cajas).FirstOrDefault();

                        ViewData["id_caja"] = new SelectList(usuario.cajas, "_id_caja", "_cbu");
                        ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_dni");
                    }
                    return View(usuarioCajaDeAhorro);
                }
            }
            if (User.IsInRole("True"))
            {
                ViewData["id_caja"] = new SelectList(_context.cajas, "_id_caja", "_cbu");
                ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_dni");
            }
            else
            {
                Usuario usuario = _context.usuarios.Where(u => u._id_usuario == Int32.Parse(User.FindFirstValue(ClaimTypes.Sid))).Include(c => c.cajas).FirstOrDefault();

                ViewData["id_caja"] = new SelectList(usuario.cajas, "_id_caja", "_cbu");
                ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_dni");
            }
            return View(usuarioCajaDeAhorro);
        }

        // GET: UsuariosCajasDeAhorro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro.FindAsync(id);
            if (usuarioCajaDeAhorro == null)
            {
                return NotFound();
            }
            ViewData["id_caja"] = new SelectList(_context.cajas, "_id_caja", "_cbu", usuarioCajaDeAhorro.id_caja);
            ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", usuarioCajaDeAhorro.id_usuario);
            return View(usuarioCajaDeAhorro);
        }

        // POST: UsuariosCajasDeAhorro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_caja,id_usuario")] UsuarioCajaDeAhorro usuarioCajaDeAhorro)
        {
            if (id != usuarioCajaDeAhorro.id_caja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioCajaDeAhorro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioCajaDeAhorroExists(usuarioCajaDeAhorro.id_caja))
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
            ViewData["id_caja"] = new SelectList(_context.cajas, "_id_caja", "_cbu", usuarioCajaDeAhorro.id_caja);
            ViewData["id_usuario"] = new SelectList(_context.usuarios, "_id_usuario", "_id_usuario", usuarioCajaDeAhorro.id_usuario);
            return View(usuarioCajaDeAhorro);
        }

        // GET: UsuariosCajasDeAhorro/Delete/5
        public async Task<IActionResult> Delete(int? id_usuario, int? id_caja)
        {
            if (id_usuario == null || id_caja == null || _context.UsuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro
                .Include(u => u.caja)
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.id_caja == id_caja && m.id_usuario == id_usuario);
            if (usuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(usuarioCajaDeAhorro);
        }

        // POST: UsuariosCajasDeAhorro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id_usuario, int id_caja)
        {
            if (_context.UsuarioCajaDeAhorro == null)
            {
                return Problem("Entity set 'MyContext.UsuarioCajaDeAhorro'  is null.");
            }
            var usuarioCajaDeAhorro = _context.UsuarioCajaDeAhorro.Where(t => t.id_caja == id_caja && t.id_usuario == id_usuario).FirstOrDefault();
            var usuarioCajaDeAhorroAux = _context.UsuarioCajaDeAhorro.Where(t => t.id_caja == id_caja).Count();
            if (usuarioCajaDeAhorro != null)
            {
                if(usuarioCajaDeAhorroAux>1)
                { 
                    _context.UsuarioCajaDeAhorro.Remove(usuarioCajaDeAhorro);
                }
                else
                {
                    ViewBag.Message = "nokp";
                    usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro
                    .Include(u => u.caja)
                    .Include(u => u.user)
                    .FirstOrDefaultAsync(m => m.id_caja == id_caja && m.id_usuario == id_usuario);
                    if (usuarioCajaDeAhorro == null)
                    {
                        return NotFound();
                    }

                    return View(usuarioCajaDeAhorro);
                }
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "ok";
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioCajaDeAhorroExists(int id)
        {
          return (_context.UsuarioCajaDeAhorro?.Any(e => e.id_caja == id)).GetValueOrDefault();
        }
    }
}
