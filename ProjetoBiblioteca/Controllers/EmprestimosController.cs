using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBiblioteca.Data;
using ProjetoBiblioteca.Models;
using ProjetoBiblioteca.ViewModels;

namespace ProjetoBiblioteca.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly AppDbContext _context;

        public EmprestimosController(AppDbContext context)
        {
            _context = context;
        }

        // HISTÓRICO
        public async Task<IActionResult> Index()
        {
            var emprestimos = await _context.Emprestimos
                .Include(e => e.Usuario)
                .Include(e => e.Livro)
                .OrderByDescending(e => e.DataEmprestimo)
                .ToListAsync();

            return View(emprestimos);
        }

        // DETALHES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var emprestimo = await _context.Emprestimos
                .Include(e => e.Usuario)
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound();

            return View(emprestimo);
        }

        // GET CREATE
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await _context.Usuarios
                .OrderBy(u => u.NomeCompleto)
                .ToListAsync();

            ViewBag.Livros = await _context.Livros
                .OrderBy(l => l.NomeLivro)
                .ToListAsync();

            var vm = new EmprestimoFormVM
            {
                DataEmprestimo = DateTime.Today,
                DataPrevistaDevolucao = DateTime.Today.AddDays(7)
            };

            return View(vm);
        }

        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmprestimoFormVM vm)
        {
            ViewBag.Usuarios = await _context.Usuarios
                .OrderBy(u => u.NomeCompleto)
                .ToListAsync();

            ViewBag.Livros = await _context.Livros
                .OrderBy(l => l.NomeLivro)
                .ToListAsync();

            if (!ModelState.IsValid)
                return View(vm);

            var usuario = await _context.Usuarios.FindAsync(vm.UsuarioId);
            var livro = await _context.Livros.FindAsync(vm.LivroId);

            if (usuario == null || livro == null)
                return NotFound();

            // sem estoque
            if (livro.QuantidadeEstoque <= 0)
            {
                ModelState.AddModelError("",
                    "Livro indisponível em estoque.");

                return View(vm);
            }

            // calcula idade
            var idade = DateTime.Today.Year - usuario.DataNascimento.Year;

            if (usuario.DataNascimento.Date >
                DateTime.Today.AddYears(-idade))
            {
                idade--;
            }

            // regra 18+
            if (livro.FaixaEtariaPermitida >= 18 && idade < 18)
            {
                ModelState.AddModelError("",
                    "Usuário menor de idade não pode pegar livro 18+.");

                return View(vm);
            }

            // baixa estoque
            livro.QuantidadeEstoque--;

            // cria empréstimo
            var emprestimo = new Emprestimo
            {
                UsuarioId = vm.UsuarioId,
                LivroId = vm.LivroId,
                DataEmprestimo = vm.DataEmprestimo,
                DataPrevistaDevolucao = vm.DataPrevistaDevolucao,
                DataRealDevolucao = null,
                Multa = 0,
                Status = "Emprestado"
            };

            _context.Emprestimos.Add(emprestimo);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DEVOLUÇÃO
        public async Task<IActionResult> Devolver(int id)
        {
            var emprestimo = await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound();

            if (emprestimo.Status == "Devolvido")
                return RedirectToAction(nameof(Index));

            emprestimo.DataRealDevolucao = DateTime.Today;

            // aumenta estoque
            emprestimo.Livro.QuantidadeEstoque++;

            // atraso
            if (DateTime.Today > emprestimo.DataPrevistaDevolucao)
            {
                int diasAtraso =
                    (DateTime.Today - emprestimo.DataPrevistaDevolucao).Days;

                emprestimo.Multa = diasAtraso * 2;
                emprestimo.Status = "Atrasado";
            }
            else
            {
                emprestimo.Status = "Devolvido";
                emprestimo.Multa = 0;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var emprestimo = await _context.Emprestimos
                .Include(e => e.Usuario)
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound();

            return View(emprestimo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo =
                await _context.Emprestimos.FindAsync(id);

            if (emprestimo != null)
            {
                _context.Emprestimos.Remove(emprestimo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}