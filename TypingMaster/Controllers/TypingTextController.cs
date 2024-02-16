﻿using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Functions.TypingTexts.Queries.GetTypingTextsByDifficultyLevel;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class TypingTextController(IMediator mediator) : ControllerBase
{
    [HttpGet("{difficultyLevel:int}")]
    public async Task<ActionResult<IEnumerable<TypingTextDto>>> GetTextsByDifficultyLevel([Required] int difficultyLevel)
    {
        var reponse = await mediator.Send(new GetTypingTextsByDifficultyLevelQuery((uint)difficultyLevel));
        return HandleResponse<IEnumerable<TypingTextDto>, GetTypingTextsByDifficultyLevelResponse>(reponse);
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