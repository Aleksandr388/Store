using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Store.DataAcess.Entities;
using Store.DataAcess.Extensions;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ShopDbContext context) : base(context)
        {
        }

        public async override Task<Author> GetByIdAsync(long id)
        {
            var result = await _dbSet.Include(x => x.PrintingEditions).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            var result = await _dbSet.Include(x => x.PrintingEditions).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(AuthorFiltration author)
        {
            var authors = await _dbSet
                .Include(x => x.PrintingEditions)
                .AsNoTracking()
                .Where(x => author.Name == null || x.Name.Contains(author.Name))
                .Where(x => author.PrintingEditionTitle == null || x.PrintingEditions.Any(y => y.Title.Contains(author.PrintingEditionTitle)))               
                .OrderByField(author.SortOrder, author.IsAccesing)
                .Skip((author.PageNumber - DefaultValues.PageStep) * author.PageSize)
                .Take(author.PageSize)
                .ToListAsync();

            return authors;
        }
        public bool IsAuthorExist(IEnumerable<Author> models)
        {
            var result = models.All(x => _dbSet.Select(y => y).Contains(x));

            return result;
        }

        public async Task<Author> GetByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task RemoveAsync(long id)
        {
            var removeModel = await _dbSet.FindAsync(id);

            removeModel.IsRemoved = true;

            await SaveChagesAsync();
        }

        public override async Task UpdateAsync(Author model)
        {
            var pEmodels = new List<PrintingEdition>(model.PrintingEditions);

            model.PrintingEditions.Clear();

            var authorForUpdate = await _dbSet
                .Include(model => model.PrintingEditions)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (authorForUpdate is not null)
            {
                authorForUpdate.PrintingEditions
                    .RemoveAll(x => !pEmodels
                    .Exists(y => y.Id == x.Id));
            }

            var result = pEmodels
                .Where(x => !authorForUpdate.PrintingEditions
                .Exists(y => y.Id == x.Id))
                .ToList();

            authorForUpdate.PrintingEditions.AddRange(result);

            _dbSet.Update(authorForUpdate);

            await SaveChagesAsync();
        }
    }
}
