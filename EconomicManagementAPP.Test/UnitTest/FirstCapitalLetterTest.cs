using EconomicManagementAPP.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Test
{
    [TestClass]
    public class FirstCapitalLetterTest
    {
        [TestMethod]
        //primera letra en minuscula debe dar error
        public void FirstLetterLower_ReturnError()
        {
            var firstCapitalLetter = new FirstCapitalLetter();
            // parametros dde prueba
            var data = "tarjeta";

            var context = new ValidationContext(new { Name = data });

            // ejecutamos el test
            var testResult = firstCapitalLetter.GetValidationResult(data, context);

            // ? significa que acepta null
            Assert.AreEqual("The first letter must be in uppercase", testResult?.ErrorMessage);
        }

        [TestMethod]
        //primera letra en minuscula debe dar error
        public void nullData_NoErrorMessage()
        {
            var firstCapitalLetter = new FirstCapitalLetter();
            // parametros dde prueba
            string data = null;

            var context = new ValidationContext(new { Name = data });

            // ejecutamos el test
            var testResult = firstCapitalLetter.GetValidationResult(data, context);

            // ? significa que acepta null
            Assert.IsNull(testResult);
        }
    }
}