namespace SerialPortTerminal
{
    partial class ManualControl
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
            this.rightButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.rotateRightButton = new System.Windows.Forms.Button();
            this.rotateLeftButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rightButton
            // 
            this.rightButton.FlatAppearance.BorderSize = 0;
            this.rightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.rightButton.Image = global::SerialPortTerminal.Properties.Resources.right;
            this.rightButton.Location = new System.Drawing.Point(294, 173);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(132, 76);
            this.rightButton.TabIndex = 1;
            this.rightButton.UseVisualStyleBackColor = true;
            // 
            // leftButton
            // 
            this.leftButton.FlatAppearance.BorderSize = 0;
            this.leftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.leftButton.Image = global::SerialPortTerminal.Properties.Resources.left;
            this.leftButton.Location = new System.Drawing.Point(29, 172);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(125, 76);
            this.leftButton.TabIndex = 2;
            this.leftButton.UseVisualStyleBackColor = true;
            // 
            // backButton
            // 
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.backButton.Image = global::SerialPortTerminal.Properties.Resources.back;
            this.backButton.Location = new System.Drawing.Point(165, 251);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(118, 140);
            this.backButton.TabIndex = 3;
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.FlatAppearance.BorderSize = 0;
            this.rotateRightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rotateRightButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.rotateRightButton.Image = global::SerialPortTerminal.Properties.Resources.rot_right;
            this.rotateRightButton.Location = new System.Drawing.Point(263, 39);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(85, 36);
            this.rotateRightButton.TabIndex = 4;
            this.rotateRightButton.UseVisualStyleBackColor = true;
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.FlatAppearance.BorderSize = 0;
            this.rotateLeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rotateLeftButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.rotateLeftButton.Image = global::SerialPortTerminal.Properties.Resources.rot_left;
            this.rotateLeftButton.Location = new System.Drawing.Point(104, 39);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(86, 36);
            this.rotateLeftButton.TabIndex = 5;
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            // 
            // upButton
            // 
            this.upButton.FlatAppearance.BorderSize = 0;
            this.upButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.upButton.Image = global::SerialPortTerminal.Properties.Resources.up;
            this.upButton.Location = new System.Drawing.Point(518, 39);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(96, 118);
            this.upButton.TabIndex = 6;
            this.upButton.UseVisualStyleBackColor = true;
            // 
            // downButton
            // 
            this.downButton.FlatAppearance.BorderSize = 0;
            this.downButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.downButton.Image = global::SerialPortTerminal.Properties.Resources.down;
            this.downButton.Location = new System.Drawing.Point(516, 227);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(98, 122);
            this.downButton.TabIndex = 7;
            this.downButton.UseVisualStyleBackColor = true;
            // 
            // forwardButton
            // 
            this.forwardButton.BackColor = System.Drawing.Color.Transparent;
            this.forwardButton.FlatAppearance.BorderSize = 0;
            this.forwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.forwardButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.forwardButton.Image = global::SerialPortTerminal.Properties.Resources.forward;
            this.forwardButton.Location = new System.Drawing.Point(181, 101);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(87, 70);
            this.forwardButton.TabIndex = 0;
            this.forwardButton.UseVisualStyleBackColor = false;
            // 
            // ManualControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 397);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.rotateLeftButton);
            this.Controls.Add(this.rotateRightButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.forwardButton);
            this.Name = "ManualControl";
            this.Text = "ManualControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button rotateRightButton;
        private System.Windows.Forms.Button rotateLeftButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
    }
}