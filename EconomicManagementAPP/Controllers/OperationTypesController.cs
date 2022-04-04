using EconomicManagementAPP.IRepositories;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class OperationTypesController : Controller
    {
        private readonly IRepositorieOperationTypes repositorieOperationTypes;

        public OperationTypesController(IRepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieOperationTypes = repositorieOperationTypes;
        }

        // Ejecutar la interfaz de OperationTypes
        public async Task<IActionResult> Index()
        {
            var operationTypes = await repositorieOperationTypes.getOperationTypes();
            return View(operationTypes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperationTypes operationTypes)
        {

            if (!ModelState.IsValid)
            {
                return View(operationTypes);
            }

            // Validamos si ya existe antes de registrar
            var operationTypeExist =
               await repositorieOperationTypes.Exist(operationTypes.Description);

            if (operationTypeExist)
            {

                ModelState.AddModelError(nameof(operationTypes.Description),
                    $"The operation types {operationTypes.Description} already exist.");

                return View(operationTypes);
            }

            await repositorieOperationTypes.Create(operationTypes);

            return RedirectToAction("Index");
        }

        // Hace que la validacion se active automaticamente desde el front
        [HttpGet]
        public async Task<IActionResult> VerificaryOperationType(string Description)
        {

            var operationType = await repositorieOperationTypes.Exist(Description);

            if (operationType)
            {
                // permite acciones directas entre front y back
                return Json($"The Operation {Description} already exist");
            }

            return Json(true);
        }


        //Retorna la vista tanto del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var operationType = await repositorieOperationTypes.getOperationTypesById(Id);

            if (operationType is null)
            {

                return RedirectToAction("NotFound", "Home");
            }

            return View(operationType);
        }

        //Modify
        [HttpPost]
        public async Task<ActionResult> Modify(OperationTypes operationTypes)
        {
            var operationType = await repositorieOperationTypes.getOperationTypesById(operationTypes.Id);

            if (operationType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Modify(operationTypes);
            return RedirectToAction("Index");
        }

        // Retorna la vista de Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var operation = await repositorieOperationTypes.getOperationTypesById(Id);

            if (operation is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(operation);
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> DeleteOperation(int Id)
        {
            var operation = await repositorieOperationTypes.getOperationTypesById(Id);

            if (operation is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}

