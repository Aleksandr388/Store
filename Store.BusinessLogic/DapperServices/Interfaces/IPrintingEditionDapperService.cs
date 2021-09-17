using Store.BusinessLogic.Models.PrintingEditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.DapperServices.Interfaces
{
    public interface IPrintingEditionDapperService
    {
        public Task<IEnumerable<PrintingEditionModel>> GetAllAsync();
        public Task<PrintingEditionModel> GetByIdAsync(PrintingEditionModel model);
        public Task UpdateAsync(PrintingEditionModel model);
        public Task CreateAsync(PrintingEditionModel model);
    }
}
