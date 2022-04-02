using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    //Hacemos las validaciones requeridas
    public class OperationTypes
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        [Remote(action: "VerificaryOperationType", controller: "OperationTypes")]
        public string Description { get; set; }        

    }
}
