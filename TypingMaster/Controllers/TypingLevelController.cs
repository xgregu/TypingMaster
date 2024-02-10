using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Functions.TypingLevels.Queries.GetAllTypingLevels;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class TypingLevelController : ControllerBase
{
    private readonly IMediator _mediator;

    public TypingLevelController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypingLevelDto>>> GetAllLevels()
    {
        var reponse = await _mediator.Send(new GetAllTypingLevelsQuery());
        return HandleResponse<IEnumerable<TypingLevelDto>, GetAllTypingLevelsResponse>(reponse);
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