using MetaTest.Controllers;
using MetaTest.Services;
using MetaTest.Services.Constants;
using MetaTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;
using System;

namespace MetaTestNUnit.Controllers
{
    public class CalculoControllerTest
    {
        #region Controllers
        private CalculoController _calculoController;
        #endregion

        #region Valores
        private decimal _valorInicialValido = 100.00M;
        private int _mesesValido = 5;

        private decimal _valorInicialInvalido = -100.00M;
        private int _mesesInvalido = -5;
        #endregion

        #region Constantes
        private const string ERRO_ARGUMENTO_CALCULA_JUROS = "Cálculo incorreto, verifique os paarâmetros informados e tente novamente.";
        private const string ERRO_GET_TAXA_JUROS = "Não foi possível obter a taxa de juros compostos, tente novamente mais tarde ou contate o administrador do sistema.";
        #endregion

        [SetUp]
        public void Setup()
        {
            _calculoController = new CalculoController();
        }

        [Test]
        public void Sucesso_Get_ShowMeTheCode()
        {
            var actionResult = _calculoController.GetShowMeTheCode();

            Assert.IsTrue(actionResult is OkObjectResult);

            var result = (actionResult as OkObjectResult).Value;
            var expected = SitesConstants.REPOSITORIO_GITHUB;

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Sucesso_Get_CalculaJuros_Valores_Validos()
        {
            var actionResult = _calculoController.GetCalculaJuros(_valorInicialValido, _mesesValido);

            Assert.IsTrue(actionResult is OkObjectResult);

            var result = (actionResult as OkObjectResult).Value;
            var expected = 105.10M;

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Falha_Get_CalculaJuros_Param_ValorInicial_Invalido()
        {
            var actionResult = _calculoController.GetCalculaJuros(_valorInicialInvalido, _mesesValido);

            Assert.IsTrue(actionResult is BadRequestObjectResult);

            var result = (actionResult as BadRequestObjectResult).Value;

            Assert.AreEqual(result, ERRO_ARGUMENTO_CALCULA_JUROS);
        }

        [Test]
        public void Falha_Get_CalculaJuros_Param_Meses_Invalido()
        {
            var actionResult = _calculoController.GetCalculaJuros(_valorInicialValido, _mesesInvalido);

            Assert.IsTrue(actionResult is BadRequestObjectResult);

            var result = (actionResult as BadRequestObjectResult).Value;

            Assert.AreEqual(result, ERRO_ARGUMENTO_CALCULA_JUROS);
        }

        [Test]
        public void Falha_Get_CalculaJuros_Falha_Get_TaxaJuros()
        {
            var problemDetailsFactory = new Mock<ProblemDetailsFactory>();

            problemDetailsFactory.Setup(x => x.CreateProblemDetails(
                    It.IsAny<Microsoft.AspNetCore.Http.HttpContext>(),
                    It.IsAny<int?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )
            ).Returns(new ProblemDetails
            {
                Detail = ERRO_GET_TAXA_JUROS,
                Status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
            });

            var problemDetails = problemDetailsFactory.Object.CreateProblemDetails(
                It.IsAny<Microsoft.AspNetCore.Http.HttpContext>(),
                statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                title: ERRO_GET_TAXA_JUROS
            );

            var calculoController = new Mock<CalculoController>();
            calculoController
                .Setup(x => x.GetCalculaJuros(It.IsAny<decimal>(), It.IsAny<int>()))
                .Returns(new ObjectResult(problemDetails));

            var actionResult = calculoController.Object.GetCalculaJuros(_valorInicialValido, _mesesValido);

            Assert.IsTrue(actionResult is ObjectResult);

            var result = (actionResult as ObjectResult).Value as ProblemDetails;

            Assert.AreEqual(result.Detail, ERRO_GET_TAXA_JUROS);
            Assert.AreEqual(result.Status, Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
        }
    }
}