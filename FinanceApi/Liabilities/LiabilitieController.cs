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
  public async Task<ActionResult<List<Liability>>> GetLiabilities()
  {

    if (!HttpContext.Items.TryGetValue("UserClaims", out var userClaims))
    {
      return Unauthorized("User is not authenticated or authorized");
    }

    var claims = userClaims as List<Claim>;
    string? userId = claims.First(c => c.Type == "sub").Value;


    Console.WriteLine($"Decoded token: {userId} ...");

    var liabilities = await _liabilitiesService.GetLiabilities(userId);

    return Ok(liabilities);



  }



  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Liability>> Get(string id)
  {
    if (!HttpContext.Items.TryGetValue("UserClaims", out var userClaims))
    {
      return Unauthorized("User is not authenticated or authorized");
    }
    else if (id == null)
    {
      return BadRequest("Id is null");
    }

    var claims = userClaims as List<Claim>;
    string? userId = claims.First(c => c.Type == "sub").Value;

    var liability = await _liabilitiesService.GetLiability(id);
    if (liability == null)
    {
      return NotFound();
    }
    else if (liability.User != userId)
    {
      return Unauthorized("User is not authorized to access this resource");
    }


    return liability;
  }



  //[Authorize]
  [HttpPost]
  public async Task<ActionResult<Liability>> Create(Liability liability)
  {
    await _liabilitiesService.CreateLiability(liability);
    return CreatedAtAction(nameof(Get), new { id = liability.Id }, liability);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(Liability liability)
  {
    if (liability.Id == null)
    {
      return BadRequest();
    }
    var liabilityFromDb = await _liabilitiesService.GetLiability(liability.Id);
    if (liabilityFromDb == null)
    {
      return NotFound("Not found or no longer exist");
    }

    if (!HttpContext.Items.TryGetValue("UserClaims", out var userClaims))
    {
      return Unauthorized("User is not authenticated or authorized");
    }

    var claims = userClaims as List<Claim>;
    string? userId = claims.First(c => c.Type == "sub").Value;
    if (liabilityFromDb.User != userId)
    {
      return Unauthorized("User is not authorized to access this resource");
    }

    // update function here

    await _liabilitiesService.UpdateLiability(liability);
    return NoContent();
  }



  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    if (!HttpContext.Items.TryGetValue("UserClaims", out var userClaims))
    {
      return Unauthorized("User is not authenticated or authorized");
    }
    if (id == null)
    {
      return BadRequest("Id is null");
    }

    var claims = userClaims as List<Claim>;
    string? userId = claims.First(c => c.Type == "sub").Value;

    var liability = await _liabilitiesService.GetLiability(id);
    if (liability == null)
    {
      return NotFound();
    }
    if (liability.User != userId)
    {
      return Unauthorized("User is not authorized to access this resource");
    }

    await _liabilitiesService.RemoveLiability(id);
    return NoContent();
  }
}