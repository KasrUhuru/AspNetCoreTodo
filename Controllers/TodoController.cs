using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {  
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // AddItem block
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest(new{error="Could not add item."});
            }

            return RedirectToAction("Index");
        }

        // MarkDone block
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id==Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done");
            }

            return RedirectToAction("Index");
        }

        // Index block
        public async Task<IActionResult> Index()
        {
            // Get todo items from database
            var items = await _todoItemService.GetIncompleteItemsAsync();

            // Put items into model
            var model = new TodoViewModel()
            {
                Items = items
            };

            // Render view using the model
            return View(model);
        }
    }
}