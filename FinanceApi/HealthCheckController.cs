using FinanceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FinanceApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class HealthController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("ok");
    }
    [HttpGet("mongo")]
    public IActionResult GetMongoConnection(IOptions<FinanceDatabaseSettings> financeDatabaseSettings)
    {
      try
      {
        var mongoClient = new MongoClient(financeDatabaseSettings.Value.ConnectionString);
        var ping = mongoClient.GetDatabase(financeDatabaseSettings.Value.DatabaseName).RunCommand((Command<BsonDocument>)"{ping:1}");

        return Ok("ok");

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to MongoDB.");
      }
    }
  }

}