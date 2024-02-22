using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Database;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class HealthController(IServiceScopeFactory scopeFactory) : Controller
{
    [HttpGet]
    public async Task<ActionResult> CheckHealth(CancellationToken cancellationToken)
    {
        try
        {
                   await using var scope = scopeFactory.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();
            if (cancellationToken.IsCancellationRequested)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{DateTimeOffset.Now} ❌ - Request cancelled");

            if (!await context.Database.CanConnectAsync(cancellationToken))
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