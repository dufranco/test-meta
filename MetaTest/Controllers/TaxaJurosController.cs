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
    public class TaxaJurosController : ControllerBase
    {
        private readonly ITaxaJurosService _taxaJurosService;

        public TaxaJurosController()
        {
            _taxaJurosService = new TaxaJurosService();
        }

        [Route("/taxa-juros")]
        [HttpGet]
        public virtual IActionResult GetTaxaJuros()
        {
            try
            {
                return Ok(_taxaJurosService.GetTaxaJurosCompostos());
            }
            catch
            {
                return Problem(
                    "Não foi possível obter a taxa de juros compostos, tente novamente mais tarde ou contate o administrador do sistema.",
                    statusCode : StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
