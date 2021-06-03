using AutoMapper;
using Microsoft.AspNetCore.Http;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Shared.Constants;
using Store.BusinessLogic.Models.PaginationModels;
using Store.DataAcess.Models;

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
                throw new CustomException(ErrorMessages.ImpossibleToCreateNewAuthor, HttpStatusCode.BadRequest);
            }

            var allAuthorsList = model.AuthorModels.Select(x => x);
            var mappedAuthorsList = _mapper.Map<IEnumerable<Author>>(allAuthorsList);

            if (!mappedAuthorsList.Any())
            {
                throw new CustomException(ErrorMessages.TheCreatedListIsEmpty, HttpStatusCode.BadRequest);
            }

            var chekListAuthors = _authorRepository.GetAllCreatedAuthors(mappedAuthorsList);

            if (!chekListAuthors)
            {
                throw new CustomException(ErrorMessages.CanNotNonexistentAuhtor, HttpStatusCode.BadRequest);
            }

            var printingEditionModel = _mapper.Map<PrintingEdition>(model);

            await _printingEditionRepository.CreateAsync(printingEditionModel);
        }

        public async Task DeleteAsync(PrintingEditionModel model)
        {
            if (model is null)
            {
                throw new CustomException(ErrorMessages.RemoveIsFailed, HttpStatusCode.BadRequest);
            }

            var mappedModel = _mapper.Map<PrintingEdition>(model);

            if (mappedModel.IsRemoved is not false)
            {
                throw new CustomException(ErrorMessages.ModelAlredyBeenDeleted, HttpStatusCode.BadRequest);
            }

            await _printingEditionRepository.DeleteAsync(mappedModel);
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllAsync()
        {
            var printingEditionsModels = await _printingEditionRepository.GetAllAsync();

            if (printingEditionsModels is null)
            {
                throw new CustomException(ErrorMessages.TheListIsEmpty, HttpStatusCode.BadRequest);
            }

            var mappedModels = _mapper.Map<IEnumerable<PrintingEditionModel>>(printingEditionsModels);

            return mappedModels;
        }

        public async Task<IEnumerable<PrintingEditionModel>> Get(PageModel pageModel)
        {
            var mappedPageModel = _mapper.Map<Page>(pageModel);
            var allmodels = await _printingEditionRepository.Get(mappedPageModel);

            var result = _mapper.Map<IEnumerable<PrintingEditionModel>>(allmodels);

            return result;
        }


        public async Task<PrintingEditionModel> GetByIdAsync(PrintingEditionModel printingEditionModel)
        {
            var getByIdModel = await _printingEditionRepository.GetByIdAsync(printingEditionModel.Id);

            if (getByIdModel is null)
            {
                throw new CustomException(ErrorMessages.NoUserForSpecifiedId, HttpStatusCode.BadRequest);
            }
            var mappedPrintingEditionModel = _mapper.Map<PrintingEditionModel>(getByIdModel);

            return mappedPrintingEditionModel;
        }

        public async Task UpdateAsync(PrintingEditionModel model)
        {
            if (model is null)
            {
                throw new CustomException(ErrorMessages.UpdatedModelIsNull, HttpStatusCode.BadRequest);
            }

            var mappedModel = _mapper.Map<PrintingEdition>(model);

            await _printingEditionRepository.UpdateAsync(mappedModel);
        }
    }
}
