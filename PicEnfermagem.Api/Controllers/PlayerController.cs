using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.Interfaces;

namespace PicEnfermagem.Api.Controllers;
[ApiController]
[Route("api/player")]
public class PlayerController : Controller
{
    private readonly IPlayerService _playerService;
    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    [HttpPost]
    public async Task<ActionResult> InsertPlayer([FromBody] PlayerInsertRequest model)
    {
        var response = await _playerService.InsertPlayer(model);
      
        if (response)
            return Ok();

        return BadRequest();
    }
}