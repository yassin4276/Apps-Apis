namespace Apps_Apis;

public interface IUnitOfWork
{
    IBaseRepository<Application> Applications { get; }
    IBaseRepository<Role> Roles { get; }
    IBaseRepository<AppRole> AppRoles { get; }
    Task<int> SaveChangesAsync();
}
