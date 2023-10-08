using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Identity.Services;

namespace PicEnfermagem.Api.Controllers;
[ApiController]
[Route("api/answer")]
public class AnswerController : Controller
{
    private readonly IAnswerService _answerService;

    public AnswerController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> PostAnswer(AnswerInsertRequest question)
    {
        var result = await _answerService.PostAnswer(question, User);

        if (result is not null)
            return Ok(result);

        return BadRequest();
    }
}
