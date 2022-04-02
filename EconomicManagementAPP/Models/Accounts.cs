using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class Accounts
    {
        //Hacemos las validaciones requeridas
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        [Remote(action: "VerificaryAccount", controller: "Accounts")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        public int AccountTypeId { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        public string Balance { get; set; }        
        public string Description { get; set; }
    }
}
