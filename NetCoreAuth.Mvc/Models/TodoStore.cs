using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace NetCoreAuth.Mvc.Models
{
    /// <summary>
    /// Simple non-thread-safe in-memory store for todo items. Strictly for demo purposes.
    /// </summary>
    public class TodoStore
    {
        public static List<TodoItem> TodoItems = new List<TodoItem>();
        public static int MaxId = 0;
        private readonly IPrincipal _principal;

        public TodoStore(IPrincipal principal)
        {
            _principal = principal;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.OrderByDescending(t => t.Order);
        }

        public TodoItem Get(int id)
        {
            return TodoItems.Where(t => t.Id == id).FirstOrDefault();
        }

        public TodoItem Save(TodoItem item)
        {
            item.Owner = _principal.Identity.Name;
            if (item.Id == 0)
            {
                item.Id = ++MaxId;
                item.Order = item.Id;
            }

            var index = TodoItems.IndexOf(item);
            if (index != -1)
            {
                TodoItems[index] = item;
            }
            else
            {
                TodoItems.Add(item);
            }

            return item;
        }

        public void Delete()
        {
            TodoItems.Clear();
        }

        public void Delete(int id)
        {
            TodoItems.RemoveAll(t => t.Id == id);
        }
    }    
}
