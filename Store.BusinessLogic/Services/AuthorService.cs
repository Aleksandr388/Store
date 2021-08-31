using AutoMapper;
using Shared.Constants;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Interfaces;
using System;
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
            var checkModel = await _authorRepository.GetByName(model.Name);

            if (checkModel is not null)
            {
                throw new Exception();

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

            var checkModel = await _authorRepository.GetByName(model.Name);

            if (checkModel is null)
            {
                throw new CustomException(ErrorMessages.AuthorNoExists, HttpStatusCode.BadRequest);
            }

            var authorModel = _mapper.Map<Author>(model);

            await _authorRepository.UpdateAsync(authorModel);
        }

        public async Task<AuthorModel> GetByIdAsync(AuthorModel modelId)
        {
            if (modelId is null)
            {
                throw new CustomException(ErrorMessages.NoUserForSpecifiedId, HttpStatusCode.BadRequest);
            }

            var checkModel = await _authorRepository.GetByIdAsync(modelId.Id);

            if (checkModel is null)
            {
                throw new CustomException(ErrorMessages.AuthorNoExists, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<AuthorModel>(checkModel);
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

            (IEnumerable<Author> authors, int count) authorCount = await _authorRepository.GetAllAuthorsAsync(mappedPageModel);

            var authors = _mapper.Map<IEnumerable<AuthorModel>>(authorCount.authors);

            var paginationInfo = new PageModel(mappedPageModel.PageNumber, mappedPageModel.PageSize, authorCount.count);

            var responseModel = new ResponseModel<AuthorModel>()
            {
                PageModel = paginationInfo,
                NavigationModels = authors
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
