using FinanceApi.Models;
using FinanceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LiabilitiesController : ControllerBase
{
  private readonly LiabilitiesService _liabilitiesService;

  public LiabilitiesController(LiabilitiesService liabilitiesService) =>
    _liabilitiesService = liabilitiesService;

  [HttpGet]
  public async Task<List<Liability>> Get() =>
    await _liabilitiesService.GetLiabilitiesAsync();

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Liability>> Get(string id)
  {
    var liability = await _liabilitiesService.GetLiabilityAsync(id);
    if (liability == null)
    {
      return NotFound();
    }
    return liability;
  }

  [HttpGet("user/{id:length(24)}")]
  public async Task<ActionResult<List<Liability>>> GetByUser(string id)
  {
    var liabilities = await _liabilitiesService.GetUserLiabilitiesAsync(id);
    if (liabilities == null)
    {
      return NotFound();
    }
    return liabilities;
  }



  [HttpPost]
  public async Task<ActionResult<Liability>> Create(Liability liability)
  {
    if (liability.User == null)
    {
      return BadRequest("User is required");
    }

    await _liabilitiesService.CreateLiabilityAsync(liability);
    return CreatedAtAction(nameof(Get), new { id = liability.Id }, liability);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Liability liability)
  {
    var liabilityFromDb = await _liabilitiesService.GetLiabilityAsync(id);
    if (liabilityFromDb == null)
    {
      return NotFound();
    }
    await _liabilitiesService.UpdateLiabilityAsync(id, liability);
    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var liabilityFromDb = await _liabilitiesService.GetLiabilityAsync(id);
    if (liabilityFromDb == null)
    {
      return NotFound();
    }
    await _liabilitiesService.RemoveLiabilityAsync(id);
    return NoContent();
  }
}