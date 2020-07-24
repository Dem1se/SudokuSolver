using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        private List<int> Inputs = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // nothing to do on load
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            int collectionIterationIndex = 0;
            int answerIndex = 0;
            int[] answers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            PopulateInputs();

            // brute force the values
            foreach (int value in Inputs.ToList())
            {
                if (value == -1)
                {
                    do
                    {
                        if (collectionIterationIndex == 0)
                        {
                            Inputs[collectionIterationIndex] = 1;
                        }
                        else
                        {
                            Inputs[collectionIterationIndex] = answers[answerIndex % 9];
                            answerIndex++;
                        }
                        
                    } while (!MoveIsValid(Inputs[collectionIterationIndex], GetPosition(collectionIterationIndex).X, GetPosition(collectionIterationIndex).Y));
                }
                collectionIterationIndex++;
                Console.WriteLine($"CellIndex: {collectionIterationIndex}");
            }

            // assign the values back to the textBoxes;
            collectionIterationIndex = 0;
            List<int> InputCopy = Inputs.ToList();
            InputCopy.Reverse();
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = InputCopy[collectionIterationIndex].ToString();
                    collectionIterationIndex++;
                }
            }
            solveButton.Enabled = false;
        }

        private void PopulateInputs()
        {
            Inputs.Clear();
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        Inputs.Add(-1);
                    }
                    else
                    {
                        Inputs.Add(Convert.ToInt32(ctrl.Text));
                    }
                }
            }
        }

        private bool MoveIsValid(int move, int column, int row)
        {
            bool isValid = true;
            // check the row for same value
            for (int x = 0; x < 9; x++)
            {
                if (move == Inputs[FindIndex(x, row)])
                {
                    if (x != column)
                    {
                        isValid = false;
                    }
                }
            }

            // check the column for same value
            for (int y = 0; y < 9; y++)
            {
                if (move == Inputs[FindIndex(column, y)])
                {
                    if (y != row)
                    {
                        isValid = false;
                    }
                }
            }

            // check the 9x9 cell for same value

            return isValid;
        }

        private Position GetPosition(int index)
        {
            return new Position(index % 9, index / 9);
        }

        private int FindIndex(int column, int row)
        {
            return column + (row * 9);
        }
    }

    struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X, Y;
    }
}
