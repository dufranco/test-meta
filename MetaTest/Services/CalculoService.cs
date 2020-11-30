using MetaTest.Services.Constants;
using MetaTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaTest.Services
{
    public class CalculoService : ICalculoService
    {
        public decimal CalculaJuros(decimal valorInicial, int meses, double taxaJuros)
        {
            var a = Convert.ToDouble(valorInicial) * Math.Pow((1 + taxaJuros), meses);
            var result = Convert.ToDecimal(Math.Truncate(100 * a) / 100);

            if (result < valorInicial)
                throw new ArgumentException("Cálculo incorreto, verifique os paarâmetros informados e tente novamente.");

            return result;
        }

        public string GetRepositorioGitHub() => SitesConstants.REPOSITORIO_GITHUB;
    }
}
