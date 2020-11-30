using MetaTest.Services;
using MetaTest.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace MetaTestNUnit.Services
{
    public class TaxaJurosServiceTest
    {
        #region Services
        private ITaxaJurosService _taxaJurosService;
        #endregion

        #region Valores
        private decimal _valorInicialValido = 100.00M;
        private int _mesesValido = 5;
        private double _taxaJurosValido = 0.01;

        private decimal _valorInicialInvalido = -100.00M;
        private int _mesesInvalido = -5;
        private double _taxaJurosInvalido = -0.01;
        #endregion

        [SetUp]
        public void Setup()
        {
            _taxaJurosService = new TaxaJurosService();
        }

        [Test]
        public void Sucesso_GetTaxaJurosCompostos()
        {
            var result = _taxaJurosService.GetTaxaJurosCompostos();
            var expected = 0.01;

            Assert.AreEqual(expected, result);
        }
    }
}