using Entities;
using Microsoft.Extensions.Logging;
using Repository;
using System.Runtime.CompilerServices;

namespace Services;

public class UserServices : IUserServices
{
    private readonly IUserRepository userRepository;

    public UserServices(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
  

    }

    public async Task<User> AddUser(User user)
    {
        var res = Zxcvbn.Core.EvaluatePassword(user.Password.ToString());

        if(res.Score < 2)
        {
            return null;
        }
        return await userRepository.AddUser(user);
    }

    public async Task<User> Login(String userName, String password)
    {
        return await userRepository.Login(userName, password);
    }
    public async Task<User> UpdateUser(User userToUpdate)
    {
        var res = Zxcvbn.Core.EvaluatePassword(userToUpdate.Password.ToString());

        if (res.Score < 2)
        {
            return null;
        }
        return await userRepository.UpdateUser(userToUpdate);
    }
    public int CheckPassword(string pass)
    {
        var res = Zxcvbn.Core.EvaluatePassword(pass.ToString());

        return userRepository.CheckPassword(res.Score);
    }
}
