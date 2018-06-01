using FluentAssertions;
using NUnit.Framework;
using System;

namespace NDDTwitter.Infra.Tests.Extension_Methods
{
    [TestFixture]
    public class CalcTimeTests
    {
        private CalcTime _utils;

        [SetUp]
        public void Initilaze()
        {
            _utils = new CalcTime();
        }

        [Test]
        public void Should_Show_Minute_Ago()
        {
            DateTime t = DateTime.Now.AddMinutes(-1);
            string value = _utils.CalculateTime(t);
            value.Should().Be("1 minuto atrás");
            // Assert.AreEqual(value, "1 minuto atrás");
        }

        [Test]
        public void Should_Show_Minutes_Ago()
        {
            DateTime t = DateTime.Now.AddMinutes(-30);
            string value = _utils.CalculateTime(t);
            value.Should().Be("30 minutos atrás");
        }

        [Test]
        public void Should_Show_Hour_Ago()
        {
            DateTime t = DateTime.Now.AddMinutes(-67);
            string value = _utils.CalculateTime(t);
            value.Should().Be("1 Hora atrás");
        }

        [Test]
        public void Should_Show_Hours_Ago()
        {
            DateTime t = DateTime.Now.AddMinutes(-127);
            string value = _utils.CalculateTime(t);
            value.Should().Be("2 Horas atrás");
        }

        [Test]
        public void Should_Show_Day_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-1);
            string value = _utils.CalculateTime(t);
            value.Should().Be("1 dia atrás");
        }

        [Test]
        public void Should_Show_Days_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-2);
            string value = _utils.CalculateTime(t);
            value.Should().Be("2 dias atrás");
        }

        [Test]
        public void Should_Show_Week_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-7);
            string value = _utils.CalculateTime(t);
            value.Should().Be("1 semana atrás");
        }

        [Test]
        public void Should_Show_Weeks_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-15);
            string value = _utils.CalculateTime(t);
            value.Should().Be("2 semanas atrás");
        }

        [Test]
        public void Should_Show_Month_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-37);
            string value = _utils.CalculateTime(t);
            value.Should().Be("1 mês atrás");
        }

        [Test]
        public void Should_Show_Months_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-107);
            string value = _utils.CalculateTime(t);
            value.Should().Be("3 meses atrás");
        }

        [Test]
        public void Should_Show_Year_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-500);
            string value = _utils.CalculateTime(t);
            value.Should().Be("1 ano atrás");
        }

        [Test]
        public void Should_Show_Years_Ago()
        {
            DateTime t = DateTime.Now.AddDays(-735);
            string value = _utils.CalculateTime(t);
            value.Should().Be("2 anos atrás");
        }
    }
}
