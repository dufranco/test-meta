using MetaTest.Services;
using MetaTest.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace MetaTestNUnit.Services
{
    public class CalculoServiceTest
    {
        #region Services
        private ICalculoService _calculoService;
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
            _calculoService = new CalculoService();
        }

        [Test]
        public void Sucesso_CalculaJuros_Valores_Validos()
        {
            var result = _calculoService.CalculaJuros(_valorInicialValido, _mesesValido, _taxaJurosValido);
            var expected = 105.10M;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Falha_CalculaJuros_Param_ValorInicial_Invalido()
        {
            Assert.Throws<ArgumentException>(() => _calculoService.CalculaJuros(_valorInicialInvalido, _mesesValido, _taxaJurosValido));
        }

        [Test]
        public void Falha_CalculaJuros_Param_Meses_Invalido()
        {
            Assert.Throws<ArgumentException>(() => _calculoService.CalculaJuros(_valorInicialValido, _mesesInvalido, _taxaJurosValido));
        }

        [Test]
        public void Falha_CalculaJuros_Param_TaxaJuros_Invalido()
        {
            Assert.Throws<ArgumentException>(() => _calculoService.CalculaJuros(_valorInicialValido, _mesesValido, _taxaJurosInvalido));
        }
    }
}