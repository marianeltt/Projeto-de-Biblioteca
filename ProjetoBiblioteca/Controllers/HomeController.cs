using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoBiblioteca.Models;
using ProjetoBiblioteca.Data;

namespace ProjetoBiblioteca.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.TotalUsuarios =
            _context.Usuarios.Count();

        ViewBag.TotalLivros =
            _context.Livros.Count();

        ViewBag.TotalDisponiveis =
            _context.Livros.Sum(l => l.QuantidadeEstoque);

        ViewBag.TotalEmprestimos =
            _context.Emprestimos.Count(e => e.DataRealDevolucao == null);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel
            {
                RequestId =
                    Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
    }
}