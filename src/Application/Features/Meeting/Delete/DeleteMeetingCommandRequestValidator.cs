using FluentValidation;

namespace Application.Features.Meeting.Delete;

public class DeleteMeetingCommandRequestValidator : AbstractValidator<DeleteMeetingCommandRequest>
{
    public DeleteMeetingCommandRequestValidator()
    {
        
    }   
}