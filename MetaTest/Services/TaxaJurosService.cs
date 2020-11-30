using MetaTest.Services.Constants;
using MetaTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaTest.Services
{
    public class TaxaJurosService : ITaxaJurosService
    {
        public virtual double GetTaxaJurosCompostos() => TaxaJurosConstants.TAXA_JUROS_COMPOSTOS;
    }
}
