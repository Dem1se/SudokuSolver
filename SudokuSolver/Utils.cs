namespace SudokuSolver.Utils
{
    static class Utils
    {
        /// <summary>
        /// finds the coordinate(2D) of the given cell index(1D)
        /// </summary>
        /// <param name="index">The one dimmensional index of the cell</param>
        /// <returns>{Position} struct instance with the 2D coordinates</returns>
        public static Position GetPosition(int index)
        {
            return new Position(index % 9, index / 9);
        }

        /// <summary>
        /// Finds the cell index (1D) of the provided coordinates(2D)
        /// </summary>
        /// <param name="column">the X coordinate of the cell</param>
        /// <param name="row">the Y coordinate of the cell</param>
        /// <returns>{int} 1D cell index of the cell</returns>
        public static int FindIndex(int column, int row)
        {
            return column + (row * 9);
        }
    }
}