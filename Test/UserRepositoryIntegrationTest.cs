using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;
using Repository;
using TestProject;

namespace Test;

public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
{
    private readonly _326223617BookStoreContext _dbContext;
    private readonly UserRepository _userRepository;

    public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
    {
        _dbContext = databaseFixture.Context;
        _userRepository = new UserRepository(_dbContext);
    }
    [Fact]
    public async Task GetUser_ValidCredentials_ReturnUser()
    {
        //Arrange
        var email = "test@gmail.com";
        var password = "password";
        var user = new User { Email = email, Password = password, FirstName = "test first name", LastName = "test last name" };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _userRepository.Login(email, password);

        //Assert
        Assert.NotNull(result);
    }
}