using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly SupabaseDbContext _db;

    public HealthController(SupabaseDbContext db)
    {
        _db = db;
    }

    [HttpGet("db")]
    public async Task<IActionResult> Db()
    {
        var canConnect = await _db.Database.CanConnectAsync();
        return Ok(new { database = canConnect });
    }
}