namespace Apps_Apis;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<AppRole> AppRoles { get; set; } = new List<AppRole>();
}
