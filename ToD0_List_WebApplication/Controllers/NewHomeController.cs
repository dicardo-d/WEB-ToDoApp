using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToD0_List_WebApplication.Models;

namespace ToD0_List_WebApplication
{
    public class NewHomeController : Controller
    {
        private readonly ToDoContext _context;

        public NewHomeController(ToDoContext context)
        {
            _context = context;
        }

        // GET: NewHome
        public async Task<IActionResult> Index()
        {
            ViewData["ToDos"] = await _context.ToDo.ToListAsync();
            return _context.ToDo != null ? 
                          View() :
                          Problem("Entity set 'ToDoContext.ToDo'  is null.");
        }

        // GET: NewHome/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDo == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDo
                .FirstOrDefaultAsync(m => m.TodoModelId == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // GET: NewHome/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewHome/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoModelId,IsCompleted,CompletionDate,Description")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: NewHome/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDo == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDo.FindAsync(id);
            if (toDoModel == null)
            {
                return NotFound();
            }
            return View(toDoModel);
        }

        // POST: NewHome/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TodoModelId,IsCompleted,CompletionDate,Description")] ToDoModel toDoModel)
        {

            List<ToDoModel> todos = await _context.ToDo.ToListAsync();
            ToDoModel model = todos.First(model => model.TodoModelId == toDoModel.TodoModelId);

            if (model != null)
            {
                model.IsCompleted = toDoModel.IsCompleted;
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoModelExists(toDoModel.TodoModelId))
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
            return RedirectToAction(nameof(Index));
        }

        // GET: NewHome/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDo == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDo
                .FirstOrDefaultAsync(m => m.TodoModelId == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // POST: NewHome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDo == null)
            {
                return Problem("Entity set 'ToDoContext.ToDo'  is null.");
            }
            var toDoModel = await _context.ToDo.FindAsync(id);
            if (toDoModel != null)
            {
                _context.ToDo.Remove(toDoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoModelExists(int id)
        {
          return (_context.ToDo?.Any(e => e.TodoModelId == id)).GetValueOrDefault();
        }
    }
}
