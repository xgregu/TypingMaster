using FluentValidation;

namespace TypingMaster.Application.Functions.Tests.Commands.CreateTest;

internal class CreatedTestCommandValidator : AbstractValidator<CreatedTestCommand>
{
    public CreatedTestCommandValidator()
    {
        RuleFor(x => x.CreateTestRequest.ExecutorName)
            .NotEmpty().WithMessage("ExecutorName cannot be empty")
            .MinimumLength(3).WithMessage("ExecutorName must be at least 3 characters long");
        
        RuleFor(x => x.CreateTestRequest.StartTime)
            .NotEmpty().WithMessage("StartTime cannot be empty")
            .Must(BeAValidDate).WithMessage("StartTime must be a valid date")
            .LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("StartTime cannot be later than now")
            .LessThan(x => x.CreateTestRequest.EndTime).WithMessage("StartTime must be earlier than end time");
        
        RuleFor(x => x.CreateTestRequest.EndTime)
            .NotEmpty().WithMessage("EndTime cannot be empty")
            .Must(BeAValidDate).WithMessage("EndTime must be a valid date")
            .GreaterThanOrEqualTo(DateTimeOffset.Now).WithMessage("EndTime cannot be later than now")
            .GreaterThan(x => x.CreateTestRequest.StartTime).WithMessage("EndTime must be later than start time");
        
        RuleFor(x => x.CreateTestRequest.TotalClicks)
            .GreaterThan(0).WithMessage("TotalClicks must be greater than 0");
        
        RuleFor(x => x.CreateTestRequest.TextId)
            .GreaterThan(0).WithMessage("TextId must be greater than 0");
    }

    private bool BeAValidDate(DateTimeOffset date) => !date.Equals(default);
}
