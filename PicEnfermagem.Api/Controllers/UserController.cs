using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.User;
using PicEnfermagem.Application.Interfaces;

namespace PaintBall_Project.Api.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly Lazy<IIdentityService> _identityService;

    public UserController(Lazy<IIdentityService> identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    [Route("user/login")]
    public async Task<ActionResult> LoginAsync(LoginRequest model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.Value.LoginAsync(model);

        if(result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("user/delete")]
    public async Task<ActionResult> UserDelete(string email)
    {
        var result = await _identityService.Value.DeleteUser(email);

        if(!result.Sucess) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet]
    [Route("user/get_users")]
    public async Task<ActionResult> GetAsync()
    {
        return Ok(await _identityService.Value.GetUser());
    }

    [HttpPost]
    [Route("user/register")]
    public async Task<ActionResult> UserRegisterAsync(UserInsertRequest userRegisterRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.Value.PreparaUser(userRegisterRequest);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    [Route("get_rank")]
    public async Task<IActionResult> GetRankAsync()
    {
        return Ok(await _identityService.Value.GetRank());
    }

    [HttpPost]
    [Route("api/user/confirmEmail")]
    public async Task<IActionResult> ConfirmUserEmail(ConfirmEmail model)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest);

        var result = await _identityService.Value.ConfirmEmail(model.Token, model.UserId);

        return Ok(result);
    }

    [HttpPost]
    [Route("api/user/certicate")]
    [Authorize]
    public async Task<IActionResult> IssueCertificateAsync()
    {
        await _identityService.Value.IssueCertificate();

        return Ok();
    }

    [HttpPost]
    [Route("api/user/send_reset_password_token")]
    public async Task<IActionResult> SendResetPasswordTokenAsync(string email)
    {
        await _identityService.Value.GenarateRefreshPasswordToken(email);

        return Ok();
    }

    [HttpPatch]
    [Route("api/user/reset_password")]
    public async Task<IActionResult> ResetPasswordAsync(UserResetPassword model)
    {
        var response = await _identityService.Value.ResetPasswordAsync(model);

        if (!response.Sucess)
            return BadRequest(response.Errors);

        return Ok();

    }
}
