using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task CreateAsync(AuthorModel model);
        public Task UpdateAsync(AuthorModel model);
        public Task DeleteAsync(AuthorModel model);
        public Task<AuthorModel> GetByIdAsync(AuthorModel Modelid);
        public Task<IEnumerable<AuthorModel>> GetAllAsync();
        public Task RemoveAsync(AuthorModel model);
        public Task<ResponseModel<AuthorModel>> GetAllAuthorsAsync(AuthorFiltrationModel authorModel);
    }
}
