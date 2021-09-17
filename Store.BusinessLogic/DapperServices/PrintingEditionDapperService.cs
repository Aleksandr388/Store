using AutoMapper;
using Store.BusinessLogic.DapperServices.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.DapperRepositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.DapperServices
{
    public class PrintingEditionDapperService : IPrintingEditionDapperService
    {
        private readonly IPrintingEditionDapperRepository _printingEditionRepository;
        private readonly IMapper _mapper;

        public PrintingEditionDapperService(IPrintingEditionDapperRepository printingEditionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _printingEditionRepository = printingEditionRepository;
        }

        public async Task<PrintingEditionModel> GetByIdAsync(PrintingEditionModel model)
        {
            var checkModel = await _printingEditionRepository.GetByIdAsync(model.Id);

            var result = _mapper.Map<PrintingEditionModel>(checkModel);

            return result;
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllAsync()
        {
            var editions = await _printingEditionRepository.GetAllAsync();

            var mappedEditions = _mapper.Map<IEnumerable<PrintingEditionModel>>(editions);

            return mappedEditions;
        }

        public async Task UpdateAsync(PrintingEditionModel model)
        {
            var editionModel = _mapper.Map<PrintingEdition>(model);

            await _printingEditionRepository.UpdateAsync(editionModel);
        }

        public async Task CreateAsync(PrintingEditionModel model)
        {
            var editionModel = _mapper.Map<PrintingEdition>(model);

            await _printingEditionRepository.CreateAsync(editionModel);
        }

    }
}
