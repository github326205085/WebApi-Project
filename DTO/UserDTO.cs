using Entities;
using System.ComponentModel.DataAnnotations;

namespace DTO;

public class UserLoginDTO
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

}
public class UserRegisterDTO
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
public class UserGetDTO
{
     public int UserId { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}

public class UserGetRegisterDTO
{
    public int UserId { get; set; }

    [Required, EmailAddress]

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

}

public class UserGetUpdateDTO
{
    public int UserId { get; set; }
    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

}
