using MinimalApi.Models;

namespace MinimalApi.Services;

public class TodoService:ITodoService
{
    private readonly List<TodoItem> _items = new();
    private int _next = 1;


    public IEnumerable<TodoItem> GetAll() => _items;


    public TodoItem? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);


    public TodoItem Create(string title, bool isCompleted)
    {
        var t = new TodoItem { Id = _next++, Title = title, IsCompleted = isCompleted, CreatedAt = DateTime.UtcNow };
        _items.Add(t);
        return t;
    }


    public bool Delete(int id)
    {
        var t = GetById(id);
        if (t is null) return false;
        _items.Remove(t);
        return true;
    }
}