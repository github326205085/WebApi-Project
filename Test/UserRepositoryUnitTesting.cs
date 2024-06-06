using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using TestProject;
using Moq.EntityFrameworkCore;

namespace Test;

public class UserRepositoryUnitTesting
{

    public UserRepositoryUnitTesting()
    {

    }
    [Fact]
    public async Task HappyPath_Login_UserExists_ReturnsUser()
    {
        //Arrange
        var user = new User { Email = "test@gmail.com", Password = "Password" };
        var mockContext = new Mock<_326223617BookStoreContext>();
        var users = new List<User>() { user };

        mockContext.Setup(x => x.Users).ReturnsDbSet(users);
        var userRepository = new UserRepository(mockContext.Object);
        //Act
        var result = await userRepository.Login(user.Email, user.Password);

        //Assert
        Assert.Equal(user, result);
    }


    [Fact]
    public async Task AddUser_ReturnsAddedUser()
    {
        // Arrange
        var user = new User { UserId = 1, FirstName = "Alice", LastName = "Smith" };

        var mockSet = new Mock<DbSet<User>>();

        var mockContext = new Mock<_326223617BookStoreContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var repository = new UserRepository(mockContext.Object);

        // Act
        var result = await repository.AddUser(user);

        // Assert
        Assert.Equal(user, result);
    }


    [Fact]
    public async Task UpdateUser_UpdatesUserInformation()
    {
        // Arrange
        var existingUser = new User { UserId = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Password = "oldpassword" };
        var updatedUser = new User { UserId = 1, FirstName = "Alice", LastName = "Jones", Email = "alice.jones@example.com", Password = "newpassword" };

        var mockSet = new Mock<DbSet<User>>();
        mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(existingUser);

        var mockContext = new Mock<_326223617BookStoreContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var repository = new UserRepository(mockContext.Object);

        // Act
        var result = await repository.UpdateUser(updatedUser);

        // Assert
        Assert.Equal(updatedUser.LastName, result.LastName);
        Assert.Equal(updatedUser.Email, result.Email);
        Assert.Equal(updatedUser.Password, result.Password);
    }


    [Fact]
    public async Task UnhappyPath_Login_UserNotFound_ReturnsNull()
    {
        var user = new User { Email = "test@gmail.com", Password = "Password" };
        var mockContext = new Mock<_326223617BookStoreContext>();
        var users = new List<User>() { user };

        mockContext.Setup(x => x.Users).ReturnsDbSet(users);
        var userRepository = new UserRepository(mockContext.Object);
        //Act
        var result = await userRepository.Login("aaa", user.Password);

        //Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task UnhappyPath_UpdateUser_UserNotFound_ReturnsNull()
    {
        // Arrange
        var existingUser = new User { UserId = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Password = "oldpassword" };
        var updatedUser = new User { UserId = 2, FirstName = "Alice", LastName = "Jones", Email = "alice.jones@example.com", Password = "newpassword" };

        var mockSet = new Mock<DbSet<User>>();
        mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(existingUser);

        var mockContext = new Mock<_326223617BookStoreContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var repository = new UserRepository(mockContext.Object);

        // Act
        var result = await repository.UpdateUser(updatedUser);
        // Assert
        Assert.Null(result);
    }
}