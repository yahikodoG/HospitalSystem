using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context)
        : base(context) { }
}