using EconomicManagementAPP.IRepositories;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IRepositorieAccounts repositorieAccounts;

        public AccountsController(IRepositorieAccounts repositorieAccounts)
        {
            this.repositorieAccounts = repositorieAccounts;
        }

        // Ejecuta la accion del index de account
        public async Task<IActionResult> Index()
        {            
            var accounts = await repositorieAccounts.getAccounts();
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]

        public async Task<IActionResult> Create(Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return View(accounts);
            }

            // Validamos si ya existe antes de registrar
            var accountExist =
               await repositorieAccounts.Exist(accounts.Name);

            if (accountExist)
            {

                ModelState.AddModelError(nameof(accounts.Name),
                    $"The account {accounts.Name} already exist.");

                return View(accounts);
            }
            await repositorieAccounts.Create(accounts);

            return RedirectToAction("Index");
        }

        //Valida automaticamente desde el front
        [HttpGet]
        public async Task<IActionResult> VerificaryAccount(string Name)
        {           
            var accountExist = await repositorieAccounts.Exist(Name);

            if (accountExist)
            {
               
                return Json($"The account {Name} already exist");
            }

            return Json(true);
        }

        //Actualizar
        //Retorna la vista del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {            
            var account = await repositorieAccounts.getAccountsById(Id);

            if (account is null)
            {

                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }
        //Ejecuta la accion del modify
        [HttpPost]
        public async Task<ActionResult> Modify(Accounts accounts)
        {           
            var account = await repositorieAccounts.getAccountsById(accounts.Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Modify(accounts);
            return RedirectToAction("Index");
        }
        // Eliminar
        //Retorna la vista de Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {            
            var account = await repositorieAccounts.getAccountsById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }

        //Ejecuta la accion del delete
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int Id)
        {            
            var account = await repositorieAccounts.getAccountsById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
