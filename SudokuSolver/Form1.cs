using System;
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
            SetUpInitialState();
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
            Solution = Inputs;
        }

        /// <summary>
        /// This Mthods records the intial state of the puzzle before the solution starts.
        /// </summary>
        private void SetUpInitialState()
        {
            for (int cell = 0; cell < 81; cell++)
            {
                if (Inputs[cell % 9, cell / 9] == -1)
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

            // check the column for same value

            // check the box for same value


            return isValid;
        }

        /// <summary>
        /// finds the coordinate(2D) of the given cell index(1D)
        /// </summary>
        /// <param name="index">The one dimmensional index of the cell</param>
        /// <returns>{Position} struct instance with the 2D coordinates</returns>
        private Position GetPosition(int index)
        {
            return new Position(index % 9, index / 9);
        }

        /// <summary>
        /// Finds the cell index (1D) of the provided coordinates(2D)
        /// </summary>
        /// <param name="column">the X coordinate of the cell</param>
        /// <param name="row">the Y coordinate of the cell</param>
        /// <returns>{int} 1D cell index of the cell</returns>
        private int FindIndex(int column, int row)
        {
            return column + (row * 9);
        }
    }
}
