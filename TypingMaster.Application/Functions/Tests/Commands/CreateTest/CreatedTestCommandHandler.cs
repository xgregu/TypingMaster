using FluentValidation.Results;
using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Functions.Tests.Commands.CreateTest;

public class CreatedTestCommandHandler(ITypingTestStore typingTestStore, ITestStatisticsCalculator statisticsCalculator)
    : IRequestHandler<CreatedTestCommand, CreatedTestCommandResponse>
{
    public async Task<CreatedTestCommandResponse> Handle(CreatedTestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var validatorResult = await ValidateRequest(request, cancellationToken);

            if (!validatorResult.IsValid)
                return CreatedTestCommandResponse.Failure(ResponseStatus.Failed,
                    string.Join(", ", validatorResult.Errors));

            var testStatistisc = await statisticsCalculator.GetTestStatistic(request.CreateTestRequest);
            var testEntity = new TypingTestEntity
            {
                ExecutorName = request.CreateTestRequest.ExecutorName,
                StartTime = request.CreateTestRequest.StartTime,
                EndTime = request.CreateTestRequest.EndTime,
                TextId = request.CreateTestRequest.TextId,
                Statistics = testStatistisc
            };

            var createdTest = await typingTestStore.AddAsync(testEntity);
            return CreatedTestCommandResponse.Success(createdTest.ToDto());
        }
        catch (Exception e)
        {
            return CreatedTestCommandResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }

    private async Task<ValidationResult> ValidateRequest(CreatedTestCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreatedTestCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);
        return validatorResult;
    }
}