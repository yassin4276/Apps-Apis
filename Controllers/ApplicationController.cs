using Microsoft.AspNetCore.Mvc;
using Apps_Apis.Dtos.Api;
using Apps_Apis.Dtos.AppDtos;
using Apps_Apis.Services.Interfaces;

namespace Apps_Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    /// <summary>
    /// Get all applications
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ApplicationDto>>>> GetAll()
    {
        var (isSuccess, data, errorMessage) = await _applicationService.GetAllApplicationsAsync();

        if (!isSuccess)
            return Ok(ApiResponse<List<ApplicationDto>>.Fail(errorMessage ?? "Failed to retrieve applications"));

        return Ok(ApiResponse<List<ApplicationDto>>.Ok(data ?? new List<ApplicationDto>()));
    }

    /// <summary>
    /// Get application by ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ApplicationDto>>> GetById(int id)
    {
        var (isSuccess, data, errorMessage) = await _applicationService.GetApplicationByIdAsync(id);

        if (!isSuccess)
            return NotFound(ApiResponse<ApplicationDto>.Fail(errorMessage ?? "Application not found"));

        return Ok(ApiResponse<ApplicationDto>.Ok(data!));
    }

    /// <summary>
    /// Create a new application
    /// </summary>
    [HttpPost("CreateApplication")]
    public async Task<IActionResult> CreateApplication([FromQuery] CreateApplicationDto dto)
    {
        var result = await _applicationService.CreateApplicationAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<string>.Fail(result.ErrorMessage ?? "Failed to create application"));

        return Ok(ApiResponse<string>.Ok("Application created successfully"));
    }

    /// <summary>
    /// Create a new application role
    /// </summary>
    [HttpPost("CreateApplicationRole")]
    public async Task<IActionResult> CreateApplicationRole([FromBody] CreateApplicationRoleDto dto)
    {
        var result = await _applicationService.CreateApplicationRoleAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<string>.Fail(result.ErrorMessage ?? "Failed to create application role"));

        return Ok(ApiResponse<string>.Ok("Application role created successfully"));
    }

    /// <summary>
    /// Update an existing application
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] UpdateApplicationDto dto)
    {
        var (isSuccess, errorMessage) = await _applicationService.UpdateApplicationAsync(dto, id);

        if (!isSuccess)
        {
            if (errorMessage?.Contains("not found") == true)
                return NotFound(ApiResponse<object>.Fail(errorMessage));
            return BadRequest(ApiResponse<object>.Fail(errorMessage ?? "Failed to update application"));
        }

        return Ok(ApiResponse<object?>.Ok(default, "Application updated successfully"));
    }

    /// <summary>
    /// Delete an application
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var (isSuccess, errorMessage) = await _applicationService.DeleteApplicationAsync(id);

        if (!isSuccess)
            return NotFound(ApiResponse<object>.Fail(errorMessage ?? "Application not found"));

        return Ok(ApiResponse<object?>.Ok(default, "Application deleted successfully"));
    }
}
