using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{

    public interface IRepositorieTransactions
    {
        Task Create(Transactions transactions); // Se agrega task por el asincronismo        
        Task<IEnumerable<Transactions>> getTransactions();
        Task Modify(Transactions transactions);
        Task<Transactions> getTransactionsById(int Id); // para el modify
        Task Delete(int Id);
    }
}
