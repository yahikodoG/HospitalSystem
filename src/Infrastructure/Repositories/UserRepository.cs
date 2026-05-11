using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context)
        : base(context) { }

    public async Task<User?> GetUserWithRolesAsync(string username, string email)
    {
        var user = await _context.Users
            .Include(u => u.UserRoleUsers)
            .ThenInclude(ur => ur.Role)
            .SingleOrDefaultAsync(u =>
                u.Username == username ||
                u.Email == email);
        return user;
    }
}