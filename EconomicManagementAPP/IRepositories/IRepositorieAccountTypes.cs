using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{
    public interface IRepositorieAccountTypes
    {
        Task Create(AccountTypes accountTypes);
        Task<bool> Exist(string Name, int UserId);
        Task<IEnumerable<AccountTypes>> getAccounts(int UserId);
        Task Modify(AccountTypes accountTypes);
        Task<AccountTypes> getAccountById(int id, int userId);
        Task Delete(int id);
    }
}
