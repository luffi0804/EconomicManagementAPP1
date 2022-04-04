using EconomicManagementAPP.IRepositories;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepositorieCategories repositorieCategories;

       
        public CategoriesController(IRepositorieCategories repositorieCategories)
        {
            this.repositorieCategories = repositorieCategories;
        }

        // Ejecuta la accion del index de categories
        public async Task<IActionResult> Index()
        {
            var categories = await repositorieCategories.getCategories();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return View(categories);
            }

            // Validamos si ya existe antes de registrar
            var categoriesExist =
               await repositorieCategories.Exist(categories.Name);

            if (categoriesExist)
            {

                ModelState.AddModelError(nameof(categories.Name),
                    $"The categorie {categories.Name} already exist.");

                return View(categories);
            }
            await repositorieCategories.Create(categories);

            return RedirectToAction("Index");
        }

        // Hace que la validacion se active automaticamente desde el front
        [HttpGet]
        public async Task<IActionResult> VerificaryCategorie(string Name)
        {
            var categoriesExist = await repositorieCategories.Exist(Name);

            if (categoriesExist)
            {

                return Json($"The categories {Name} already exist");
            }

            return Json(true);
        }


        //Retorna la vista tanto del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var categorie = await repositorieCategories.getCategoriesById(Id);

            if (categorie is null)
            {

                return RedirectToAction("NotFound", "Home");
            }

            return View(categorie);
        }
        //Modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(Categories categories)
        {
            var categorie = await repositorieCategories.getCategoriesById(categories.Id);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieCategories.Modify(categories);
            return RedirectToAction("Index");
        }

        // Ejecuta la interfaz del delete
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var categorie = await repositorieCategories.getCategoriesById(Id);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(categorie);
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteCategories(int Id)
        {
            var categorie = await repositorieCategories.getCategoriesById(Id);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieCategories.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
