using AutoMapper;
using Shared.Constants;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
            if (model.AuthorModels is not null)
            {
                throw new CustomException(ErrorMessages.ImpossibleToCreateNewAuthor, HttpStatusCode.BadRequest);
            }

            var allAuthorsList = model.AuthorModels.Select(x => x);
            var mappedAuthorsList = _mapper.Map<IEnumerable<Author>>(allAuthorsList);

            if (!mappedAuthorsList.Any())
            {
                throw new CustomException(ErrorMessages.TheCreatedListIsEmpty, HttpStatusCode.BadRequest);
            }

            var chekListAuthors = _authorRepository.IsAuthorExist(mappedAuthorsList);

            if (chekListAuthors)
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

        public async Task<ResponseModel<PrintingEditionModel>> GetAllPrintingEditionsAsync(PrintingEditionFiltrationModel pEModel)
        {
            var mappedPageModel = _mapper.Map<PrintingEditionFiltration>(pEModel);

            (IEnumerable<PrintingEdition> editions, int count) editionsCount = await _printingEditionRepository.GetAllPrintingEditionsAsync(mappedPageModel);

            var printingEditions = _mapper.Map<IEnumerable<PrintingEditionModel>>(editionsCount.editions);

            var paginationInfo = new PageModel(mappedPageModel.PageNumber, mappedPageModel.PageSize, editionsCount.count);

            var responseModel = new ResponseModel<PrintingEditionModel>()
            {
                PageModel = paginationInfo,
                NavigationModels = printingEditions
            };

            return responseModel;
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
