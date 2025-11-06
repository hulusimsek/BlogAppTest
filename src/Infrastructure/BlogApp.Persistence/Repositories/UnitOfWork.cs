using Microsoft.EntityFrameworkCore.Storage;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BlogApp.Domain.Repositories;
using BlogApp.Persistence.Data;
using BlogApp.Persistence.Identity;

namespace BlogApp.Persistence.Repositories;

/// <summary>
/// Unit of Work Implementation
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    private readonly UserManager<IdentityAppUser> _userManager;
    private readonly IMapper _mapper;

    public UnitOfWork(
        ApplicationDbContext context,
        UserManager<IdentityAppUser> userManager,
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
        Users = new UserRepository(_context, _userManager, _mapper);
        Roles = new RoleRepository(_context, _mapper);
    }

    public IUserRepository Users { get; private set; }
    public IRoleRepository Roles { get; private set; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}