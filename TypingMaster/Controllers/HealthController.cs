using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingMaster.Database;

namespace TypingMaster.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class HealthController : Controller
{
    private readonly IServiceProvider _serviceProvider;

    public HealthController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet]
    public async Task<ActionResult> CheckHealth()
    {
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        if (!await context.Database.CanConnectAsync())
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"{DateTimeOffset.Now} 👎 - Database connection failed");
        
        return Ok($"{DateTimeOffset.Now} 👍 - Connection healthy");
    }
}