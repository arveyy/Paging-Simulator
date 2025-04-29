namespace Paging_Simulator
{
    partial class Form3
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblMemorySize;
        private Label lblFrameCount;
        private Label lblPageSize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblMemorySize = new Label();
            lblFrameCount = new Label();
            lblPageSize = new Label();
            SuspendLayout();
            // 
            // lblMemorySize
            // 
            lblMemorySize.AutoSize = true;
            lblMemorySize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMemorySize.Location = new Point(50, 20);
            lblMemorySize.Name = "lblMemorySize";
            lblMemorySize.Size = new Size(0, 21);
            lblMemorySize.TabIndex = 0;
            // 
            // lblFrameCount
            // 
            lblFrameCount.AutoSize = true;
            lblFrameCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFrameCount.Location = new Point(50, 50);
            lblFrameCount.Name = "lblFrameCount";
            lblFrameCount.Size = new Size(0, 21);
            lblFrameCount.TabIndex = 1;
            // 
            // lblPageSize
            // 
            lblPageSize.AutoSize = true;
            lblPageSize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPageSize.Location = new Point(50, 80);
            lblPageSize.Name = "lblPageSize";
            lblPageSize.Size = new Size(0, 21);
            lblPageSize.TabIndex = 2;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1111, 621);
            Controls.Add(lblPageSize);
            Controls.Add(lblFrameCount);
            Controls.Add(lblMemorySize);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form3";
            Text = "Paging Simulation";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}