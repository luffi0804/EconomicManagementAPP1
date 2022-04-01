using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{
    public interface IRepositorieAccounts
    {
        Task Create(Accounts accounts); // Se agrega task por el asincronismo
        Task<bool> Exist(string Name);
        Task<IEnumerable<Accounts>> getAccounts();
        Task Modify(Accounts accounts);
        Task<Accounts> getAccountsById(int Id); // para el modify
        Task Delete(int Id);
    }
}
