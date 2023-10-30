using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.User;
using PicEnfermagem.Application.Interfaces;

namespace PicEnfermagem.Api.Controllers;
[ApiController]
[Route("api/question")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> PostAsync(QuestionInsertRequest model)
    {
        var response = await _questionService.InsertAsync(model);

        if (response)
            return Ok();

        return BadRequest();
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllAsync()
    {
        var response = await _questionService.GetAllAsync();

        return Ok(response);
    }

    [HttpGet]
    [Route("/getByCategory")]
    public async Task<ActionResult> GetByCategoryAsync([FromQuery] int categoryId)
    {
        var response = await _questionService.GetByCategoryAsync(categoryId);

        return Ok(response);
    }

    [HttpGet]
    [Route("/questionary-state")]
    public async Task<bool> GetQuestionaryState()
    {

        var result = await _questionService.GetQuestionaryState();

        return result;
    }

    [HttpPost]
    [Route("/change-questionary-state")]
    public async Task<bool> ChangeQuestionaryState()
    {
        var result = await _questionService.ChangeQuestionaryState();

        return result;
    }
}
