using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTask.Models;

namespace AppTask.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly DbTasksContext _context;

        public FuncionariosController(DbTasksContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
              return _context.Funcionarios != null ? 
                          View(await _context.Funcionarios.ToListAsync()) :
                          Problem("Entity set 'DbTasksContext.Funcionarios'  is null.");
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Cargo")] Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Cargo")] Funcionarios funcionarios)
        {
            if (id != funcionarios.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionariosExists(funcionarios.Codigo))
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
            return View(funcionarios);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'DbTasksContext.Funcionarios'  is null.");
            }
            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios != null)
            {
                _context.Funcionarios.Remove(funcionarios);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionariosExists(int id)
        {
          return (_context.Funcionarios?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
