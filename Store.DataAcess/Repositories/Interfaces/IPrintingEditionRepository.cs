using Store.DataAcess.Entities;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseRepository<PrintingEdition>
    {
        public Task<PrintingEdition> GetByTitleAsync(string title);
    }
}
