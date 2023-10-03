using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.Interfaces;

namespace PicEnfermagem.Api.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    //Preciso armazenar as resposta do user por request no perfil dele
    //Retornar no get inicial se as perguntas ja foram respondidas 
    //armazenar tempo de resposta de cada pergunta para depois fazer uma comparação
    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var response = await _categoryService.GetAsync();

        if (!response.Sucess)
            return Ok(response.Data);

        return BadRequest(response);
    }

    [Authorize(Policy = "AdminRole")]
    [HttpPost]
    public async Task<ActionResult> InsertAsync(CategoryInsertRequest category)
    {
        var result = await _categoryService.InsertAsync(category);

        if (result)
            return Ok();

        return BadRequest();
    }
}
