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
[Route("api/[controller]")]
public class TestController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypingTestDto>>> GetAllTests()
    {
        var reponse = await mediator.Send(new GetAllTestsQuery());
        return HandleResponse<IEnumerable<TypingTestDto>, GetAllTestResponse>(reponse);
    }

    [HttpGet("{testId:long}")]
    public async Task<ActionResult<TypingTestDto>> GetTest([Required] long testId)
    {
        var response = await mediator.Send(new GetTestQuery(testId));
        return HandleResponse<TypingTestDto, GetTestResponse>(response);
    }
    
    [HttpGet("{testId:long}/ranking")]
    public async Task<ActionResult<long>> GetTestRanking([Required] long testId)
    {
        var response = await mediator.Send(new GetTestRankingQuery(testId));
        return HandleResponse<long, GetTestRankingResponse>(response);
    }

    [HttpPost]
    public async Task<ActionResult<TypingTestDto>> Test([Required] CreateTestRequest createTest)
    {
        var response = await mediator.Send(new CreatedTestCommand(createTest));
        return HandleResponse<TypingTestDto, CreatedTestCommandResponse>(response);
    }
    
    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<TypingTestDto>>> GetPagedTests([FromQuery] long startIndex, [FromQuery] long count)
    {
        var response = await mediator.Send(new GetPagedTestsQuery(startIndex, count));
        return HandleResponse<IEnumerable<TypingTestDto>, GetPagedTestsResponse>(response);
    }
    
    [HttpGet("count")]
    public async Task<ActionResult<long>> GetPagedTests()
    {
        var response = await mediator.Send(new GetCountTestsQuery());
        return HandleResponse<long, GetCountTestsResponse>(response);
    }
    
    private ActionResult<T1> HandleResponse<T1, T2>(T2 response) where T2: Response<T1>
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