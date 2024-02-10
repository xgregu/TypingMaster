using FluentValidation.Results;
using MediatR;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Functions.Tests.Commands.CreateTest;

public class CreatedTestCommandHandler : IRequestHandler<CreatedTestCommand, CreatedTestCommandResponse>
{
    private readonly ITypingTestStore _typingTestStore;
    private readonly ITestStatisticsCalculator _statisticsCalculator;

    public CreatedTestCommandHandler(ITypingTestStore typingTestStore, ITestStatisticsCalculator statisticsCalculator)
    {
        _typingTestStore = typingTestStore;
        _statisticsCalculator = statisticsCalculator;
    }
    
    public async Task<CreatedTestCommandResponse> Handle(CreatedTestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validatorResult = await ValidateRequest(request, cancellationToken);

            if (!validatorResult.IsValid)
                return CreatedTestCommandResponse.Failure(ResponseStatus.Failed, string.Join(", ", validatorResult.Errors));

            var testStatistisc = await _statisticsCalculator.GetTestStatistic(request.TestRequest);
            var testEntity = new TypingTestEntity
            {
                ExecutorName = request.TestRequest.ExecutorName,
                StartTime = request.TestRequest.StartTime,
                EndTime = request.TestRequest.EndTime,
                TextId = request.TestRequest.TextId,
                Statistics = testStatistisc,
            };

            var createdTest = await _typingTestStore.AddAsync(testEntity);
            return CreatedTestCommandResponse.Success(createdTest.ToDto());
        }
        catch (Exception e)
        {
            return CreatedTestCommandResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
    
    private async Task<ValidationResult> ValidateRequest(CreatedTestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatedTestCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);
        return validatorResult;
    }
}