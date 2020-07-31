using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Utils
{
    static class Utils
    {
        public static int[] FindCellBlock(int row, int column)
        {
            int[] CellBlockPosition = new int[2];
            CellBlockPosition[0] = row / 3;
            CellBlockPosition[1] = column / 3;
            return CellBlockPosition;
        }

        public static void PrettyPrintArray(int[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
        }
    }

    static class Verify
    {
        public static bool IsEmpty(int[,] Grid)
        {
            List<int> GridList = new List<int>();
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    GridList.Add(Grid[y, x]);
            GridList.RemoveAll(new Predicate<int>(x => x == 0));
            if (GridList.Count == 0)
                return true;
            else
                return false;
        }
        
        /// <summary> Checks if the puzzle state is valid (There are no violation to the 3 base rules)</summary> 
        /// <param name="Grid">The non-empty puzzle state matrix. Call IsEmpty() before calling this.</param>
        /// <returns>{bool} Whether the puzzle is valid or not</returns>
        public static bool IsValid(int [,] Grid)
        {
            // check the rows for same value
            List<int> row = new List<int>(9);
            for (int y = 0; y < 9; y++)
            {
                row.Clear();
                for (int x = 0; x < 9; x++)
                {
                    row.Add(Grid[y, x]);
                }
                row.RemoveAll(new Predicate<int>(i => i == 0));
                var rowDuplicates = row.GroupBy(i => i)
                    .Where(g => g.Count() > 1)
                    .Select(i => i.Key);
                if (rowDuplicates.Count() > 0)
                    return false;
            }

            // check the column for same value
            List<int> column = new List<int>(9);
            for (int x = 0; x < 9; x++)
            {
                column.Clear();
                for (int y = 0; y < 9; y++)
                {
                    column.Add(Grid[y, x]);
                }
                column.RemoveAll(new Predicate<int>(j => j == 0));
                var columnDuplicates = column.GroupBy(j => j)
                    .Where(g => g.Count() > 1)
                    .Select(j => j.Key);
                if (columnDuplicates.Count() > 0)
                    return false;
            }

            // check the box for same value
            List<int> cellBlock = new List<int>(9);
            for (int y = 0; y <= 6; y += 3)
            {
                for (int x = 0; x <= 6; x += 3)
                {
                    for (int vertical = 0; vertical < 3; vertical++)
                    {
                        for (int horizontal = 0; horizontal < 3; horizontal++)
                        {
                            cellBlock.Add(Grid[y + vertical, x + horizontal]);
                        }
                    }

                    cellBlock.RemoveAll(new Predicate<int>(k => k == 0));
                    var duplicates = cellBlock.GroupBy(k => k)
                        .Where(g => g.Count() > 1)
                        .Select(k => k.Key);
                    if (duplicates.Count() > 0)
                        return false;
                    cellBlock.Clear();
                }
            }

            // return true if the function hasn't returned already -- no rule violations.
            return true;
        }
    }
}