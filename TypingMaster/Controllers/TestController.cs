using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Functions.Tests.Commands.CreateTest;
using TypingMaster.Application.Functions.Tests.Queries.GetAllTests;
using TypingMaster.Application.Functions.Tests.Queries.GetTest;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypingTestDto>>> GetAllTests()
    {
        var reponse = await _mediator.Send(new GetAllTestsQuery());
        return HandleResponse<IEnumerable<TypingTestDto>, GetAllTestResponse>(reponse);
    }

    [HttpGet("{testId:long}")]
    public async Task<ActionResult<TypingTestDto>> GetTest([Required] long testId)
    {
        var response = await _mediator.Send(new GetTestQuery(testId));
        return HandleResponse<TypingTestDto, GetTestResponse>(response);
    }

    [HttpPost]
    public async Task<ActionResult<TypingTestDto>> Test([Required] TestRequest test)
    {
        var response = await _mediator.Send(new CreatedTestCommand(test));
        return HandleResponse<TypingTestDto, CreatedTestCommandResponse>(response);
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