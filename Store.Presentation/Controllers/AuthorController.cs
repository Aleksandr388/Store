using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AuthorModel model)
        {
            await _authorService.CreateAsync(model);
            return Ok();
        }

        [HttpPost("Upadate")]
        public async Task<IActionResult> Upadate([FromBody] AuthorModel model)
        {
            await _authorService.UpdateAsync(model);
            return Ok();
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById([FromBody] long id)
        {
            var result = await _authorService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authorService.GetAllAsync();

            return Ok(result);
        }

        [HttpPost("Remove")]
        public async Task<IActionResult> Remove([FromBody] AuthorModel model)
        {
            await _authorService.RemoveAsync(model);

            return Ok("Remove is done");
        }

    }
}

