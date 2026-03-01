namespace Apps_Apis.Dtos.AppDtos;

public class UpdateApplicationRoleDto
{
    public int RoleId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
}
