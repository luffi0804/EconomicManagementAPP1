using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.IRepositories;

namespace EconomicManagementAPP.Services
{


    public class RepositorieAccounts : IRepositorieAccounts
    {
        private readonly string connectionString;

        public RepositorieAccounts(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //creamos la cuenta
        public async Task Create(Accounts accounts)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Accounts 
                                                (Name, AccountTypeId, Balance, Description) 
                                                VALUES (@Name, @AccountTypeId, @Balance, @Description); SELECT SCOPE_IDENTITY();", accounts);
            accounts.Id = id;
        }

        //Verificamos si existe
        public async Task<bool> Exist(string Name)
        {
            using var connection = new SqlConnection(connectionString);

            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                    @"SELECT 1
                                    FROM Accounts
                                    WHERE Name = @Name;",
                                    new { Name });
            return exist == 1;
        }

        //Devuelve la lista
        public async Task<IEnumerable<Accounts>> getAccounts()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Accounts>(@"SELECT Id, Name, AccountTypeId, Balance, Description
                                                            FROM Accounts");
        }



        // Actualizar
        public async Task Modify(Accounts accounts)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Accounts
                                            SET Name = @Name, AccountTypeId=@AccountTypeId, Balance=@Balance, Description=@Description
                                            WHERE Id = @Id", accounts);
        }

        //Para actualizar se necesita obtener el tipo de cuenta por el id
        public async Task<Accounts> getAccountsById(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Accounts>(@"
                                                                SELECT Id, Name, AccountTypeId, Balance, Description
                                                                FROM Accounts
                                                                WHERE Id = @Id",
                                                                new { Id });
        }

        //Delete
        public async Task Delete(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Accounts WHERE Id = @Id", new { Id });
        }
       
    }
}
