using AutoMapper;
using Shared.Constants;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
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
            var authorModel = _mapper.Map<Author>(model);

            await _authorRepository.UpdateAsync(authorModel);
        }

        public async Task DeleteAsync(AuthorModel model)
        {
            var deleteAuthorModel = _mapper.Map<Author>(model);

            await _authorRepository.DeleteAsync(deleteAuthorModel);
        }

        public async Task<AuthorModel> GetByIdAsync(AuthorModel modelId)
        {
            if (modelId.Id == 0)
            {
                throw new CustomException(ErrorMessages.NoUserForSpecifiedId, HttpStatusCode.BadRequest);
            }

            var authorModel = await _authorRepository.GetByIdAsync(modelId.Id);

            return _mapper.Map<AuthorModel>(authorModel);
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAsync()
        {
            IEnumerable<Author> model = await _authorRepository.GetAllAsync();

            var mappedModel = _mapper.Map<IEnumerable<AuthorModel>>(model);

            return mappedModel;
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
