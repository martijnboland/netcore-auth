using System.ComponentModel.DataAnnotations;

namespace NetCoreAuth.Mvc.Models
{
  public class TodoItem
    {
        public int Id { get; set; }
        public int Order { get; set; }
        [Required]
        public string Title { get; set; }
        public bool Completed { get; set; }
        public string Owner { get; set;}

        public override bool Equals(object obj)
        {
           var todoItem = obj as TodoItem;
           return (todoItem != null) && (Id == todoItem.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}