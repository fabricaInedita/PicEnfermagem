using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.Interfaces;

namespace PicEnfermagem.Api.Controllers;
[ApiController]
[Route("api/question")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService questionService;
    public QuestionsController(IQuestionService questionService)
    {
        this.questionService = questionService;
    }

    [Authorize(Policy = "AdminRole")]
    [HttpPost]
    public async Task<ActionResult> PostAsync(QuestionInsertRequest model)
    {
        var response = await questionService.InsertAsync(model);

        if (response)
            return Ok();

        return BadRequest();
    }


    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var response = await questionService.GetAllAsync();

        return Ok(response);
    }

    [HttpGet]
    [Route("/getByCategory")]
    public async Task<ActionResult> GetByCategoryAsync([FromQuery] int categoryId)
    {
        var response = await questionService.GetByCategoryAsync(categoryId);

        return Ok(response);
    }
}
