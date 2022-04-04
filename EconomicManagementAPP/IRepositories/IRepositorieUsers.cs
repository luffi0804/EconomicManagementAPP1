using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{
    public interface IRepositorieUsers
    {
        Task Create(Users users); 
        Task<bool> Exist(string Email); 
        Task<IEnumerable<Users>> getUser(); 
        Task Modify(Users users);
        Task<Users> getUserById(int Id); 
        Task<Users> Login(string Email, string Password);
        Task Delete(int Id);
    }
}
