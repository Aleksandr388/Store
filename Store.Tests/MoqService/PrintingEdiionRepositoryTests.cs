using AutoMapper;
using Moq;                                                                                                                                                                                                                                                                                                                                                            
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Store.Tests
{
    public class PrintingEdiionRepositoryTests
    {
        [Fact]
        public async Task ReturnsResultWithAListOfPrintingEditions()
        {
            var editions = new List<PrintingEdition>();

            //Arrange 
            var mockAuthor = new Mock<IAuthorRepository>();
            var mockMapper = new Mock<IMapper>();
            var mock = new Mock<IPrintingEditionRepository>();
            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(editions);
            var service = new PrintingEditionService(mockMapper.Object, mock.Object, mockAuthor.Object);

            //Act
            IEnumerable<PrintingEditionModel> act = await service.GetAllAsync();

            //Assert
            Assert.Equal(editions.Count, act.Count());
        }

        [Fact]
        public async Task ResultGetPrintingEditionById()
        {
            long id = 1;

            PrintingEdition edition = MoqFile.MoqPrintingEditionModel.GetPrintingEdition();
            PrintingEditionModel editionModel = MoqFile.MoqPrintingEditionModel.GetPrintingEditionModel(); 
            var mockAuthor = new Mock<IAuthorRepository>();
            var mockMapper = new Mock<IMapper>();
            var mock = new Mock<IPrintingEditionRepository>();
            mock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(edition);
            var service = new PrintingEditionService(mockMapper.Object, mock.Object, mockAuthor.Object);

            PrintingEditionModel act = await service.GetByIdAsync(editionModel);


            Assert.Equal(edition.Id, editionModel.Id);
            Assert.NotNull(mock.Object);
        }

        [Fact]
        public async Task ReturnResultPrintingEditionUpdate()
        {
            //Arrange
            int id = 1;
            PrintingEdition edition = MoqFile.MoqPrintingEditionModel.GetPrintingEdition();
            List<PrintingEdition> editions = new List<PrintingEdition>();
            editions.Add(edition);

            var mockAuthor = new Mock<IAuthorRepository>();
            var mockMapper = new Mock<IMapper>();
            var mock = new Mock<IPrintingEditionRepository>();

            mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(editions.Where(t => t.Id == id).FirstOrDefault());

            mock.Setup(s => s.SaveChagesAsync());

            var service = new PrintingEditionService(mockMapper.Object, mock.Object, mockAuthor.Object);

            PrintingEditionModel reqestModel = MoqFile.MoqPrintingEditionModel.GetPrintingEditionModel();

            bool act;

            try
            {
                await service.UpdateAsync(reqestModel);
                act = true;
            }
            catch (System.Exception)
            {

                throw;
            }

            //Assert
            Assert.True(act);
        }
    }
}
