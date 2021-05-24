using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {

        public PrintingEditionRepository(ShopDbContext contex) : base(contex)
        {
        }

        
    }
}
