using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.DataAcess.Extensions;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {

        public PrintingEditionRepository(ShopDbContext contex) : base(contex)
        {
        }

        public async override Task<PrintingEdition> GetByIdAsync(long id)
        {
            var result = await _dbSet
                .Include(x => x.Authors)
                .FirstOrDefaultAsync();

            return result;
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

        public override async Task<IEnumerable<PrintingEdition>> GetAllAsync()
        {
            var result = await _dbSet
                .Include(x => x.Authors)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PrintingEdition>> GetAllPrintingEditionsAsync(PrintingEditionFiltration PFSModel)
        {
            var printingEditions = await _dbSet
                .Include(x => x.Authors)
                .AsNoTracking()
                .Where(x => PFSModel.Description == null || x.Description.Contains(PFSModel.Description))
                .Where(x => PFSModel.Title == null || x.Title.Contains(PFSModel.Title))
                .Where(x => PFSModel.NameAuthor == null || x.Authors.Any(y => y.Name.Contains(PFSModel.NameAuthor)))
                .Where(x => PFSModel.MaxPrice == 0 || x.Price >= PFSModel.MinPrice && x.Price <= PFSModel.MaxPrice)
                .Where(x => !PFSModel.Type.Any() || PFSModel.Type.Contains(x.Type))
                .OrderByField(PFSModel.SortIndex, PFSModel.IsAccesing)
                .Skip((PFSModel.PageNumber - 1) * PFSModel.PageSize)
                .Take(PFSModel.PageSize)
                .ToListAsync();

            return printingEditions;
        }

        public override async Task UpdateAsync(PrintingEdition model)
        {
            var authorsModels = new List<Author>(model.Authors);

            model.Authors.Clear();

            _dbSet.UpdateRange(model);

            var pEForUpdate = await _dbSet
               .Include(model => model.Authors)
               .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (pEForUpdate.Authors is not null)
            {
                pEForUpdate.Authors
                 .RemoveAll(x => !authorsModels
                 .Exists(y => y.Id == x.Id));
            }

            var result = authorsModels
                .Where(x => !pEForUpdate.Authors
                .Exists(y => y.Id == x.Id))
                .ToList();

            pEForUpdate.Authors.AddRange(result);

            _dbSet.Update(pEForUpdate);

            await SaveChagesAsync();
        }
    }
}




