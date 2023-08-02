using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinalAR.Models;

namespace TPFinalAR.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MyContext _context;

        public UsuariosController(MyContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();
            //ViewBag.Id = User.FindFirstValue(ClaimTypes.Sid);

            return _context.usuarios != null ? 
                          View(await _context.usuarios.ToListAsync()) :
                          Problem("Entity set 'MyContext.usuarios'  is null.");
        }

        // GET: Usuarios/CerrarSesion
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "ok-cierre";
            return RedirectToAction(nameof(Login));
        }

        // GET: Usuarios/Login
        public async Task<IActionResult> Login()
        {
            if (TempData["Message"]!=null)
                ViewBag.Message = TempData["Message"].ToString();
            return View();
        }

        // POST: Usuarios/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string mail, string password)
        {

            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "nok";
                return View();
            }

            Usuario usuario = _context.usuarios.Where(t => t._mail == mail && t._password == password).FirstOrDefault();

            if(usuario!=null)
            {
                if(!usuario._bloqueado)
                { 
                    usuario._intentosFallidos = 0;
                    _context.usuarios.Update(usuario);
                    //TempData["Message"] = "ok";
                    await _context.SaveChangesAsync();

                    var claims = new List<Claim>
                                 {
                                     new Claim(ClaimTypes.Sid, usuario._id_usuario.ToString()),
                                     new Claim(ClaimTypes.Name, usuario._nombre + " " + usuario._apellido),
                                     new Claim(ClaimTypes.Email, usuario._mail),
                                     new Claim(ClaimTypes.Role, usuario._esUsuarioAdmin.ToString())
                                 };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                    new ClaimsPrincipal(claimsIdentity),
                                                    authProperties);
                    //HttpContext.Session.SetString("conectado", "True");
                    return RedirectToAction("Index", "Home");
                } 
                else
                {
                    ViewBag.Message = "nok-bloqueado";
                    return View();
                }
            }
            else
            {
                usuario = _context.usuarios.Where(t => t._mail == mail).FirstOrDefault();
                if (usuario!=null)
                {
                    usuario._intentosFallidos++;
                    if (usuario._intentosFallidos == 3)
                    {
                        usuario._bloqueado = true;
                    }
                    _context.usuarios.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                ViewBag.Message = "nokp";
                return View();
            }
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m._id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id_usuario,_dni,_nombre,_apellido,_mail,_password,_intentosFallidos,_esUsuarioAdmin,_bloqueado")] Usuario usuario)
        {
            if(_context.usuarios.Where(u => u._dni== usuario._dni).Count()==0 && _context.usuarios.Where(u => u._mail== usuario._mail).Count()==0)
            { 
                if (ModelState.IsValid)
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    if (User.IsInRole("True") || User.IsInRole("False"))
                    { 
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Message"] = "ok-registro";
                        return RedirectToAction(nameof(Login));
                    }
                }
            }
            else
            {
                ViewBag.Message = "nok-repeat";
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("_id_usuario,_dni,_nombre,_apellido,_mail,_password,_intentosFallidos,_esUsuarioAdmin,_bloqueado")] Usuario usuario)
        {
            if (id != usuario._id_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario._id_usuario))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m._id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int _id_usuario)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'MyContext.usuarios'  is null.");
            }
            var usuario = await _context.usuarios.FindAsync(_id_usuario);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Estado/5
        public async Task<IActionResult> Estado(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m._id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Estado/5
        [HttpPost, ActionName("Estado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Estado(int _id_usuario)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'MyContext.usuarios'  is null.");
            }
            var usuario = await _context.usuarios.FindAsync(_id_usuario);
            if (usuario != null)
            {
                usuario._bloqueado = !usuario._bloqueado;
                usuario._intentosFallidos = 0;
                _context.usuarios.Update(usuario);
                TempData["Message"] = "ok";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.usuarios?.Any(e => e._id_usuario == id)).GetValueOrDefault();
        }
    }
}
