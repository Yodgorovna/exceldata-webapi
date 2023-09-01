using ExcelData.DataAccess.Utils;
using ExcelData.Service.Dtos.Users;
using ExcelData.Service.Interfaces.Users;
using ExcelData.Service.Validators.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace ExcelData.WebApi.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly int maxPageSize = 50;

    public UsersController(IUserService userService)
    {
        this._service = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByIdAsync(long userId)
        => Ok(await _service.GetByIdAsync(userId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UserCreateDto dto)
    {
        var validator = new UserCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
    {
        var validator = new UserUpdateValidator();
        var validationResult = validator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(userId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(long userId)
        => Ok(await _service.DeleteAsync(userId));

}
