using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Dtos;

public class TodoCreateDto
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}