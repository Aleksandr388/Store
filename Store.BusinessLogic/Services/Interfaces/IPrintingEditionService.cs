using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task CreateAsync(PrintingEditionModel model);
        public Task UpdateAsync(PrintingEditionModel model);
        public Task DeleteAsync(PrintingEditionModel model);
        public Task<PrintingEditionModel> GetByIdAsync(PrintingEditionModel model);
        public Task<IEnumerable<PrintingEditionModel>> GetAllAsync();
        public Task<ResponseModel<PrintingEditionModel>> GetAllPrintingEditionsAsync(PrintingEditionFiltrationModel pageModel);
    }
}
