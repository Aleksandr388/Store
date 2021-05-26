using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {

        public PrintingEditionRepository(ShopDbContext contex) : base(contex)
        {
        }
        public async Task<PrintingEdition> GetByTitleAsync(string title)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Title == title);

            return result;
        }
        public override async Task CreateAsync(PrintingEdition model)
        {
            List<Author> authors = new List<Author>(model.Authors);

            model.Authors.Clear();

            await _dbSet.AddAsync(model);

            model.Authors = authors;

            await SaveChagesAsync();
        }
    }
}




