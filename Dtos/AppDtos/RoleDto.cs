namespace Apps_Apis.Dtos.AppDtos;

public class RoleDto
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ApplicationId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
