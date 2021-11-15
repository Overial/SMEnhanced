using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMEnhanced
{
    public partial class Form1 : Form
    {
        int dimension;

        int bfsMethodStepsCount;
        int btMethodStepsCount;

        // Returs true if xCoords are en prise
        bool AreTwoQueensEnPrise(int x1, int y1, int x2, int y2)
        {
            // Check for equal coords
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }

            // We need to cast rays in needed directions
            int i = x1;
            int j = y1;

            // NW
            while (i >= 1 && j >= 1)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                --i;
                --j;
            }

            // N
            i = x1;
            j = y1;
            while (i >= 1)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                --i;
            }

            // NE
            i = x1;
            j = y1;
            while (i >= 1 && j <= dimension)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                --i;
                ++j;
            }

            // W
            i = x1;
            j = y1;
            while (j >= 1)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                --j;
            }

            // E
            i = x1;
            j = y1;
            while (j <= dimension)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                ++j;
            }

            // SW
            i = x1;
            j = y1;
            while (i <= dimension && j >= 1)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                ++i;
                --j;
            }

            // S
            i = x1;
            j = y1;
            while (i <= dimension)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                ++i;
            }

            // SE
            i = x1;
            j = y1;
            while (i <= dimension && j <= dimension)
            {
                // Found queen
                if (i == x2 && j == y2)
                {
                    return true;
                }

                ++i;
                ++j;
            }

            return false;
        }

        // Returns true if current queen is en prise, false if is not en prise
        bool IsQueenEnPrise(int[] xCoords, int iCurrentQueenYCoord)
        {
            // Check every previous queen before current
            for (int j = 1; j <= iCurrentQueenYCoord - 1; ++j)
            {
                if (AreTwoQueensEnPrise(xCoords[j], j, xCoords[iCurrentQueenYCoord], iCurrentQueenYCoord))
                {
                    return true;
                }
            }

            return false;
        }

        // Method to draw board on screen
        void DrawBoard()
        {
            // Disable user board management
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;

            // Clear board
            this.dataGridView1.Columns.Clear();

            // Center alignment
            this.dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set column height
            this.dataGridView1.RowTemplate.Height = 32;

            // Set columns
            for (int i = 0; i < dimension; ++i)
            {
                // DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                // this.dataGridView1.Columns.Add(imageCol);

                // Initialize board column
                this.dataGridView1.Columns.Add(i.ToString(), (i + 1).ToString());

                // Set column width
                this.dataGridView1.Columns[i].Width = 32;
            }

            // Set rows
            this.dataGridView1.Rows.Add(dimension);

            // Print column numbers
            for (int i = 0; i < dimension; ++i)
            {
                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        // Method to create arrangement option
        void AddNewOptionToList(int[] row)
        {
            // Set arrangement option variable
            string option = "";

            // Fill arrangement option
            if (radioButton1.Checked)
            {
                for (int i = 1; i <= dimension; ++i)
                {
                    option += row[i].ToString() + "," + i.ToString() + ";";
                }
            }
            else if (radioButton2.Checked)
            {
                for (int i = 0; i < dimension; ++i)
                {
                    option += (row[i] + 1).ToString() + "," + (i + 1).ToString() + ";";
                }
            }

            // Add arrangement option to list
            this.listBox1.Items.Add(option);
        }

        // Method to draw xCoords on board
        void DrawQueensOnBoard(string option)
        {
            // Clear board
            for (int i = 0; i < dimension; ++i)
            {
                for (int j = 0; j < dimension; ++j)
                {
                    // this.dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;

                    this.dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }

            // Display column with xCoords
            int k = 0;
            for (int i = 0; i < dimension; ++i)
            {
                // Parse x coord
                string xOption = "";
                while (option[k] != ',')
                {
                    xOption += option[k].ToString();
                    ++k;
                }
                ++k;

                // Parse y coord
                string yOption = "";
                while (option[k] != ';')
                {
                    yOption += option[k].ToString();
                    ++k;
                }
                ++k;

                // Set queen coord
                int xQueen = Int32.Parse(xOption);
                int yQueen = Int32.Parse(yOption);

                // Bitmap img = new Bitmap(@"D:\queen_image.png");
                // this.dataGridView1.Rows[yQueen].Cells[xQueen].Value = img;
                // this.dataGridView1.Rows[yQueen].Cells[xQueen].Value = Image.FromFile(@"D:\queen_image.png");
                // this.dataGridView1.Rows[yQueen].Cells[xQueen].Style.BackColor = Color.Black;

                // Display queen on board
                this.dataGridView1.Rows[yQueen - 1].Cells[xQueen - 1].Value = "Q";
            }
        }

        public Form1()
        {
            InitializeComponent();

            // Check BFS option
            this.radioButton1.Checked = true;
        }

        // Show selected option
        private void listBox1_Click(object sender, EventArgs e)
        {
            // If list is empty then do nothing
            if (this.listBox1.Items.Count <= 0)
            {
                return;
            }

            // Get selected option
            int iSelectedOption = this.listBox1.SelectedIndex;

            // Draw current option on board
            DrawQueensOnBoard(this.listBox1.Items[iSelectedOption].ToString());
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // If list is empty then do nothing
            if (this.listBox1.Items.Count <= 0)
            {
                return;
            }

            // Get selected option
            int iSelectedOption = this.listBox1.SelectedIndex;

            // Draw current option on board
            DrawQueensOnBoard(this.listBox1.Items[iSelectedOption].ToString());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If list is empty then do nothing
            if (this.listBox1.Items.Count <= 0)
            {
                return;
            }

            // Get selected option
            int iSelectedOption = this.listBox1.SelectedIndex;

            // Draw current option on board
            DrawQueensOnBoard(this.listBox1.Items[iSelectedOption].ToString());
        }

        public void BFSSearch(ref int iOptionsCount)
        {
            // xCoords[iCurrentQueenYCoord] – X
            // iCurrentQueenYCoord – Y

            // Queen X coords
            int[] xCoords = new int[dimension + 1];

            // Current queen Y coord
            int iCurrentQueenYCoord = 1;

            // Initial queen X coords
            xCoords[0] = 0;
            xCoords[iCurrentQueenYCoord] = 0;

            // Set every queen
            while (iCurrentQueenYCoord > 0)
            {
                ++bfsMethodStepsCount;
                xCoords[iCurrentQueenYCoord] += 1;

                // The last element
                if (iCurrentQueenYCoord == dimension)
                {
                    // Rollback
                    if (xCoords[iCurrentQueenYCoord] > dimension)
                    {
                        while (xCoords[iCurrentQueenYCoord] > dimension)
                        {
                            --iCurrentQueenYCoord;
                        }
                    }
                    else if (!IsQueenEnPrise(xCoords, iCurrentQueenYCoord))
                    {
                        // Set new option
                        AddNewOptionToList(xCoords);
                        ++iOptionsCount;
                        --iCurrentQueenYCoord;
                    }
                }
                // Not the last element
                else
                {
                    // Rollback
                    if (xCoords[iCurrentQueenYCoord] > dimension)
                    {
                        while (xCoords[iCurrentQueenYCoord] > dimension)
                        {
                            --iCurrentQueenYCoord;
                        }
                    }
                    // Move to next column
                    else if (!IsQueenEnPrise(xCoords, iCurrentQueenYCoord))
                    {
                        ++iCurrentQueenYCoord;
                        xCoords[iCurrentQueenYCoord] = 0;
                    }
                }
            }
        }

        // Board for BT method
        int[] board;

        // Check current tile
        public bool IsTileSafe(int row, int col)
        {
            int j = 0;
            while (j < col)
            {
                if (// Check row
                    row == board[j] ||
                    // Check left diagonal
                    col - row == j - board[j] ||
                    // Check right diagonal
                    col + row == j + board[j])
                {
                    return false;
                }

                ++j;
            }

            return true;
        }

        // Recursive backtracking method
        public void BacktrackingMethod(int col, ref int iOptionsCount)
        {
            ++btMethodStepsCount;

            if (col == dimension)
            {
                ++iOptionsCount;

                AddNewOptionToList(board);
            }

            for (int row = 0; row < dimension; ++row)
            {
                if (IsTileSafe(row, col))
                {
                    board[col] = row;
                    BacktrackingMethod(col + 1, ref iOptionsCount);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();

            // Prevent invalid input
            try
            {
                // Get matrix dimension
                this.dimension = Convert.ToInt32(this.textBox1.Text);

                // 4 < dimension < 8
                if (dimension < 4)
                {
                    dimension = 4;
                }
                else if (dimension > 8)
                {
                    dimension = 8;
                }

                // Display board dimension
                label4.Text = "Доска " + dimension + "x" + dimension + ":";
            }
            catch
            {
                this.textBox1.Clear();
            }

            this.textBox1.Clear();
            DrawBoard();

            if (this.radioButton1.Checked)
            {
                // Amount of options
                int iOptionsCount = 0;

                // Call BFS method function
                BFSSearch(ref iOptionsCount);

                if (iOptionsCount > 0)
                {
                    // Display options arrangement options amount
                    this.listBox1.SelectedIndex = 0;
                    this.listBox1_Click(sender, e);
                    label2.Text = "Количество вариантов размещений: " + iOptionsCount.ToString();

                    // Display steps amount to capture effectiveness
                    label7.Text = "Количество шагов: " + bfsMethodStepsCount.ToString();
                    bfsMethodStepsCount = 0;
                }
                else
                {
                    label2.Text = "Количество вариантов размещений: 0";
                }
            }
            else if (this.radioButton2.Checked)
            {
                int iOptionsCount = 0;

                board = new int[dimension];

                // Call backtracking method function
                BacktrackingMethod(0, ref iOptionsCount);

                if (iOptionsCount > 0)
                {
                    // Display options arrangement options amount
                    this.listBox1.SelectedIndex = 0;
                    this.listBox1_Click(sender, e);
                    label2.Text = "Количество вариантов размещений: " + iOptionsCount.ToString();

                    // Display steps amount to capture effectiveness
                    label7.Text = "Количество шагов: " + btMethodStepsCount.ToString();
                    btMethodStepsCount = 0;
                }
                else
                {
                    label2.Text = "Количество вариантов размещений: 0";
                }
            }
        }

        // Previous listBox choice
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex > 0)
            {
                --this.listBox1.SelectedIndex;
            }
        }

        // Next listBox choice
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex < this.listBox1.Items.Count - 1)
            {
                ++this.listBox1.SelectedIndex;
            }
        }
    }
}
