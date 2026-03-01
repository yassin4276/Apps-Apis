

namespace Apps_Apis.Dtos.AppDtos;

public class ApplicationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Stack { get; set; } = string.Empty;
    public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
}
