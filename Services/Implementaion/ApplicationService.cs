using Microsoft.EntityFrameworkCore;
using Apps_Apis;
using Apps_Apis.Data;
using Apps_Apis.Dtos.AppDtos;
using Apps_Apis.Services.Interfaces;

namespace Apps_Apis.Services.Implementaion;

public class ApplicationService : IApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;

    public ApplicationService(IUnitOfWork unitOfWork, AppDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> CreateApplicationAsync(CreateApplicationDto dto)
    {
        try
        {
            var application = new Application
            {
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image,
                Url = dto.Url,
                Stack = dto.Stack
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> CreateApplicationRoleAsync(CreateApplicationRoleDto dto)
    {
        try
        {
            var application = await _unitOfWork.Applications.GetByIdAsync(dto.ApplicationId);
            if (application == null)
                return (false, "Application not found");
            var role = await _unitOfWork.Roles.GetByIdAsync(dto.RoleId);
            if (role == null)
                return (false, "Role not found");
            var applicationRole = new AppRole
            {
                ApplicationId = dto.ApplicationId,
                RoleId = dto.RoleId,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            await _unitOfWork.AppRoles.AddAsync(applicationRole);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteApplicationRoleAsync(int applicationId, int appRoleId)
    {
        try
        {
            var application = await _unitOfWork.Applications.GetByIdAsync(applicationId);
            if (application == null)
                return (false, "Application not found");
            var applicationRole = await _unitOfWork.AppRoles.GetByIdAsync(appRoleId);
            if (applicationRole == null)
                return (false, "Application role not found");
            _unitOfWork.AppRoles.Delete(applicationRole);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception ex)
        {   
            return (false, ex.Message);
        }
    }
    public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateApplicationAsync(UpdateApplicationDto dto, int appId)
    {
        try
        {
            var application = await _unitOfWork.Applications.GetByIdAsync(appId);
            if (application == null)
                return (false, "Application not found");

            if (dto.Name != null) application.Name = dto.Name;
            if (dto.Description != null) application.Description = dto.Description;
            if (dto.Image != null) application.Image = dto.Image;
            if (dto.Url != null) application.Url = dto.Url;
            if (dto.Stack != null) application.Stack = dto.Stack;

            _unitOfWork.Applications.Update(application);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteApplicationAsync(int appId)
    {
        try
        {
            var application = await _unitOfWork.Applications.GetByIdAsync(appId);
            if (application == null)
                return (false, "Application not found");

            _unitOfWork.Applications.Delete(application);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, ApplicationDto? Data, string? ErrorMessage)> GetApplicationByIdAsync(int appId)
    {
        try
        {
            var application = await _unitOfWork.Applications.GetAllAsQueryable()
                .Include(a => a.AppRoles)
                .ThenInclude(ar => ar.Role)
                .FirstOrDefaultAsync(a => a.Id == appId);

            if (application == null)
                return (false, null, "Application not found");

            var dto = MapToDto(application);
            return (true, dto, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, List<ApplicationDto>? Data, string? ErrorMessage)> GetAllApplicationsAsync()
    {
        try
        {
            var applications = await _unitOfWork.Applications.GetAllAsQueryable()
                .Include(a => a.AppRoles)
                .ThenInclude(ar => ar.Role)
                .ToListAsync();

            var dtos = applications.Select(MapToDto).ToList();
            return (true, dtos, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    private static ApplicationDto MapToDto(Application application)
    {
        return new ApplicationDto
        {
            Id = application.Id,
            Name = application.Name,
            Description = application.Description,
            Image = application.Image,
            Url = application.Url,
            Stack = application.Stack,
            Roles = application.AppRoles
                .Select(ar => new RoleDto
                {
                    RoleId = ar.RoleId,
                    Name = ar.Role.Name,
                    ApplicationId = ar.ApplicationId,
                    Email = ar.Email,
                    PasswordHash = ar.PasswordHash
                })
                .ToList()
        };
    }
}
