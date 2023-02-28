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

    var userId = HttpContext.Items["userId"] as string;
    if (userId == null)
    {
      return Unauthorized("Something went wrong userId is null");
    }
    var liabilities = await _liabilitiesService.GetLiabilities(userId);

    return Ok(liabilities);
  }


  [Authorize]
  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Liability>> Get(string id)
  {
    var userId = HttpContext.Items["userId"] as string;
    if (userId == null)
    {
      return Unauthorized("Something went wrong userId is null");
    }

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



  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Liability>> Create(Liability liability)
  {
    var userId = HttpContext.Items["userId"] as string;
    if (userId == null)
    {
      return Unauthorized("Something went wrong userId is null");
    }

    liability.User = userId;
    if (liability.Recurring)
    {
      liability.PaymentStart = DateTime.Now;
      liability.Active = true;
    }


    await _liabilitiesService.CreateLiability(liability);
    return CreatedAtAction(nameof(Get), new { id = liability.Id }, liability);
  }



  [Authorize]
  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Liability liability)
  {
    if (liability.Id == null || liability.Id != id)
    {
      return BadRequest("Liability Id is null or does not match");
    }
    var liabilityFromDb = await _liabilitiesService.GetLiability(liability.Id);
    if (liabilityFromDb == null)
    {
      return NotFound("Not found or no longer exist");
    }

    var userId = HttpContext.Items["userId"] as string;
    if (userId == null)
    {
      return Unauthorized("Something went wrong userId is null");
    }

    if (liabilityFromDb.User != userId)
    {
      return Unauthorized("User is not authorized to access this resource");
    }


    var updated = await _liabilitiesService.UpdateLiability(liability, liability.Id);
    return Ok(updated);
  }


  [Authorize]
  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var userId = HttpContext.Items["userId"] as string;
    if (userId == null)
    {
      return Unauthorized("Something went wrong userId is null");
    }

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

  //endpoint deleteall liabilities/deleteall
  [Authorize]
  [HttpDelete("deleteall")]
  public async Task<IActionResult> DeleteAll()
  {
    var userId = HttpContext.Items["userId"] as string;
    if (userId == null)
    {
      return Unauthorized("Something went wrong userId is null");
    }
    await _liabilitiesService.RemoveAllLiabilities(userId);

    return NoContent();

  }



}


