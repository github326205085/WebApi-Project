using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repository;
public class UserRepository : IUserRepository
{
    private _326223617BookStoreContext bookStoreContext;
    public UserRepository(_326223617BookStoreContext bookStoreContext)
    {
        this.bookStoreContext = bookStoreContext;
    }



    public async Task<User> AddUser(User user)
    {
        await bookStoreContext.Users.AddAsync(user);
        await bookStoreContext.SaveChangesAsync();
        return user;
    }



    public async Task<User> Login(String Email, String Password)
    {
       return await bookStoreContext.Users.FirstOrDefaultAsync(userf => userf.Email.Equals(Email) && userf.Password.Equals(Password));
    }


    public async Task<User> UpdateUser(User userToUpdate)
    {
        User user = await bookStoreContext.Users.FindAsync(userToUpdate.UserId);
        if (user!=null) {
            if (!userToUpdate.FirstName.Equals(""))
                user.FirstName = userToUpdate.FirstName;
            else user.FirstName = user.FirstName;
            if (!userToUpdate.LastName.Equals(""))
                user.LastName = userToUpdate.LastName;
            else user.LastName = user.LastName;
            if (!userToUpdate.Email.Equals(""))
                user.Email = userToUpdate.Email;
            else user.Email = user.Email;
            if (!userToUpdate.Password.Equals(""))
                user.Password = userToUpdate.Password;
            else user.Password = user.Password;
        await bookStoreContext.SaveChangesAsync();
        }
        return user;
    }
    public int CheckPassword(int res)
    {
        return res;
    }
}