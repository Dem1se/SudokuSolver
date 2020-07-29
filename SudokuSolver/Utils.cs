using System;

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
}