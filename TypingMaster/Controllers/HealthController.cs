using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypingMaster.Database;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class HealthController(IDbContextFactory<TestDbContext> dbFactory) : Controller
{
    [HttpGet]
    public async Task<ActionResult> CheckHealth(CancellationToken cancellationToken)
    {
        try
        {
            if (cancellationToken.IsCancellationRequested)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{DateTimeOffset.Now} ❌ - Request cancelled");
            
            await using var dbContext = await dbFactory.CreateDbContextAsync(cancellationToken);
            if (!await dbContext.Database.CanConnectAsync(cancellationToken))
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    $"{DateTimeOffset.Now} 👎 - Database connection failed");

            return Ok($"{DateTimeOffset.Now} 👍 - Connection healthy");
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{DateTimeOffset.Now} ❌ - Request cancelled");
        }
    }
}