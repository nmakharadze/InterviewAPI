using Interview.Application.Repositories;

namespace Interview.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly InterviewDbContext _context;

    public UnitOfWork(InterviewDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}




