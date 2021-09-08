using AutoMapper;
using Store.BusinessLogic.DapperServices.Interfaces;
using Store.BusinessLogic.Models.Authors;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.DapperRepositories;
using Store.DataAcess.Repositories.DapperRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.DapperServices
{
    public class AuthorDapperService : IAuthorDapperService
    {
        
        private readonly IAuthorDapperRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorDapperService(IAuthorDapperRepository authorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<AuthorModel> GetByIdAsync(AuthorModel model)
        {
            var checkModel = await _authorRepository.GetByIdAsync(model.Id);

            var result = _mapper.Map<AuthorModel>(checkModel);

            return result;
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();

            var mappedAuthors = _mapper.Map<IEnumerable<AuthorModel>>(authors);

            return mappedAuthors; 
        }

        public async Task UpdateAsync(AuthorModel model)
        {
            var authorModel = _mapper.Map<Author>(model);

            await _authorRepository.UpdateAsync(authorModel);
        }

        public async  Task CreateAsync(AuthorModel model)
        {
            var authorModel = _mapper.Map<Author>(model);

            await _authorRepository.CreateAsync(authorModel);
        }
    }
}
