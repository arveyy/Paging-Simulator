namespace Paging_Simulator
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            MemorySizeInput = new Guna.UI2.WinForms.Guna2TextBox();
            panel2 = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            FramesInput = new Guna.UI2.WinForms.Guna2TextBox();
            Continue = new Guna.UI2.WinForms.Guna2Button();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // MemorySizeInput
            // 
            MemorySizeInput.BorderColor = Color.Black;
            MemorySizeInput.BorderThickness = 3;
            MemorySizeInput.CustomizableEdges = customizableEdges1;
            MemorySizeInput.DefaultText = "";
            MemorySizeInput.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            MemorySizeInput.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            MemorySizeInput.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            MemorySizeInput.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            MemorySizeInput.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            MemorySizeInput.Font = new Font("Segoe UI", 9F);
            MemorySizeInput.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            MemorySizeInput.Location = new Point(67, 115);
            MemorySizeInput.Name = "MemorySizeInput";
            MemorySizeInput.PlaceholderText = "";
            MemorySizeInput.SelectedText = "";
            MemorySizeInput.ShadowDecoration.CustomizableEdges = customizableEdges2;
            MemorySizeInput.Size = new Size(234, 36);
            MemorySizeInput.TabIndex = 0;
            MemorySizeInput.TextChanged += MemorySizeInput_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Controls.Add(label1);
            panel2.Location = new Point(67, 72);
            panel2.Name = "panel2";
            panel2.Size = new Size(234, 37);
            panel2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(222, 15);
            label1.TabIndex = 4;
            label1.Text = "Enter memory size (100MB - 500MB)";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(label2);
            panel1.Location = new Point(67, 157);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 37);
            panel1.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 10);
            label2.Name = "label2";
            label2.Size = new Size(202, 15);
            label2.TabIndex = 4;
            label2.Text = "How many frames to be created?";
            // 
            // FramesInput
            // 
            FramesInput.BorderColor = Color.Black;
            FramesInput.BorderThickness = 3;
            FramesInput.CustomizableEdges = customizableEdges3;
            FramesInput.DefaultText = "";
            FramesInput.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            FramesInput.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            FramesInput.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            FramesInput.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            FramesInput.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            FramesInput.Font = new Font("Segoe UI", 9F);
            FramesInput.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            FramesInput.Location = new Point(67, 200);
            FramesInput.Name = "FramesInput";
            FramesInput.PlaceholderText = "";
            FramesInput.SelectedText = "";
            FramesInput.ShadowDecoration.CustomizableEdges = customizableEdges4;
            FramesInput.Size = new Size(234, 36);
            FramesInput.TabIndex = 4;
            FramesInput.TextChanged += FramesInput_TextChanged;
            // 
            // Continue
            // 
            Continue.CustomizableEdges = customizableEdges5;
            Continue.DisabledState.BorderColor = Color.DarkGray;
            Continue.DisabledState.CustomBorderColor = Color.DarkGray;
            Continue.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            Continue.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            Continue.FillColor = Color.Black;
            Continue.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Continue.ForeColor = Color.White;
            Continue.Location = new Point(67, 253);
            Continue.Name = "Continue";
            Continue.ShadowDecoration.CustomizableEdges = customizableEdges6;
            Continue.Size = new Size(234, 45);
            Continue.TabIndex = 6;
            Continue.Text = "Continue";
            Continue.Click += Continue_Click_1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1111, 621);
            Controls.Add(Continue);
            Controls.Add(panel1);
            Controls.Add(FramesInput);
            Controls.Add(panel2);
            Controls.Add(MemorySizeInput);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            Text = "Form2";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox MemorySizeInput;
        private Panel panel2;
        private Label label1;
        private Panel panel1;
        private Label label2;
        private Guna.UI2.WinForms.Guna2TextBox FramesInput;
        private Guna.UI2.WinForms.Guna2Button Continue;
    }
}