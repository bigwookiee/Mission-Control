namespace SerialPortTerminal
{
    partial class GUIform
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.holdPos = new System.Windows.Forms.Button();
            this.eLand = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.instructionP = new System.Windows.Forms.Panel();
            this.submit = new System.Windows.Forms.Button();
            this.addInstr = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.overallProgress = new System.Windows.Forms.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.altitude = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitFlightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFlightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFlightInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualControlsLink = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentsPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topView = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altitude)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.topView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MistyRose;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.holdPos);
            this.panel1.Controls.Add(this.eLand);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.instructionP);
            this.panel1.Controls.Add(this.submit);
            this.panel1.Controls.Add(this.addInstr);
            this.panel1.Location = new System.Drawing.Point(1, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 418);
            this.panel1.TabIndex = 0;
            // 
            // holdPos
            // 
            this.holdPos.BackColor = System.Drawing.Color.Red;
            this.holdPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.holdPos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.holdPos.Location = new System.Drawing.Point(8, 370);
            this.holdPos.Name = "holdPos";
            this.holdPos.Size = new System.Drawing.Size(87, 37);
            this.holdPos.TabIndex = 9;
            this.holdPos.Text = "Hold Position";
            this.holdPos.UseVisualStyleBackColor = false;
            // 
            // eLand
            // 
            this.eLand.BackColor = System.Drawing.Color.Red;
            this.eLand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eLand.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.eLand.Location = new System.Drawing.Point(229, 370);
            this.eLand.Name = "eLand";
            this.eLand.Size = new System.Drawing.Size(85, 37);
            this.eLand.TabIndex = 8;
            this.eLand.Text = "Emergency Land";
            this.eLand.UseVisualStyleBackColor = false;
            this.eLand.Click += new System.EventHandler(this.LandNow);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(254, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Flight Planning System";
            // 
            // instructionP
            // 
            this.instructionP.AutoScroll = true;
            this.instructionP.Location = new System.Drawing.Point(2, 35);
            this.instructionP.Name = "instructionP";
            this.instructionP.Size = new System.Drawing.Size(316, 329);
            this.instructionP.TabIndex = 6;
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(101, 370);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(122, 37);
            this.submit.TabIndex = 5;
            this.submit.Text = "Submit Flight Plan";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.SubmitFlightPlan);
            // 
            // addInstr
            // 
            this.addInstr.Location = new System.Drawing.Point(292, 3);
            this.addInstr.Name = "addInstr";
            this.addInstr.Size = new System.Drawing.Size(26, 26);
            this.addInstr.TabIndex = 0;
            this.addInstr.Tag = "button";
            this.addInstr.Text = "+";
            this.addInstr.UseVisualStyleBackColor = true;
            this.addInstr.Click += new System.EventHandler(this.addInstruction);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Thistle;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.overallProgress);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.currentProgress);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(1, 439);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 143);
            this.panel2.TabIndex = 1;
            // 
            // overallProgress
            // 
            this.overallProgress.BackColor = System.Drawing.Color.Thistle;
            this.overallProgress.Location = new System.Drawing.Point(8, 105);
            this.overallProgress.Name = "overallProgress";
            this.overallProgress.Size = new System.Drawing.Size(303, 23);
            this.overallProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.overallProgress.TabIndex = 4;
            this.overallProgress.Value = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Overall Flight Plan Progress";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Current Instruction Progress";
            // 
            // currentProgress
            // 
            this.currentProgress.BackColor = System.Drawing.Color.Thistle;
            this.currentProgress.Location = new System.Drawing.Point(8, 54);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(303, 23);
            this.currentProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.currentProgress.TabIndex = 1;
            this.currentProgress.Value = 60;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Status Info";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.CadetBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.altitude);
            this.panel4.Location = new System.Drawing.Point(332, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(143, 558);
            this.panel4.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SerialPortTerminal.Properties.Resources.sideCopter;
            this.pictureBox1.Location = new System.Drawing.Point(87, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "Altitude";
            // 
            // altitude
            // 
            this.altitude.BackColor = System.Drawing.Color.CadetBlue;
            this.altitude.Location = new System.Drawing.Point(87, 38);
            this.altitude.Maximum = 30;
            this.altitude.Name = "altitude";
            this.altitude.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.altitude.Size = new System.Drawing.Size(45, 479);
            this.altitude.TabIndex = 0;
            this.altitude.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(920, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.submitFlightPlanToolStripMenuItem,
            this.openFlightPlanToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // submitFlightPlanToolStripMenuItem
            // 
            this.submitFlightPlanToolStripMenuItem.Name = "submitFlightPlanToolStripMenuItem";
            this.submitFlightPlanToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.submitFlightPlanToolStripMenuItem.Text = "Submit Flight Plan";
            // 
            // openFlightPlanToolStripMenuItem
            // 
            this.openFlightPlanToolStripMenuItem.Name = "openFlightPlanToolStripMenuItem";
            this.openFlightPlanToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openFlightPlanToolStripMenuItem.Text = "Open Flight Plan";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFlightInstructionToolStripMenuItem,
            this.removeSelectedToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addFlightInstructionToolStripMenuItem
            // 
            this.addFlightInstructionToolStripMenuItem.Name = "addFlightInstructionToolStripMenuItem";
            this.addFlightInstructionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.addFlightInstructionToolStripMenuItem.Text = "Add Flight Instruction";
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualControlsLink,
            this.instrumentsPanelToolStripMenuItem,
            this.serialTerminalToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // manualControlsLink
            // 
            this.manualControlsLink.Name = "manualControlsLink";
            this.manualControlsLink.Size = new System.Drawing.Size(169, 22);
            this.manualControlsLink.Text = "Manual Controls";
            // 
            // instrumentsPanelToolStripMenuItem
            // 
            this.instrumentsPanelToolStripMenuItem.Name = "instrumentsPanelToolStripMenuItem";
            this.instrumentsPanelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.instrumentsPanelToolStripMenuItem.Text = "Instruments Panel";
            // 
            // serialTerminalToolStripMenuItem
            // 
            this.serialTerminalToolStripMenuItem.Name = "serialTerminalToolStripMenuItem";
            this.serialTerminalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.serialTerminalToolStripMenuItem.Text = "Serial Terminal";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // topView
            // 
            this.topView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.topView.BackgroundImage = global::SerialPortTerminal.Properties.Resources.maps;
            this.topView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.topView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.topView.Controls.Add(this.pictureBox2);
            this.topView.Controls.Add(this.label9);
            this.topView.Location = new System.Drawing.Point(472, 24);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(447, 558);
            this.topView.TabIndex = 4;
            this.topView.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SerialPortTerminal.Properties.Resources.topCopter;
            this.pictureBox2.Location = new System.Drawing.Point(395, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(222, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "Longitude / Latitude";
            // 
            // GUIform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(920, 584);
            this.Controls.Add(this.topView);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUIform";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altitude)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.topView.ResumeLayout(false);
            this.topView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addInstr;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel topView;
        private System.Windows.Forms.TrackBar altitude;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Panel instructionP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar overallProgress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submitFlightPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFlightPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFlightInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualControlsLink;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button holdPos;
        private System.Windows.Forms.Button eLand;
        private System.Windows.Forms.ToolStripMenuItem instrumentsPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serialTerminalToolStripMenuItem;
    }
}

