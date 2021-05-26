using AutoMapper;
using Microsoft.AspNetCore.Http;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IMapper _mapper;
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IAuthorRepository _authorRepository;

        public PrintingEditionService(IMapper mapper, IPrintingEditionRepository printingEditionRepository, IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _printingEditionRepository = printingEditionRepository;
            _authorRepository = authorRepository;
        }

        public async Task CreateAsync(PrintingEditionModel model)
        {
            if (model.AuthorModels is null)
            {
                throw new CustomException(Shared.Constants.ErrorMessages.ImpossibleToCreateNewAuthor, StatusCodes.Status400BadRequest);
            }

            var allAuthorsList = model.AuthorModels.Select(x => x);
            var mappedAuthorsList = _mapper.Map<IEnumerable<Author>>(allAuthorsList);

            if (!mappedAuthorsList.Any())
            {
                throw new CustomException(Shared.Constants.ErrorMessages.TheCreatedListIsEmpty, StatusCodes.Status400BadRequest);
            }

            var chekListAuthors = _authorRepository.GetAllCreatedAuthors(mappedAuthorsList);

            if (!chekListAuthors)
            {
                throw new CustomException(Shared.Constants.ErrorMessages.CanNotNonexistentAuhtor, StatusCodes.Status400BadRequest);
            }

            var chekPrintingEdition = await _printingEditionRepository.GetByTitleAsync(model.Title);

            if (chekPrintingEdition is not null)
            {
                throw new CustomException(Shared.Constants.ErrorMessages.EditionAlreadyExists, StatusCodes.Status400BadRequest);
            }

            var printingEditionModel = _mapper.Map<PrintingEdition>(model);

            await _printingEditionRepository.CreateAsync(printingEditionModel);
        }

        public Task DeleteAsync(PrintingEditionModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PrintingEditionModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PrintingEditionModel> GetByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(PrintingEditionModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
