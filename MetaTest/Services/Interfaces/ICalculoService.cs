using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaTest.Services.Interfaces
{
    public interface ICalculoService
    {
        public decimal CalculaJuros(decimal valorInicial, int meses, double taxaJuros);
        public string GetRepositorioGitHub();
    }
}
