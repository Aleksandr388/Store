using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseRepository<PrintingEdition>
    {
        public Task<PrintingEdition> GetByTitleAsync(string title);
        public Task<IEnumerable<PrintingEdition>> Get(Page pageModel);
    }
}
