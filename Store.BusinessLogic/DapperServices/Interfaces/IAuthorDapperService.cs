using Store.BusinessLogic.Models.Authors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.DapperServices.Interfaces
{
    public interface IAuthorDapperService
    {
        public Task<IEnumerable<AuthorModel>> GetAllAsync();
        public Task<AuthorModel> GetByIdAsync(AuthorModel model);
        public Task UpdateAsync(AuthorModel model);
        public Task CreateAsync(AuthorModel model);
    }
}
