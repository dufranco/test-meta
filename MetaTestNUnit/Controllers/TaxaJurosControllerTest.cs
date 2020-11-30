using MetaTest.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;

namespace MetaTestNUnit.Controllers
{
    public class TaxaJurosControllerTest
    {
        #region Controllers
        private TaxaJurosController _taxaJurosController;
        #endregion

        #region Constantes
        private const string ERRO_GET_TAXA_JUROS = "Não foi possível obter a taxa de juros compostos, tente novamente mais tarde ou contate o administrador do sistema.";
        #endregion

        [SetUp]
        public void Setup()
        {
            _taxaJurosController = new TaxaJurosController();
        }

        [Test]
        public void Sucesso_Get_TaxaJuros()
        {
            var actionResult = _taxaJurosController.GetTaxaJuros();
            var expected = 0.01M;

            Assert.IsTrue(actionResult is OkObjectResult);

            var result = actionResult as OkObjectResult;

            Assert.AreEqual(result.Value, expected);
        }

        [Test]
        public void Falha_Get_TaxaJuros()
        {
            var problemDetailsFactory = new Mock<ProblemDetailsFactory>();

            problemDetailsFactory.Setup(x => x.CreateProblemDetails(
                    It.IsAny<HttpContext>(),
                    It.IsAny<int?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )
            ).Returns(new ProblemDetails
            {
                Detail = ERRO_GET_TAXA_JUROS,
                Status = StatusCodes.Status500InternalServerError
            });

            var problemDetails = problemDetailsFactory.Object.CreateProblemDetails(
                It.IsAny<HttpContext>(),
                statusCode: StatusCodes.Status500InternalServerError,
                title: ERRO_GET_TAXA_JUROS
            );

            var calculoController = new Mock<TaxaJurosController>();
            calculoController
                .Setup(x => x.GetTaxaJuros())
                .Returns(new ObjectResult(problemDetails));

            var actionResult = calculoController.Object.GetTaxaJuros();

            Assert.IsTrue(actionResult is ObjectResult);

            var result = (actionResult as ObjectResult).Value as ProblemDetails;

            Assert.AreEqual(result.Detail, ERRO_GET_TAXA_JUROS);
            Assert.AreEqual(result.Status, StatusCodes.Status500InternalServerError);
        }
    }
}