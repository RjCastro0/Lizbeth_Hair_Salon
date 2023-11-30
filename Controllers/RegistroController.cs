using Lisbeth_Hair_Salon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lisbeth_Hair_Salon.Controllers
{
    public class RegistroController : Controller
    {
        private readonly HairSalonContext _context;

        public RegistroController(HairSalonContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el nombre de usuario ya está en uso
                if (await _context.Usuarios.AnyAsync(u => u.UserName == usuario.UserName))
                {
                    ModelState.AddModelError("UserName", "El nombre de usuario ya está en uso.");
                    return View("Index", usuario);
                }

                // Agregar lógica adicional de validación si es necesario

                // Agregar el nuevo usuario a la base de datos
                _context.Add(usuario);
                await _context.SaveChangesAsync();


                // Redirigir al usuario a la página de inicio o a donde sea apropiado
                return RedirectToAction("Index", "Home");
            }

            // Si el modelo no es válido, regresar al formulario con los errores
            return View("Index", usuario);
        }
    }
}
