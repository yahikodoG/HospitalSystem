using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtAuthService
{
    string GenerateToken(User user, List<string> roles);
    string DecodePayloadToken(string token);
}