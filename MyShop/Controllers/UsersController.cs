using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyShop.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : ControllerBase
{
    private readonly IUserServices userServices;
    private readonly IMapper mapper;
    private readonly ILogger<UsersController> logger;

    public UsersController(IUserServices userServices, IMapper mapper, ILogger<UsersController> logger)
    {
        this.logger = logger;
        this.userServices = userServices;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<UserGetRegisterDTO>> Create([FromBody] UserRegisterDTO userDTO)
    {
       
        User newUser = await userServices.AddUser(mapper.Map<UserRegisterDTO, User>(userDTO));

        if (newUser == null)
        {
            return NoContent();
        } 
        return  mapper.Map<User, UserGetRegisterDTO>(newUser);
    }
     
    [HttpPost("password")]
    public ActionResult<int> Password([FromBody] string pass)
    {
        if (pass == "")
            return NoContent();
        return userServices.CheckPassword(pass);
    }

    [HttpPost("login")]
    public  async Task< ActionResult<UserGetDTO> >Login([FromBody] UserLoginDTO u ) 
    {
        logger.LogError($"Someone connected to this site with userName: {u.Email} and password: {u.Password}");

        User user = await userServices.Login(u.Email, u.Password);
            UserGetDTO useGetDTO=mapper.Map<User, UserGetDTO>(user);
            if (user != null)
                return  Ok(useGetDTO);
            return Unauthorized();
    }

    [HttpPut]
    public async Task<ActionResult<UserGetDTO>> Update([FromBody] UserGetUpdateDTO userToUpdate)
    {    
        User user = await userServices.UpdateUser(mapper.Map<UserGetUpdateDTO, User>(userToUpdate));
        UserGetDTO useGetDTO = mapper.Map<User, UserGetDTO>(user);
        if (user != null)
            return Ok(useGetDTO);
        return NoContent();
    }
}
