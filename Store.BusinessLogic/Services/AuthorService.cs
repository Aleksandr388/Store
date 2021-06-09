using AutoMapper;
using Shared.Constants;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IMapper mapper, IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(AuthorModel model)
        {
            var chekModel = await _authorRepository.GetByName(model.Name);

            if (chekModel is not null)
            {
                throw new CustomException(ErrorMessages.AuthtorWithThisNameCreated, HttpStatusCode.BadRequest);
            }

            var authorModel = _mapper.Map<Author>(model);

            await _authorRepository.CreateAsync(authorModel);
        }

        public async Task UpdateAsync(AuthorModel model)
        {
            if (model is null)
            {
                throw new CustomException(ErrorMessages.UpdatedModelIsNull, HttpStatusCode.BadRequest);
            }

            var authorModel = _mapper.Map<Author>(model);

            

            await _authorRepository.UpdateAsync(authorModel);
        }

        public async Task DeleteAsync(AuthorModel model)
        {
            if (model is null)
            {
                throw new CustomException(ErrorMessages.RemoveIsFailed, HttpStatusCode.BadRequest);
            }

            var deleteAuthorModel = _mapper.Map<Author>(model);

            if (deleteAuthorModel.IsRemoved is not false)
            {
                throw new CustomException(ErrorMessages.ModelAlredyBeenDeleted, HttpStatusCode.BadRequest);
            }

            await _authorRepository.DeleteAsync(deleteAuthorModel);
        }

        public async Task<AuthorModel> GetByIdAsync(AuthorModel modelId)
        {
            if (modelId is null)
            {
                throw new CustomException(ErrorMessages.NoUserForSpecifiedId, HttpStatusCode.BadRequest);
            }

            var authorModel = await _authorRepository.GetByIdAsync(modelId.Id);

            return _mapper.Map<AuthorModel>(authorModel);
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAsync()
        {
            IEnumerable<Author> models = await _authorRepository.GetAllAsync();

            if (models is null)
            {
                throw new CustomException(ErrorMessages.TheListIsEmpty, HttpStatusCode.BadRequest);
            }

            var mappedModel = _mapper.Map<IEnumerable<AuthorModel>>(models);

            return mappedModel;
        }

        public async Task<ResponseModel<AuthorModel>> GetAllAuthorsAsync(AuthorFiltrationModel authorModel)
        {
            var mappedPageModel = _mapper.Map<AuthorFiltration>(authorModel);

            var allmodels = await _authorRepository.GetAllAuthorsAsync(mappedPageModel);

            var printingEditions = _mapper.Map<IEnumerable<AuthorModel>>(allmodels);

            var paginationInfo = new PageModel(mappedPageModel.PageNumber, mappedPageModel.PageSize);

            var responseModel = new ResponseModel<AuthorModel>()
            {
                PageModel = paginationInfo,
                NavigationModels = printingEditions
            };

            return responseModel;
        }

        public async Task RemoveAsync(AuthorModel model)
        {
            if (model is null)
            {
                throw new CustomException(ErrorMessages.NoUserForSpecifiedId, HttpStatusCode.BadRequest);
            }

            var removeModel = _mapper.Map<Author>(model);

            await _authorRepository.RemoveAsync(removeModel.Id);
        }
    }
}
