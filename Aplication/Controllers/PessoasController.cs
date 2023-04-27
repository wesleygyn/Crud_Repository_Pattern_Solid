using Data.Context;
using Data.Repositories.Abstractions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Controllers
{
    public class PessoasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IPessoaRepository _pessoaRepository;

        public PessoasController(IWebHostEnvironment environment, AppDbContext context, IPessoaRepository pessoaRepository)
        {
            _environment = environment;
            _context = context;
            _pessoaRepository = pessoaRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _pessoaRepository.Obter());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var pessoa = await _pessoaRepository.ObterPorIdAsync(id);

                if (pessoa == null)
                {
                    return NotFound();
                }

                return PartialView("_DetailsPessoa", pessoa);
            }
            catch
            {
                throw;
            }
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return PartialView("_CreatePessoa");
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid();
            await _pessoaRepository.AddAsync(pessoa);

            TempData["mensagemResultSucces"] = $"O cadastro de {pessoa.Nome} foi realizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!PessoaExists(id))
            {
                return NotFound();
            }

            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);

            return PartialView("_EditPessoa", pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pessoaRepository.Atualizar(pessoa);
                    TempData["mensagemResultSucces"] = $"O cadastro de {pessoa.Nome} foi editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
                    {
                        TempData["mensagemResultError"] = $"Não existe cadastro com o Id {pessoa.Id}.";
                        return NotFound();
                    }
                    else
                    {
                        TempData["mensagemResultError"] = $"Não foi possível editar o cadastro de {pessoa.Nome}.";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!PessoaExists(id))
            {
                return NotFound();
            }

            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);

            return PartialView("_DeletePessoa", pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }
            else
            {
                await _pessoaRepository.DeletarAsync(pessoa);
            }

            TempData["mensagemResultSucces"] = $"O cadastro de {pessoa.Nome} foi removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(Guid id)
        {
            var pessoa = _pessoaRepository.ObterPorIdAsync(id);
            if (pessoa == null)
            { 
                return false; 
            }

            return true;

        }
    }
}
