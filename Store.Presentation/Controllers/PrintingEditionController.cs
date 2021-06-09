using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PaginationModels;
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
        public async Task<IActionResult> Create([FromBody] PrintingEditionModel printingEditionModel)
        {
            await _printingEditionService.CreateAsync(printingEditionModel);

            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] PrintingEditionModel printingEditionModel)
        {
            await _printingEditionService.DeleteAsync(printingEditionModel);

            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _printingEditionService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllPrintingEditions(PrintingEditionFiltrationModel pEModel)
        {
            var result = await _printingEditionService.GetAllPrintingEditionsAsync(pEModel);

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromBody] PrintingEditionModel modelId)
        {
            var result = await _printingEditionService.GetByIdAsync(modelId);

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] PrintingEditionModel printingEditionModel)
        {
            await _printingEditionService.UpdateAsync(printingEditionModel);

            return Ok("Update is done");
        }
    }
}
