using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceApi.Models;
using FinanceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LiabilitiesController : ControllerBase
{
  private readonly LiabilitiesService _liabilitiesService;

  public LiabilitiesController(LiabilitiesService liabilitiesService) =>
    _liabilitiesService = liabilitiesService;


  [Authorize]
  [HttpGet]
  [Authorize]
  [HttpGet("liabilities")]
  public async Task<ActionResult<List<Liability>>> GetLiabilities()
  {
    if (!HttpContext.Items.TryGetValue("UserClaims", out var userClaims))
    {
      Console.WriteLine("U=====================");
      return Unauthorized("User is not authenticated or authorized");
    }
    else
    {
      var claims = userClaims as List<Claim>;

      Console.WriteLine($"Decoded token: {claims} ...");

      string? userId = claims.First(c => c.Type == "sub").Value;


      Console.WriteLine($"Decoded token: {userId} ...");

      var liabilities = await _liabilitiesService.GetUserLiabilities(userId);

      return Ok(liabilities);

    }


  }



  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Liability>> Get(string id)
  {
    var liability = await _liabilitiesService.GetLiability(id);
    if (liability == null)
    {
      return NotFound();
    }
    return liability;
  }

  [HttpGet("user/{id:length(24)}")]
  public async Task<ActionResult<List<Liability>>> GetByUser(string id)
  {
    var liabilities = await _liabilitiesService.GetUserLiabilities(id);
    if (liabilities == null)
    {
      return NotFound();
    }
    return liabilities;
  }


  //[Authorize]
  [HttpPost]
  public async Task<ActionResult<Liability>> Create(Liability liability)
  {
    await _liabilitiesService.CreateLiability(liability);
    return CreatedAtAction(nameof(Get), new { id = liability.Id }, liability);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Liability liability)
  {
    var liabilityFromDb = await _liabilitiesService.GetLiability(id);
    if (liabilityFromDb == null)
    {
      return NotFound();
    }
    await _liabilitiesService.UpdateLiability(id, liability);
    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var liabilityFromDb = await _liabilitiesService.GetLiability(id);
    if (liabilityFromDb == null)
    {
      return NotFound();
    }
    await _liabilitiesService.RemoveLiability(id);
    return NoContent();
  }
}