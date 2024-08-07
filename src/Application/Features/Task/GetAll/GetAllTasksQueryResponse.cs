﻿namespace Application.Features.Task.GetAll
{
    public class GetAllTasksQueryResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
