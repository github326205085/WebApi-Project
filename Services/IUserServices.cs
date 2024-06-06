using Entities;

namespace Services
{
    public interface IUserServices
    {
        Task<User> AddUser(User user);
        Task<User> Login(String userName, String password);
        Task<User> UpdateUser(User userToUpdate);
        public int CheckPassword(string pass);

    }
}