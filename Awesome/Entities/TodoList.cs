using Awesome.Enums;

namespace Awesome.Entities;

public class TodoList
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public ETodoListStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<TodoItem> TodoItems { get; set; }

    public TodoList()
    {
        TodoItems = new List<TodoItem>();
    }
}
