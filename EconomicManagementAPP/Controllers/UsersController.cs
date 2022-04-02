using EconomicManagementAPP.IRepositories;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepositorieUsers repositorieUsers;

        //Inicializamos  repositorieUsers para despues inyectarle las funcionalidades de la interfaz
        public UsersController(IRepositorieUsers repositorieUsers)
        {
            this.repositorieUsers = repositorieUsers;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Ejecuta la interfaz de LoginView Movil
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await repositorieUsers.Login(loginViewModel.Email, loginViewModel.Password);

            if (result is null)
            {
                ModelState.AddModelError(String.Empty, "Wrong Email or Password");
                return View(loginViewModel);
            }
            else
            {
                return RedirectToAction("Create", "AccountTypes");
            }
        }

        // Ejecuta la interfaz
        public async Task<IActionResult> Index()
        {

            var users = await repositorieUsers.getUser();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(Users users)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }


            // Valida que el usuario existe mediante el email.
            var usersExist =
               await repositorieUsers.Exist(users.Email);

            if (usersExist)
            {
                
                ModelState.AddModelError(nameof(users.Email),
                    $"The account {users.Email} already exist.");

                return View(users);
            }
            await repositorieUsers.Create(users);
           
            return RedirectToAction("Index");
        }


        // Activa la validacion desde el front por medio de el email
        [HttpGet]
        public async Task<IActionResult> VerificaryUsers(string Email)
        {

            var usersExist = await repositorieUsers.Exist(Email);

            if (usersExist)
            {
                return Json($"The account {Email} already exist.");
            }

            return Json(true);
        }

        
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {

            var user = await repositorieUsers.getUserById(Id);

            if (user is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        //Modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(Users users)
        {
            var user = await repositorieUsers.getUserById(users.Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(users);
            }

            await repositorieUsers.Modify(users);
            return RedirectToAction("Index");
        }


        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await repositorieUsers.getUserById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = await repositorieUsers.getUserById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUsers.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
