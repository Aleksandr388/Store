using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Store.DataAcess.Entities;
using Store.DataAcess.Options;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.DapperRepositories.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.DapperRepositories
{
    public class PrintingEditionDapperRepository : BaseDapperRepository<PrintingEdition>, IPrintingEditionDapperRepository
    {
        private readonly ConnectionStrings _connectionString;

        public PrintingEditionDapperRepository(IOptions<ConnectionStrings> options) : base(options)
        {
            _connectionString = options.Value;
        }

        public override async Task<IEnumerable<PrintingEdition>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                string query = @"SELECT a.*, p.* 
                               FROM Authors a
                               LEFT JOIN AuthorPrintingEdition ap ON ap.AuthorsId = a.Id 
                               LEFT JOIN PrintingEditions p on p.Id = ap.PrintingEditionsId";

                var editions = await connection.QueryAsync<PrintingEdition, Author, PrintingEdition>(query,
                    (PrintingEdition, Author) =>
                    {
                        if (PrintingEdition != null)
                        {
                            PrintingEdition.Authors.Add(Author);
                        }
                        return PrintingEdition;
                    });

                return editions;
            }
        }

        public override async Task<PrintingEdition> GetByIdAsync(long id)
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                string query = $@"SELECT a.*, p.* 
                               FROM Authors a
                               LEFT JOIN AuthorPrintingEdition ap ON ap.AuthorsId = a.Id 
                               LEFT JOIN PrintingEditions p on p.Id = ap.PrintingEditionsId
                               WHERE a.Id = {id}";

                var printingEditions = await connection.QueryAsync<PrintingEdition, Author, PrintingEdition>(query,
                    (PrintingEdition, Author) =>
                    {
                        if (PrintingEdition is not null)
                        {
                            PrintingEdition.Authors.Add(Author);
                        }
                        return PrintingEdition;
                    });

                return printingEditions.FirstOrDefault();
            }
        }
        public override async Task UpdateAsync(PrintingEdition model)
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                var authorsModels = new List<Author>(model.Authors);

                await connection.UpdateAsync(model);

                if (authorsModels is not null)
                {
                    await connection.UpdateAsync(authorsModels);
                }

                var authorsList = new List<long>();

                foreach (var item in authorsModels)
                {
                    authorsList.Add(item.Id);
                }

                string qeryForDelete = $@"DELETE FROM AuthorPrintingEdition
                                       WHERE AuthorPrintingEdition.AuthorsId = {model.Id}";

                await connection.QueryAsync(qeryForDelete);

                foreach (var item in authorsList)
                {
                    var queryForUpdateRelatingShip = $@"INSERT INTO AuthorPrintingEdition (AuthorsId, PrintingEditionsId) VALUES ({model.Id}, {item})";

                    await connection.QueryAsync(queryForUpdateRelatingShip);
                }

            }
        }
    }
}

