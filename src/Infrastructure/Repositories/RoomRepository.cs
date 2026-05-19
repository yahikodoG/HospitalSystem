using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(AppDbContext context)
        : base(context) { }

    public async Task<bool> ExistsByNameAsync(string roomName)
    {
        return await _dbSet
            .AnyAsync(r => r.RoomName == roomName);
    }

    // public async Task<bool> HasAppointmentsAsync(int roomId)
    // {
    //     return await _context.Appointments
    //         .AnyAsync(a => a.RoomId == roomId);
    // }
}