using Entities;

namespace Repository;

public interface IUserRepository
{
    Task<User> AddUser(User user);
    Task<User> Login(String userName, String password);
    Task<User> UpdateUser(User userToUpdate);
    int CheckPassword(int res);

}