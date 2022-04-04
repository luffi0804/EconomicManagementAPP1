﻿using EconomicManagementAPP.IRepositories;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly IRepositorieTransactions repositorieTransactions;

        //Inicializamos l variable repositorieAccountTypes para despues inyectarle las funcionalidades de la interfaz
        public TransactionsController(IRepositorieTransactions repositorieTransactions)
        {
            this.repositorieTransactions = repositorieTransactions;
        }

        // Ejecutar la interfaz de transaction
        public async Task<IActionResult> Index()
        {
            var transactions = await repositorieTransactions.getTransactions();
            return View(transactions);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transactions transactions)
        {
            if (!ModelState.IsValid)
            {
                return View(transactions);
            }
            
            await repositorieTransactions.Create(transactions);
           
            return RedirectToAction("Index");
        }        




        //Actualizar
        //Retorna la vista del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var transaction = await repositorieTransactions.getTransactionsById(Id);

            if (transaction is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(transaction);
        }
        //Modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(Transactions transactions)
        {
            var transaction = await repositorieTransactions.getTransactionsById(transactions.Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Modify(transactions);
            return RedirectToAction("Index");
        }
        // Retorna la interfaz de delete
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var transaction = await repositorieTransactions.getTransactionsById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(transaction);
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int Id)
        {
            var transaction = await repositorieTransactions.getTransactionsById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
