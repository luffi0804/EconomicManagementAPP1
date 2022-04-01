using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{
    public interface IRepositorieCategories
    {
        Task Create(Categories categories); // Se agrega task por el asincronismo
        Task<bool> Exist(string Name);
        Task<IEnumerable<Categories>> getCategories();
        Task Modify(Categories categories);
        Task<Categories> getCategoriesById(int Id); // para el modify
        Task Delete(int Id);
    }
}
