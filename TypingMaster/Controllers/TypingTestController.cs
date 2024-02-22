using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Functions.Tests.Commands.CreateTest;
using TypingMaster.Application.Functions.Tests.Queries.GetAllTests;
using TypingMaster.Application.Functions.Tests.Queries.GetCountTestsQuery;
using TypingMaster.Application.Functions.Tests.Queries.GetPagedTestsQuery;
using TypingMaster.Application.Functions.Tests.Queries.GetTest;
using TypingMaster.Application.Functions.Tests.Queries.GetTestRanking;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class TypingTestController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypingTestDto>>> GetAllTests()
    {
        var response = await mediator.Send(new GetAllTestsQuery());
        return HandleResponse<IEnumerable<TypingTestDto>, GetAllTestResponse>(response);
    }

    [HttpGet("{testId:long}")]
    public async Task<ActionResult<TypingTestDto>> GetTest([Required] long testId)
    {
        var response = await mediator.Send(new GetTestQuery(testId));
        return HandleResponse<TypingTestDto, GetTestResponse>(response);
    }

    [HttpGet("{testId:long}/ranking")]
    public async Task<ActionResult<long>> GetTestRanking([Required] long testId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new GetTestRankingQuery(testId), cancellationToken);
            return HandleResponse<long, GetTestRankingResponse>(response);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"{DateTimeOffset.Now} ❌ - Request cancelled");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TypingTestDto>> Test([Required] CreateTestRequest createTest)
    {
        var response = await mediator.Send(new CreatedTestCommand(createTest));
        return HandleResponse<TypingTestDto, CreatedTestCommandResponse>(response);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedTestResponse>> GetPagedTests([FromQuery] long startIndex,
        [FromQuery] long count, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new GetPagedTestsQuery(startIndex, count), cancellationToken);
            return HandleResponse<PagedTestResponse, GetPagedTestsResponse>(response);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"{DateTimeOffset.Now} ❌ - Request cancelled");
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult<long>> GetCountTests()
    {
        var response = await mediator.Send(new GetCountTestsQuery());
        return HandleResponse<long, GetCountTestsResponse>(response);
    }

    private ActionResult<T1> HandleResponse<T1, T2>(T2 response) where T2 : Response<T1>
    {
        return response.Status switch
        {
            ResponseStatus.Success => Ok(response),
            ResponseStatus.NotFound => NotFound(),
            ResponseStatus.Failed => StatusCode(StatusCodes.Status500InternalServerError, response.Message),
            ResponseStatus.Error => StatusCode(StatusCodes.Status500InternalServerError, response.Message),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}