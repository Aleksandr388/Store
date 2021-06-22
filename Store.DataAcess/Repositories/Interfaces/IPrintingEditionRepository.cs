using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseRepository<PrintingEdition>
    {
        public Task<IEnumerable<PrintingEdition>> GetAllPrintingEditionsAsync(PrintingEditionFiltration pageModel);
        public Task<List<PrintingEdition>> GetEditionRangeAsync(List<long> pEditionsId);
        public Task<IEnumerable<PrintingEdition>> GetEditionsPrices(List<OrderItem> orderItems);
    }
}
