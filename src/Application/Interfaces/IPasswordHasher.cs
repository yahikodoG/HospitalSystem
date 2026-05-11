namespace Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password, int workFactor = 12);
    bool VerifyPassword(string password = "", string hashedPassword = "");
}