using Apps_Apis.Dtos.AppDtos;

namespace Apps_Apis.Services.Interfaces;

public interface IApplicationService
{
    Task<(bool IsSuccess, string? ErrorMessage)> CreateApplicationAsync(CreateApplicationDto application);
    Task<(bool IsSuccess, string? ErrorMessage)> CreateApplicationRoleAsync(CreateApplicationRoleDto applicationRole);
    Task<(bool IsSuccess, string? ErrorMessage)> UpdateApplicationAsync(UpdateApplicationDto application, int appId);
    Task<(bool IsSuccess, string? ErrorMessage)> DeleteApplicationAsync(int appId);
    Task<(bool IsSuccess, string? ErrorMessage)> DeleteApplicationRoleAsync( int applicationId, int appRoleId);
    Task<(bool IsSuccess, ApplicationDto? Data, string? ErrorMessage)> GetApplicationByIdAsync(int appId);
    Task<(bool IsSuccess,List<ApplicationDto>? Data, string? ErrorMessage)> GetAllApplicationsAsync();
    
}
