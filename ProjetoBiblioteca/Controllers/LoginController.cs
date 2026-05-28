using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBiblioteca.Data;
using ProjetoBiblioteca.ViewModels;

namespace ProjetoBiblioteca.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Email == vm.Email &&
                    u.Senha == vm.Senha);

            if (usuario == null)
            {
                ViewBag.Erro = "Login inválido";
                return View(vm);
            }

            // salva login na sessão
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNome", usuario.NomeCompleto);

            return RedirectToAction("Index", "Home");
        }
    }
}