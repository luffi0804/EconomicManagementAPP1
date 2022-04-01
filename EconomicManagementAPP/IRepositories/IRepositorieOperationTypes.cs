using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{
    public interface IRepositorieOperationTypes
    {
        Task Create(OperationTypes operationTypes); // Se agrega task por el asincronismo
        Task<bool> Exist(string Description);
        Task<IEnumerable<OperationTypes>> getOperationTypes();
        Task Modify(OperationTypes operationTypes);
        Task<OperationTypes> getOperationTypesById(int Id); // para el modify
        Task Delete(int Id);
    }
}
