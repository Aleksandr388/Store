using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAcess.Entities;
using System;
using System.Collections.Generic;

namespace Store.Tests.MoqFile
{
    public static class MoqPrintingEditionModel
    {
        public static PrintingEdition GetPrintingEdition()
        {
            PrintingEdition edition = new PrintingEdition()
            {
                Id = 1,
                Authors = new List<Author>(),
                CreationDate = DateTime.Now,
                Curency = Shared.Enums.CurencyType.EUR,
                Description = "",
                IsRemoved = false,
                Price = 100,
                Status = Shared.Enums.StatusType.Available,
                Title = "",
                Type = Shared.Enums.EditionType.Book

            };
            return edition;
        }

        public static PrintingEditionModel GetPrintingEditionModel()
        {
            PrintingEditionModel editionModel = new PrintingEditionModel()
            {
                Id = 1,
                AuthorModels = new List<AuthorModel>(),
                CreationDate = DateTime.Now,
                Curency = Shared.Enums.CurencyType.EUR,
                Description = "",
                IsRemoved = false,
                Price = 100,
                Status = Shared.Enums.StatusType.Available,
                Title = "",
                Type = Shared.Enums.EditionType.Book

            };
            return editionModel;
        }
    }
}
