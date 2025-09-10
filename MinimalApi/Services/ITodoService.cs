using MinimalApi.Models;

namespace MinimalApi.Services;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll();
    TodoItem? GetById(int id);
    TodoItem Create(string title, bool isCompleted);
    bool Delete(int id);
}