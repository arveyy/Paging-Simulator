using System;
using System.Windows.Forms;

namespace Paging_Simulator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Continue_Click_1(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                int memorySize = int.Parse(MemorySizeInput.Text);
                int frameCount = int.Parse(FramesInput.Text);

                Form3 simulationForm = new Form3(memorySize, frameCount);
                simulationForm.Show();
                this.Hide();
            }
        }

        private bool ValidateInputs()
        {
            // Validate memory size
            if (!int.TryParse(MemorySizeInput.Text, out int memorySize) ||
                memorySize < 100 || memorySize > 500)
            {
                MessageBox.Show("Please enter a valid memory size between 100MB and 500MB",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            // Validate frame count
            if (!int.TryParse(FramesInput.Text, out int frameCount) || frameCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of frames (must be positive)",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void MemorySizeInput_TextChanged(object sender, EventArgs e)
        {
            // You could add real-time validation here if needed
        }

        private void FramesInput_TextChanged(object sender, EventArgs e)
        {
            // You could add real-time validation here if needed
        }
    }
}