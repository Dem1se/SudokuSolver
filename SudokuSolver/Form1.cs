using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class MainForm : Form
    {
        private int[,] Grid = new int[9, 9];

        public MainForm()
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
            PopulateGridData();
            if (Utils.Verify.IsEmpty(Grid)) 
            {
                MessageBox.Show("The Puzzle is empty, fill in some values to solve first.", "Empty Puzzle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            if (!Utils.Verify.IsValid(Grid))
            {
                MessageBox.Show("The Puzzle is not valid, check the values.", "Invalid Puzzle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Console.WriteLine("Valid puzzle. Starting Solving.");
            Solver();
        }

        /// <summary>
        /// This method extracts the state of the textbox controls and copies their values to a list.
        /// </summary>
        private void PopulateGridData()
        {
            int cellIndex = 0;
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        Grid[cellIndex / 9, cellIndex % 9] = 0;
                    }
                    else
                    {
                        Grid[cellIndex / 9, cellIndex % 9] = Convert.ToInt32(ctrl.Text);
                    }
                    cellIndex++;
                }
            }
        }

        /// <summary>
        /// This methods solves the current puzzle state, using a simple traceback algorithm.
        /// </summary>
        private void Solver()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (Grid[row, column] == 0)
                    {
                        for (int answer = 1; answer < 10; answer++)
                        {
                            if (MoveIsValid(answer, column, row))
                            {
                                Grid[row, column] = answer;
                                Solver();
                                Grid[row, column] = 0;
                            }
                        }
                        return;
                    }
                }
            }
            Assigner();
        }

        private void Assigner()
        {
            Console.WriteLine("Assigned values back");
            Utils.Utils.PrettyPrintArray(Grid);

            // assign the values back to the textBoxes;
            int collectionIterationIndex = 0;
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = Grid[collectionIterationIndex / 9, collectionIterationIndex % 9].ToString();
                    collectionIterationIndex++;
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
                if (move == Grid[row, x])
                {
                    if (x != column)
                        isValid = false;
                }
            }

            // check the column for same value
            for (int y = 0; y < 9; y++)
            {
                if (move == Grid[y, column])
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
                        Grid[
                            (Utils.Utils.FindCellBlock(row, column)[0] * 3) + vertical,
                            (Utils.Utils.FindCellBlock(row, column)[1] * 3) + horizontal
                        ]);
                }
            }
            cellBlock.RemoveAll(new Predicate<int>(x => x == 0));
            var duplicates = cellBlock.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);
            if (duplicates.Count() > 0)
            {
                isValid = false;
            }

            return isValid;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
            }
        }
    }
}
