using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        private int[,] Inputs = new int[9, 9];
        private int[,] Solution = new int[9, 9];
        private bool[,] PartOfQuestion = new bool[9, 9];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The event handler method that takes place when user clicks the "Solve" button.
        /// </summary>
        private void solveButton_Click(object sender, EventArgs e)
        {
            PopulatePuzzlesStates();
            SetUpPartOfQuestion();
            Solver();

            #region Assigner
            /*
            // assign the values back to the textBoxes;
            int collectionIterationIndex = 0;
            List<int> InputCopy = Inputs.;
            InputCopy.Reverse();
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = InputCopy[collectionIterationIndex].ToString();
                    collectionIterationIndex++;
                }
            }
            */
            #endregion
            solveButton.Enabled = false;
        }

        /// <summary>
        /// This method extracts the state of the textbox controls and copies their values to a list.
        /// </summary>
        private void PopulatePuzzlesStates()
        {
            int cellIndex = 0;
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        Inputs[cellIndex / 9, cellIndex % 9] = -1;
                    }
                    else
                    {
                        Inputs[cellIndex / 9, cellIndex % 9] = Convert.ToInt32(ctrl.Text);
                    }
                    cellIndex++;
                }
            }
            Array.Copy(Inputs, Solution, 81);
        }

        /// <summary>
        /// This Mthods records the intial state of the puzzle before the solution starts.
        /// </summary>
        private void SetUpPartOfQuestion()
        {
            for (int cell = 0; cell < 81; cell++)
            {
                if (Inputs[cell / 9, cell % 9] == -1)
                {
                    PartOfQuestion[cell / 9, cell % 9] = false;
                }
                else
                {
                    PartOfQuestion[cell / 9, cell % 9] = true;
                }
            }
        }

        /// <summary>
        /// This methods solves the current puzzle state, using a simple traceback algorithm.
        /// </summary>
        private void Solver()
        {
            int[] answers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    int answerIndex = 0;
                    if (!PartOfQuestion[row, column])
                    {
                        do
                        {
                            try
                            {
                                Solution[row, column] = answers[answerIndex];
                                answerIndex++;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                // backtrace routine
                                Solution[row, column] = -1;
                                do
                                {
                                    // check if the cell is part of the question, don't modify if true
                                    if (!PartOfQuestion[row, column])
                                    {
                                        // keep moving to the left and adding value untill valid
                                        try
                                        {
                                            column--;
                                            answerIndex++;
                                            // check if the cell is part of the question, don't modify if true
                                            if (!PartOfQuestion[row, column])
                                                Solution[row, column] = answers[answerIndex];
                                            else
                                                column--;
                                        }
                                        // if you can't move left anymore, move up
                                        catch (IndexOutOfRangeException)
                                        {
                                            row--;
                                            column = 8;
                                        }
                                        finally
                                        {
                                            if (!PartOfQuestion[row, column])
                                                Solution[row, column] = answers[answerIndex];
                                        }
                                    }
                                    // if cell is part of the question, move one step to the left
                                    else
                                    {
                                        column--;
                                    }
                                } while (!MoveIsValid(Solution[row, column], column, row));
                            }
                        } while (!MoveIsValid(Solution[row, column], column, row));
                    }
                    // debug statements
                    Console.WriteLine("Cell Index: " + $"{(row * 9) + column} -- {Solution[row, column]}");
                }
            }
        }

        /// <summary>
        /// Checks if a move at the specified coordinates is valid.
        /// </summary>
        /// <param name="move">The value that was assigned at the coordinate</param>
        /// <param name="column">The column that the move took place in</param>
        /// <param name="row">The row that the move took place in</param>
        /// <returns>{bool} whether the move made at coordinate is valid</returns>
        private bool MoveIsValid(int move, int column, int row)
        {
            bool isValid = true;
            // check the row for same value
            for (int x = 0; x < 9; x++)
            {
                if (move == Solution[row, x])
                {
                    if (x != column)
                        isValid = false;
                }
            }

            // check the column for same value
            for (int y = 0; y < 9; y++)
            {
                if (move == Solution[y, column])
                {
                    if (y != row)
                        isValid = false;
                }
            }

            // check the box for same value
            List<int> cellBlock = new List<int>(9);
            for (int vertical = 0; vertical < 3; vertical++)
            {
                for (int horizontal = 0; horizontal < 3; horizontal++)
                {
                    cellBlock.Add(
                        Solution[
                            (Utils.Utils.FindCellBlock(row, column)[0] * 3) + vertical,
                            (Utils.Utils.FindCellBlock(row, column)[1] * 3) + horizontal
                        ]);
                }
            }
            cellBlock.RemoveAll(new Predicate<int>(x => x == -1));
            var duplicates = cellBlock.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);
            if (duplicates.Count() > 0)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
