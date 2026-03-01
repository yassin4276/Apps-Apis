namespace Apps_Apis.Dtos.AppDtos;

/// <summary>
/// Used when creating an application - ApplicationId is set automatically from the newly created application.
/// </summary>
public class RoleForCreateApplicationDto
{
    public int RoleId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
