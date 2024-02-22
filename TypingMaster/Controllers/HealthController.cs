using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypingMaster.Database;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class HealthController(IDbContextFactory<TestDbContext> dbFactory) : Controller
{
    [HttpGet]
    public async Task<ActionResult> CheckHealth(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status500InternalServerError, $"{DateTimeOffset.Now} ❌ - Request cancelled");
        
        try
        {
            
            await using var dbContext = await dbFactory.CreateDbContextAsync(cancellationToken);

            var culture = dbContext.Cultures.ToList();
            
            var result = await dbContext.HealthCheck(cancellationToken);
            
            if(result.IsOk)
                return Ok($"{DateTimeOffset.Now} 👍 - Connection healthy");
            
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                $"{DateTimeOffset.Now} 👎 - {result.Error}");
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{DateTimeOffset.Now} ❌ - Request cancelled");
        }
    }
}