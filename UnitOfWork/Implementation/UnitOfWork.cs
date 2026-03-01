using Apps_Apis.Data;
using Apps_Apis.Repos.Implementation;

namespace Apps_Apis.UnitOfWork.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IBaseRepository<Application>? _applications;
    private IBaseRepository<Role>? _roles;
    private IBaseRepository<AppRole>? _appRoles;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<Application> Applications =>
        _applications ??= new BaseRepo<Application>(_context);

    public IBaseRepository<Role> Roles =>
        _roles ??= new BaseRepo<Role>(_context);

    public IBaseRepository<AppRole> AppRoles =>
        _appRoles ??= new BaseRepo<AppRole>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
