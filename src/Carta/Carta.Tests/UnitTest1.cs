using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carta.Core;
using System.Collections.Generic;
using System.Linq;

namespace Carta.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GridInitialLineStateTest()
        {
            /*
                 c0 c1 c2 c3
             r0 [ o  .  o  . ]
             r1 [ o  .  o  o ]
             r2 [ .  .  o  . ]             
             */
            var grid = new bool[4, 3] {
                { true, true, false },
                { false, false, false },
                { true, true, true},
                { false, true, false },
            };

            var cartaGrid = new CartaGrid(grid);

            Assert.IsTrue(cartaGrid.Rows.All(r => !r.Completed));
            Assert.IsTrue(cartaGrid.Columns.Where(c => c.Index != 1).All(c => !c.Completed));
            Assert.AreEqual(true, cartaGrid.Columns[1].Completed);
        }

        [TestMethod]
        public void Grid_3x3_BuiltTest()
        {
            /*
                 c0 c1 c2
             r0 [ o  .  o ]
             r1 [ o  o  o ]
             r2 [ .  .  . ]             
             */
            var grid = new bool[3, 3] {
                { true, true, false },
                { false, true, false },
                { true, true, false },
            };

            var colsBlocks = new[] { new[] { 2 }, new[] { 1 }, new[] { 2 } };
            var rowsBlocks = new[] { new[] { 1, 1 }, new[] { 3 }, new[] { 0 } };
            TestGrid(grid, colsBlocks, rowsBlocks);
        }

        [TestMethod]
        public void Grid_4x3_BuildTest()
        {
            /*
                 c0 c1 c2 c3
             r0 [ o  .  o  . ]
             r1 [ o  .  o  o ]
             r2 [ .  .  o  . ]             
             */
            var grid = new bool[4, 3] {
                { true, true, false },
                { false, false, false },
                { true, true, true},
                { false, true, false },
            };

            var colsBlocks = new[] { new[] { 2 }, new[] { 0 }, new[] { 3 }, new[] { 1 } };
            var rowsBlocks = new[] { new[] { 1, 1 }, new[] {1, 2 }, new[] { 1 } };
            TestGrid(grid, colsBlocks, rowsBlocks);
        }

        private void TestGrid(bool[,] grid, int[][] colsBlocks, int[][] rowsBlocks)
        {
            var cartaGrid = new CartaGrid(grid);
            Assert.AreEqual(grid.GetLength(0), cartaGrid.Columns.Count);
            Assert.AreEqual(grid.GetLength(1), cartaGrid.Rows.Count);


            var cols = new Tuple<int, int, bool>[grid.GetLength(0)][];
            var rows = new Tuple<int, int, bool>[grid.GetLength(1)][];
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                cols[x] = new Tuple<int, int, bool>[grid.GetLength(1)];
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    if (x == 0)
                    {
                        rows[y] = new Tuple<int, int, bool>[grid.GetLength(0)];
                    }
                    var filled = grid[x, y];

                    cols[x][y] = Tuple.Create(x, y, filled);
                    rows[y][x] = Tuple.Create(x, y, filled);
                }
            }

            for (var i = 0; i < cartaGrid.Columns.Count; i++)
            {
                AssertLine(colsBlocks[i], cols[i], cartaGrid.Columns[i]);
            }

            for (var i = 0; i < cartaGrid.Rows.Count; i++)
            {
                AssertLine(rowsBlocks[i], rows[i], cartaGrid.Rows[i]);
            }
        }

        private void AssertLine(int[] blocks, Tuple<int, int, bool>[] cells, CartaLine line)
        {
            Assert.AreEqual(blocks.Length, line.Blocks.Count);
            Assert.AreEqual(cells.Length, line.Cells.Count);

            for (var i = 0; i < blocks.Length; i++)
            {
                Assert.AreEqual(blocks[i], line.Blocks[i]);
            }

            for (var i = 0; i < cells.Length; i++)
            {
                Assert.AreEqual(cells[i].Item1, line.Cells[i].X);
                Assert.AreEqual(cells[i].Item2, line.Cells[i].Y);
                Assert.AreEqual(cells[i].Item3, line.Cells[i].Filled);
            }
        }
    }
}
