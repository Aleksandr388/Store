using AutoMapper;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IMapper _mapper;
        private readonly IPrintingEditionRepository _printingEditionRepository;

        public PrintingEditionService(IMapper mapper, IPrintingEditionRepository printingEditionRepository)
        {
            _mapper = mapper;
            _printingEditionRepository = printingEditionRepository;
        }

        public async Task CreateAsync(PrintingEditionModel model)
        {
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
