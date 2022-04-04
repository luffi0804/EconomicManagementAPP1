﻿using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.IRepositories;

namespace EconomicManagementAPP.Services
{

    public class RepositorieCategories : IRepositorieCategories
    {
        private readonly string connectionString;

        public RepositorieCategories(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Categories categories)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Categories 
                                                (Name, OperationTypeId, UserId) 
                                                VALUES (@Name, @OperationTypeId, @UserId); SELECT SCOPE_IDENTITY();", categories);
            categories.Id = id;
        }


        public async Task<bool> Exist(string Name)
        {
            using var connection = new SqlConnection(connectionString);
            // El select 1 es traer lo primero que encuentre y el default es 0
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                    @"SELECT 1
                                    FROM Categories
                                    WHERE Name = @Name;",
                                    new { Name });
            return exist == 1;
        }

        // Obtenemos las cuentas del usuario
        public async Task<IEnumerable<Categories>> getCategories()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categories>(@"SELECT Id, Name, OperationTypeId, UserId
                                                            FROM Categories");
        }



        // Actualizar
        public async Task Modify(Categories categories)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Categories
                                            SET Name = @Name, OperationTypeId=@OperationTypeId, UserId=@UserId
                                            WHERE Id = @Id", categories);
        }

        //Para actualizar se necesita obtener el tipo de cuenta por el id
        public async Task<Categories> getCategoriesById(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Categories>(@"
                                                                SELECT Id, Name, OperationTypeId, UserId
                                                                FROM Categories
                                                                WHERE Id = @Id",
                                                                new { Id });
        }

        //Delete
        public async Task Delete(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Categories WHERE Id = @Id", new { Id });
        }

    }
}
