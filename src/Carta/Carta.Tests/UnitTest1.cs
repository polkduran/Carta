using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carta.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var grid = new bool[3, 3] {
                { false, true, false },
                { true, true, false },
                { false, true, false },
            };

            var cartaGrid = new Carta.Core.CartaGrid(grid);
            Assert.IsNotNull(cartaGrid);
        }
    }
}
