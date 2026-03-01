namespace Apps_Apis.Dtos.AppDtos;

public class CreateApplicationRoleDto
{
    public int ApplicationId { get; set; }
    public int RoleId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
