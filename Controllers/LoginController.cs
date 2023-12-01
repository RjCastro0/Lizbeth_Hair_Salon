using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lisbeth_Hair_Salon.Models;

namespace Lisbeth_Hair_Salon.Controllers
{
    public class LoginController : Controller
    {
        private readonly HairSalonContext _context;

        public LoginController(HairSalonContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Usuario> ObtenerUsuario()
        {
            List<Usuario> usuarios = _context.Usuarios.ToList();
            return usuarios;
        }

        public Usuario ValidarUsuario(string UserName, string Password)
        {
            return ObtenerUsuario().FirstOrDefault(item => item.UserName == UserName && item.Password == Password);
        }

        [HttpPost]
        public async Task<IActionResult> ValidarInicio(Usuario usuario)
        {
            var perfil = ValidarUsuario(usuario.UserName, usuario.Password);

            if (perfil != null)
            {
                await AutenticarUsuario(perfil);
                return RedirectToAction("Index", "TicketDeVentas");
            }
            else
            {
                ViewBag.Validacion = "No se puede registrar, ya existe";
                return RedirectToAction(nameof(Index));


            }
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task AutenticarUsuario(Usuario perfil)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, perfil.UserName)
            };

            string[] usuarioRol = { perfil.Role };

            foreach (string Role in usuarioRol)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        [HttpPost]
        public async Task<IActionResult> ValidarCredenciales(string UserName, string Password)
        {
            var perfil = ValidarUsuario(UserName, Password);

            if (perfil != null)
            {
                
                return BadRequest("Credenciales incorrectas");
            }
            else
            {
                await AutenticarUsuario(perfil);
                return Ok(); // Cambia según tu necesidad
            }
        }
    }
}
