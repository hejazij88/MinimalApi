namespace MinimalApi.Dtos;

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }


    public TodoDto() { }
    public TodoDto(Models.TodoItem src)
    {
        Id = src.Id;
        Title = src.Title;
        IsCompleted = src.IsCompleted;
        CreatedAt = src.CreatedAt;
    }
}