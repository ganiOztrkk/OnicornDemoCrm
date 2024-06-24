namespace Application.Features.Task.GetByUserId
{
    public class GetTaskByUserIdQueryResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
    }
}
