using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAuth.Mvc.Authorization;
using NetCoreAuth.Mvc.Models;

namespace NetCoreAuth.Mvc.Controllers
{
    [Authorize]
    public class TodoItemsController : Controller
    {
        private readonly TodoStore _todoStore;
        private readonly IAuthorizationService _authorizationService;

        public TodoItemsController(TodoStore todoStore, IAuthorizationService authorizationService)
        {
            _todoStore = todoStore;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return  View(_todoStore.GetAll()); 
        }

        public ActionResult Create()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        public ActionResult Create(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _todoStore.Save(todoItem);
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todoItem = _todoStore.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            if (await _authorizationService.AuthorizeAsync(User, todoItem, new OwnerRequirement()))
            {
                return View(todoItem);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoItem todoItem)
        {
            if (! await _authorizationService.AuthorizeAsync(User, todoItem, new OwnerRequirement()))
            {
                return Forbid();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _todoStore.Save(todoItem);

                    return RedirectToAction("Index");
                }
                return View(todoItem);
            }
        }
    }
}