namespace Mvc.Models.Task;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public List<Guid> UserIds { get; set; } = new List<Guid>();
}