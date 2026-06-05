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
        
        // Exibe a tela de login.
        public IActionResult Index()
        {
            return View();
        }
        
        // Valida as credenciais do usuário e cria a sessão após autenticação.
        [HttpPost]
        public async Task<IActionResult> Index(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Email == vm.Email &&
                    u.Senha == vm.Senha &&
                    u.Status == true);

            // Verifica se existe um usuário ativo com os dados informados.
            if (usuario == null)
            {
                ViewBag.Erro = "Login inválido ou Usuário inativo";
                return View(vm);
            }

            // Armazena os dados do usuário na sessão.
            HttpContext.Session.SetInt32(
                "UsuarioId",
                usuario.Id);

            HttpContext.Session.SetString(
                "UsuarioNome",
                usuario.NomeCompleto);

            Console.WriteLine(
                "Salvou UsuarioId: " +
                HttpContext.Session.GetInt32("UsuarioId"));

            return RedirectToAction(
                "Index",
                "Home");
        }
        
        // Encerra a sessão do usuário.
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Index",
                "Login");
        }
    }
}