using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoBiblioteca.Data;
using ProjetoBiblioteca.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoBiblioteca.Controllers
{
    public class LivrosController : Controller
    {
        
        private readonly AppDbContext _context;

        public LivrosController(AppDbContext context)
        {
            _context = context;
        }
        
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var usuarioId = context.HttpContext
                .Session
                .GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                context.Result =
                    RedirectToAction("Index", "Login");

                return;
            }

            var usuario = _context.Usuarios
                .FirstOrDefault(u =>
                    u.Id == usuarioId);

            if (usuario == null || !usuario.Status)
            {
                context.HttpContext
                    .Session
                    .Clear();

                context.Result =
                    RedirectToAction("Index", "Login");

                return;
            }

            base.OnActionExecuting(context);
        }
        
        // GET: Livros
        public async Task<IActionResult> Index(
            string busca,
            int pagina = 1)
        {
            int itensPorPagina = 10;

            var livros = _context.Livros.AsQueryable();

            // busca
            if (!string.IsNullOrWhiteSpace(busca))
            {
                livros = livros.Where(l =>
                    l.NomeLivro.Contains(busca));
            }

            // total de registros
            int totalLivros =
                await livros.CountAsync();

            // calcula total de páginas
            int totalPaginas =
                (int)Math.Ceiling(
                    totalLivros / (double)itensPorPagina
                );

            // pega só os itens da página atual
            var lista = await livros
                .OrderBy(l => l.NomeLivro)
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToListAsync();

            // envia pra View
            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Busca = busca;

            return View(lista);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeLivro,Autor,QuantidadeEstoque,FaixaEtariaPermitida,Categoria,AnoPublicacao")] Livro livro)
        {
            if (livro.QuantidadeEstoque < 0)
            {
                ModelState.AddModelError("QuantidadeEstoque",
                    "A quantidade em estoque não pode ser negativa.");
            }
            
            if (!ModelState.IsValid)
            {
                return View(livro);
            }

            _context.Add(livro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeLivro,Autor,QuantidadeEstoque,FaixaEtariaPermitida,Categoria,AnoPublicacao")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }
            
            if (livro.QuantidadeEstoque < 0)
            {
                ModelState.AddModelError("QuantidadeEstoque",
                    "A quantidade em estoque não pode ser negativa.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
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
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            var possuiEmprestimos = await _context.Emprestimos
                .AnyAsync(e => e.LivroId == id);

            if (possuiEmprestimos)
            {
                ModelState.AddModelError("",
                    "Não é possível excluir um livro com empréstimos registrados.");

                return View(livro);
            }

            if (livro != null)
            {
                _context.Livros.Remove(livro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
