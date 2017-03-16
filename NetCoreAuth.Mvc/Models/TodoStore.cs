using System.Collections.Generic;
using System.Linq;

namespace NetCoreAuth.Mvc.Models
{
    /// <summary>
    /// Simple non-thread-safe in-memory store for todo items. Strictly for demo purposes.
    /// </summary>
    public class TodoStore
    {
        public static List<TodoItem> TodoItems = new List<TodoItem>();
        public static int MaxId = 0;

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
            if (item.Id == 0)
            {
                // New
                item.Id = ++MaxId;
                item.Order = item.Id;
            }

            var index = TodoItems.IndexOf(item);
            if (index != -1)
            {
                // Replace existing
                TodoItems[index] = item;
            }
            else
            {
                // Add new item
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
