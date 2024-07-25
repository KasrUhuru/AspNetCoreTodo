using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {  
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;


        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
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
        [ValidateAntiForgeryToken]
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

    }

    public class ApplicationUser
    {
    }
}