using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositories
{
    public interface IRepositorieUsers
    {
        Task Create(Users users); // Se agrega task por el asincronismo
        Task<bool> Exist(string Email); //Hacemos asincrona la validacion de un email existente
        Task<IEnumerable<Users>> getUser(); //Creamos un metodo al cual despues llamamos para la creacion de la lista
        Task Modify(Users users);
        Task<Users> getUserById(int Id); // para el modify
        Task<Users> Login(string Email, string Password);
        Task Delete(int Id);
    }
}
