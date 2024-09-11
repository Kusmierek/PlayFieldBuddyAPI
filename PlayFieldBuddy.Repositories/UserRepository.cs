using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PlayFieldBuddyDbContext _context;
    
    public UserRepository(PlayFieldBuddyDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<User?>> GetManyUsers(CancellationToken cancellationToken) =>
        await _context.Users.ToListAsync(cancellationToken);

    public async Task DeleteUser(User user, CancellationToken cancellationToken)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<User?> GetSingleUserById(Guid id, CancellationToken cancellationToken) =>
        await _context.Users
        .Include(g=>g.OwnedGames)
        .Include(g=>g.JoinedGames)
        .FirstOrDefaultAsync(user => user.Id == id);
    
    public async Task<int?> UpdateUser(User user, CancellationToken cancellationToken)
    {
        _context.Entry(user).State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByName(string username, CancellationToken cancellationToken) =>
        await _context.Users.FirstOrDefaultAsync(user =>
            user.Username == username);
}