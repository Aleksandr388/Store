using Amazon.Runtime.Internal.Util;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Store.DataAcess.Entities;
using Store.DataAcess.Options;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.DapperRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.DapperRepositories
{
    public class AuthorDapperRepository : BaseDapperRepository<Author>, IAuthorDapperRepository
    {
        private readonly ConnectionStrings _connectionString;

        public AuthorDapperRepository(IOptions<ConnectionStrings> options) : base(options)
        {
            _connectionString = options.Value;
        }

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                string query = @"SELECT a.*, p.* 
                               FROM Authors a
                               LEFT JOIN AuthorPrintingEdition ap ON ap.AuthorsId = a.Id 
                               LEFT JOIN PrintingEditions p on p.Id = ap.PrintingEditionsId";

                var authors = await connection.QueryAsync<Author, PrintingEdition, Author>(query,
                    (Author, PrintingEdition) =>
                    {
                        if (PrintingEdition != null)
                        {
                            Author.PrintingEditions.Add(PrintingEdition);
                        }
                        return Author;
                    });

                return authors;
            }
        }

        public override async Task<Author> GetByIdAsync(long id)
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                string query = $@"SELECT a.*, p.* 
                               FROM Authors a
                               LEFT JOIN AuthorPrintingEdition ap ON ap.AuthorsId = a.Id 
                               LEFT JOIN PrintingEditions p on p.Id = ap.PrintingEditionsId
                               WHERE a.Id = {id}";

                var author = await connection.QueryAsync<Author, PrintingEdition, Author>(query,
                    (Author, PrintingEdition) =>
                    {
                        if (PrintingEdition is not null)
                        {
                            Author.PrintingEditions.Add(PrintingEdition);
                        }
                        return Author;
                    });

                return author.FirstOrDefault();
            }
        }
        public override async Task UpdateAsync(Author model)
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                var pEModels = new List<PrintingEdition>(model.PrintingEditions);

                string query = $@"SELECT a.*, p.* 
                               FROM Authors a
                               LEFT JOIN AuthorPrintingEdition ap ON ap.AuthorsId = a.Id 
                               LEFT JOIN PrintingEditions p on p.Id = ap.PrintingEditionsId
                               WHERE a.Id = {model.Id}";

                var authors = await connection.QueryAsync<Author, PrintingEdition, Author>(query,
                    (Author, PrintingEdition) =>
                    {
                        if (PrintingEdition is not null)
                        {
                            Author.PrintingEditions.Add(PrintingEdition);
                        }
                        return Author;
                    });

                var authorForUpdate = authors.FirstOrDefault();

                await connection.UpdateAsync(model);

                if (pEModels is not null)
                {
                    await connection.UpdateAsync(pEModels);
                }

                var editionIdss = new List<long>();

                foreach (var item in pEModels)
                {

                    editionIdss.Add(item.Id);
                }

                var count = editionIdss.Count();

                var authorsIds = new long[] { model.Id };
                authorsIds = Enumerable.Repeat(model.Id, count).ToArray();     

                string qeryForDelete = $@"DELETE FROM AuthorPrintingEdition
                                       WHERE AuthorPrintingEdition.AuthorsId = {model.Id}";

                await connection.QueryAsync(qeryForDelete);

                foreach (var item in editionIdss)
                {
                    var queryForUpdateRelatingShip = $@"INSERT INTO AuthorPrintingEdition (AuthorsId, PrintingEditionsId) VALUES ({model.Id}, {item})";

                    await connection.QueryAsync(queryForUpdateRelatingShip);
                }

            }
        }
    }
}
