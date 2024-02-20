using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Functions.TypingLevels.Queries.GetAllTypingLevels;
using TypingMaster.Application.Functions.TypingLevels.Queries.GetTypingLevelName;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class TypingLevelController(IMediator mediator) : ControllerBase
{
    [HttpGet("{difficultyLevel:int}")]
    public async Task<ActionResult<string>> GetAllLevels([Required] uint difficultyLevel,
        [Required] [FromQuery] string cultureCode)
    {
        var response = await mediator.Send(new GetTypingLevelNameQuery(difficultyLevel, cultureCode));
        return HandleResponse<string, GetTypingLevelNameResponse>(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypingLevelDto>>> GetAllLevels([Required] [FromQuery] string cultureCode)
    {
        var response = await mediator.Send(new GetAllTypingLevelsQuery(cultureCode));
        return HandleResponse<IEnumerable<TypingLevelDto>, GetAllTypingLevelsResponse>(response);
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