using EconomicManagementAPP.IRepositories;
using EconomicManagementAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly IRepositorieAccountTypes repositorieAccountTypes;

       
        public AccountTypesController(IRepositorieAccountTypes repositorieAccountTypes)
        {
            this.repositorieAccountTypes = repositorieAccountTypes;
        }

        // Ejecuta la accion index de accountTypes
        public async Task<IActionResult> Index()
        {
            var userId = 1;
            var accountTypes = await repositorieAccountTypes.getAccounts(userId);
            return View(accountTypes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountTypes accountTypes, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(accountTypes);
            }

            accountTypes.UserId = 1;
            accountTypes.OrderAccount = id;

            // Validamos si ya existe antes de registrar
            var accountTypeExist =
               await repositorieAccountTypes.Exist(accountTypes.Name, accountTypes.UserId);

            if (accountTypeExist)
            {
                ModelState.AddModelError(nameof(accountTypes.Name),
                    $"The account {accountTypes.Name} already exist.");

                return View(accountTypes);
            }
            await repositorieAccountTypes.Create(accountTypes);
            // Redireccionamos a la lista
            return RedirectToAction("Index");
        }

        // Hace que la validacion se active automaticamente desde el front
        [HttpGet]
        public async Task<IActionResult> VerificaryAccountType(string Name)
        {
            var UserId = 1;
            var accountTypeExist = await repositorieAccountTypes.Exist(Name, UserId);

            if (accountTypeExist)
            {
          
                return Json($"The account {Name} already exist");
            }

            return Json(true);
        }

        //Actualizar
        //Retorna la vista del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int id )
        {
            var userId = 1;
            var accountType = await repositorieAccountTypes.getAccountById(id, userId);

            if (accountType is null)
            { 
               
                return RedirectToAction("NotFound", "Home");
            }

            return View(accountType);
        }
        //Modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(AccountTypes accountTypes)
        {
            var userId = 1;
            var accountType = await repositorieAccountTypes.getAccountById(accountTypes.Id, userId);

            if (accountType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccountTypes.Modify(accountTypes);
            return RedirectToAction("Index");
        }
        // Retorna la interfaz de delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = 1;
            var account = await repositorieAccountTypes.getAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = 1;
            var account = await repositorieAccountTypes.getAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccountTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
