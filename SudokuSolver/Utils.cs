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
    }
}