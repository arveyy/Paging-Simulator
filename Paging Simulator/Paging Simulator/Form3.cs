using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Paging_Simulator
{
    public partial class Form3 : Form
    {
        private int memorySize;
        private int frameCount;
        private int pageSize;
        private Dictionary<int, int> pageTable = new Dictionary<int, int>();
        private List<Frame> frames = new List<Frame>();
        private Random random = new Random();
        private Guna2Button btnExitFullScreen = null!; // Use null-forgiving operator to suppress the warning

        public Form3(int memorySize, int frameCount)
        {
            InitializeComponent();

            // Set full screen properties
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Store parameters
            this.memorySize = memorySize;
            this.frameCount = frameCount;
            this.pageSize = memorySize / frameCount;

            // Initialize UI
            InitializeSimulation();
        }

        private void InitializeSimulation()
        {
            // Initialize frames
            for (int i = 0; i < frameCount; i++)
            {
                frames.Add(new Frame(i, pageSize));
            }

            // Setup UI
            SetupMemoryDisplay();
            SetupControls();
            CreateExitButton();
        }

        private void SetupMemoryDisplay()
        {
            // Update info labels
            lblMemorySize.Text = $"Total Memory: {memorySize}MB";
            lblFrameCount.Text = $"Frame Count: {frameCount}";
            lblPageSize.Text = $"Page Size: {pageSize}MB";

            // Create visual representation of frames
            int x = 50, y = 120;
            int panelWidth = this.ClientSize.Width - 100;
            int columns = Math.Max(1, panelWidth / 170); // 150px width + 20px margin

            for (int i = 0; i < frameCount; i++)
            {
                var framePanel = new Panel
                {
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle,
                    Size = new Size(150, 60),
                    Location = new Point(x, y),
                    Tag = i
                };

                var lblFrame = new Label
                {
                    Text = $"Frame {i}\n{pageSize}MB",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var lblPage = new Label
                {
                    Text = "Free",
                    Dock = DockStyle.Bottom,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Name = $"lblPage_{i}"
                };

                framePanel.Controls.Add(lblFrame);
                framePanel.Controls.Add(lblPage);
                this.Controls.Add(framePanel);

                // Update position for next frame
                x += 170;
                if (x + 150 > panelWidth)
                {
                    x = 50;
                    y += 80;
                }
            }
        }

        private void SetupControls()
        {
            // Add button to simulate page allocation
            var btnAllocate = new Guna2Button
            {
                Text = "Allocate Random Page",
                Size = new Size(180, 45),
                Location = new Point(50, this.ClientSize.Height - 100),
                FillColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Black", 9F, FontStyle.Bold)
            };
            btnAllocate.Click += AllocateRandomPage;
            this.Controls.Add(btnAllocate);

            // Add button to reset simulation
            var btnReset = new Guna2Button
            {
                Text = "Reset Simulation",
                Size = new Size(180, 45),
                Location = new Point(250, this.ClientSize.Height - 100),
                FillColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Black", 9F, FontStyle.Bold)
            };
            btnReset.Click += ResetSimulation;
            this.Controls.Add(btnReset);
        }

        private void CreateExitButton()
        {
            btnExitFullScreen = new Guna2Button
            {
                Text = "Exit Full Screen",
                Size = new Size(120, 35),
                Location = new Point(this.ClientSize.Width - 150, 20),
                FillColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnExitFullScreen.Click += (s, e) => {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            };
            this.Controls.Add(btnExitFullScreen);
        }

        private void AllocateRandomPage(object? sender, EventArgs e)
        {
            int pageNumber = random.Next(0, 100);
            int frameIndex = -1;

            // Find a free frame
            for (int i = 0; i < frames.Count; i++)
            {
                if (frames[i].IsFree)
                {
                    frameIndex = i;
                    break;
                }
            }

            if (frameIndex == -1)
            {
                MessageBox.Show("No free frames available!", "Memory Full",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Allocate the page
            frames[frameIndex].IsFree = false;
            frames[frameIndex].PageNumber = pageNumber;
            pageTable[pageNumber] = frameIndex;

            // Update UI
            UpdateFrameDisplay(frameIndex, pageNumber);
        }

        private void UpdateFrameDisplay(int frameIndex, int pageNumber)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel && panel.Tag is int tag && tag == frameIndex)
                {
                    foreach (Control lbl in panel.Controls)
                    {
                        if (lbl.Name == $"lblPage_{frameIndex}")
                        {
                            lbl.Text = $"Page {pageNumber}";
                            lbl.BackColor = Color.LightGreen;
                        }
                    }
                }
            }
        }

        private void ResetSimulation(object? sender, EventArgs e)
        {
            // Clear all frames
            foreach (Frame frame in frames)
            {
                frame.IsFree = true;
                frame.PageNumber = -1;
            }

            pageTable.Clear();

            // Reset UI
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control lbl in panel.Controls)
                    {
                        if (lbl.Name?.StartsWith("lblPage_") == true)
                        {
                            lbl.Text = "Free";
                            lbl.BackColor = Color.Transparent;
                        }
                    }
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Ensure full screen is maintained after load
            this.WindowState = FormWindowState.Maximized;
        }
    }

    public class Frame
    {
        public int Id { get; }
        public int Size { get; }
        public bool IsFree { get; set; }
        public int PageNumber { get; set; }

        public Frame(int id, int size)
        {
            Id = id;
            Size = size;
            IsFree = true;
            PageNumber = -1;
        }
    }
}