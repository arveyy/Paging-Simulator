using System;
using System.Windows.Forms;

namespace Paging_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Set the Continue button properties programmatically if needed
            Continue.FillColor = Color.Black;
            Continue.ForeColor = Color.White;
            Continue.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            // Create and show Form2
            Form2 form2 = new Form2();
            form2.Show();

            // Hide the current form (Form1)
            this.Hide();

            // Handle the closed event of Form2 to show Form1 again when needed
            form2.FormClosed += (s, args) => this.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Add custom painting for panel1 if needed
            // For example, you could add a border or background image
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
                                  Color.Black, ButtonBorderStyle.Solid);
        }
    }
}