using Awesome.Enums;

namespace Awesome.Entities;

public class TodoItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public ETodoItemStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public int TodoListId { get; set; }

    public TodoList TodoList { get; set; }
}
