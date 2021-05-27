using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingEditionController : Controller
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]PrintingEditionModel model)
        {
            await _printingEditionService.CreateAsync(model);

            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] PrintingEditionModel model)
        {
            await _printingEditionService.DeleteAsync(model);

            return Ok();
        }
    }
}
