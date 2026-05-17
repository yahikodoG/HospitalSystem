using Application.DTOs.Services;
using Domain.Entities;

namespace Application.Mappings;

public static class ServiceMappings
{
    public static ServiceResponse MapToResponse(this Service service)
    {
        return new ServiceResponse
        {
            ServiceCode = service.ServiceCode,
            ServiceName = service.ServiceName,
            ServiceTypeId = service.ServiceTypeId,
            Price = service.Price,
            Description = service.Description
        };
    }
}