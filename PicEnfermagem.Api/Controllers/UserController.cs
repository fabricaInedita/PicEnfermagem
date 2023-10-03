using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.Interfaces;

namespace PaintBall_Project.Api.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public UserController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    [Route("user/login")]
    public async Task<ActionResult> LoginAsync(LoginRequest model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.LoginAsync(model);

        if(result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("user/delete")]
    public async Task<ActionResult> UserDelete(string email)
    {
        var result = await _identityService.DeleteUser(email);

        if(!result.Sucess) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet]
    [Route("user/get_users")]
    public async Task<ActionResult> GetAsync()
    {
        return Ok(await _identityService.GetUser());
    }

    [HttpPost]
    [Route("user_admin/register")]
    public async Task<ActionResult> UserAdminRegisterAsync(UserAdminRegisterRequest userRegisterRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.RegisterUserAdmin(userRegisterRequest);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost]
    [Route("user/register")]
    public async Task<ActionResult> UserRegisterAsync(UserInsertRequest userRegisterRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.RegisterUser(userRegisterRequest);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost]
    [Route("user/postAnswer")]
    public async Task<ActionResult> PostAnswer(AnswerInsertRequest question)
    {
        var result = await _identityService.PostAnswer(question, User);

        if (result)
            return Ok();

        return BadRequest();
    }
}
