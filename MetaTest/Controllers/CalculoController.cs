using MetaTest.Services;
using MetaTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {
        #region Controllers
        private readonly TaxaJurosController _taxaJurosController;
        #endregion

        #region Services
        private readonly ICalculoService _calculoService;
        #endregion

        public CalculoController()
        {
            _taxaJurosController = new TaxaJurosController();
            _calculoService = new CalculoService();
        }

        [Route("/calcula-juros")]
        [HttpGet]
        public virtual IActionResult GetCalculaJuros(decimal valorInicial, int meses)
        {
            try
            {
                var result = _taxaJurosController.GetTaxaJuros();

                if (result is ProblemDetails)
                    return result;

                var taxaJuros = Convert.ToDouble((result as OkObjectResult).Value);

                return Ok(_calculoService.CalculaJuros(valorInicial, meses, taxaJuros));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro inesperado."
                );
            }
        }

        [Route("/show-me-the-code")]
        [HttpGet]
        public IActionResult GetShowMeTheCode()
        {
            return Ok(_calculoService.GetRepositorioGitHub());
        }
    }
}
