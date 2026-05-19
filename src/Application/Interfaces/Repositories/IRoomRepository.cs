using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<bool> ExistsByNameAsync(string roomName);
}