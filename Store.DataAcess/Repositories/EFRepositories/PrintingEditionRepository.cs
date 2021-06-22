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

        public async Task<IEnumerable<PrintingEdition>> GetAllPrintingEditionsAsync(PrintingEditionFiltration editioModel)
        {
            var printingEditions = await _dbSet
                .Include(x => x.Authors)
                .AsNoTracking()
                .Where(x => editioModel.Description == null || x.Description.Contains(editioModel.Description))
                .Where(x => editioModel.Title == null || x.Title.Contains(editioModel.Title))
                .Where(x => editioModel.NameAuthor == null || x.Authors.Any(y => y.Name.Contains(editioModel.NameAuthor)))
                .Where(x => editioModel.MaxPrice == 0 || x.Price >= editioModel.MinPrice && x.Price <= editioModel.MaxPrice)
                .Where(x => !editioModel.Type.Any() || editioModel.Type.Contains(x.Type))
                .OrderByField(editioModel.SortOrder, editioModel.IsAccesing)
                .Skip((editioModel.PageNumber - 1) * editioModel.PageSize)
                .Take(editioModel.PageSize)
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

        public async Task<List<PrintingEdition>> GetEditionRangeAsync(List<long> editionId)
        {
            var editionList = _dbSet.Where(ed => editionId.Contains(ed.Id));

            var result = await editionList.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PrintingEdition>> GetEditionsPrices(List<OrderItem> orderItems)
        {
            var printingEditions = await _dbSet.ToListAsync();

            var result = printingEditions.Where(x => orderItems.Any(y => y.PrintingEditionId == x.Id));

            return result;
        }
    }
}



