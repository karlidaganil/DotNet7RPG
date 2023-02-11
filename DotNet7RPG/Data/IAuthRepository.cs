namespace DotNet7RPG.Data;

public interface IAuthRepository
{
    public Task<ServiceResponse<int>> Register(User user, string password);
    public Task<ServiceResponse<string>> Login(string username, string password);
    public Task<bool> UserExists(string username);
}