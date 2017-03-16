using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAuth.Mvc.Models;

namespace NetCoreAuth.Mvc.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly TodoStore _todoStore;

        public TodoItemsController(TodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        public ActionResult Index()
        {
            return  View(_todoStore.GetAll()); 
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(TodoItem todoItem)
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