using FinanceApi.Models;
using FinanceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly UsersService _userService;

  public UsersController(UsersService userService) =>
    _userService = userService;


  [HttpGet]
  public async Task<List<User>> Get() =>
    await _userService.GetUsersAsync();

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<User>> Get(string id)
  {
    var user = await _userService.GetUserAsync(id);
    if (user == null)
    {
      return NotFound();
    }
    return user;
  }


  [HttpPost]
  public async Task<ActionResult<User>> Create(User user)
  {
    await _userService.CreateUserAsync(user);
    return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
  }
}