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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.down = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.Button();
            this.rot_left = new System.Windows.Forms.Button();
            this.rot_right = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(177, 391);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Movement";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(186, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Rotation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(507, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Altitude";
            // 
            // down
            // 
            this.down.FlatAppearance.BorderSize = 0;
            this.down.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.down.Font = new System.Drawing.Font("Consolas", 10F);
            this.down.Image = global::SerialPortTerminal.Properties.Resources.down;
            this.down.Location = new System.Drawing.Point(493, 229);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(98, 122);
            this.down.TabIndex = 7;
            this.down.Tag = "Down";
            this.down.UseVisualStyleBackColor = true;
            this.down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            this.down.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.previewKey);
            // 
            // up
            // 
            this.up.FlatAppearance.BorderSize = 0;
            this.up.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.up.Font = new System.Drawing.Font("Consolas", 10F);
            this.up.Image = global::SerialPortTerminal.Properties.Resources.up;
            this.up.Location = new System.Drawing.Point(495, 41);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(96, 118);
            this.up.TabIndex = 6;
            this.up.Tag = "Up";
            this.up.UseVisualStyleBackColor = true;
            this.up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // rot_left
            // 
            this.rot_left.FlatAppearance.BorderSize = 0;
            this.rot_left.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rot_left.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.rot_left.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.rot_left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rot_left.Font = new System.Drawing.Font("Consolas", 10F);
            this.rot_left.Image = global::SerialPortTerminal.Properties.Resources.rot_left;
            this.rot_left.Location = new System.Drawing.Point(105, 36);
            this.rot_left.Name = "rot_left";
            this.rot_left.Size = new System.Drawing.Size(86, 36);
            this.rot_left.TabIndex = 5;
            this.rot_left.Tag = "Q";
            this.rot_left.UseVisualStyleBackColor = true;
            this.rot_left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.rot_left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // rot_right
            // 
            this.rot_right.FlatAppearance.BorderSize = 0;
            this.rot_right.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rot_right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.rot_right.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.rot_right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rot_right.Font = new System.Drawing.Font("Consolas", 10F);
            this.rot_right.Image = global::SerialPortTerminal.Properties.Resources.rot_right;
            this.rot_right.Location = new System.Drawing.Point(263, 36);
            this.rot_right.Name = "rot_right";
            this.rot_right.Size = new System.Drawing.Size(85, 36);
            this.rot_right.TabIndex = 4;
            this.rot_right.Tag = "E";
            this.rot_right.UseVisualStyleBackColor = true;
            this.rot_right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.rot_right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // back
            // 
            this.back.FlatAppearance.BorderSize = 0;
            this.back.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Consolas", 10F);
            this.back.Image = global::SerialPortTerminal.Properties.Resources.back;
            this.back.Location = new System.Drawing.Point(165, 248);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(118, 140);
            this.back.TabIndex = 3;
            this.back.Tag = "S";
            this.back.UseVisualStyleBackColor = true;
            this.back.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.back.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // left
            // 
            this.left.FlatAppearance.BorderSize = 0;
            this.left.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.left.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.left.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.left.Font = new System.Drawing.Font("Consolas", 10F);
            this.left.Image = global::SerialPortTerminal.Properties.Resources.left;
            this.left.Location = new System.Drawing.Point(29, 169);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(125, 76);
            this.left.TabIndex = 2;
            this.left.Tag = "A";
            this.left.UseVisualStyleBackColor = true;
            this.left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // right
            // 
            this.right.FlatAppearance.BorderSize = 0;
            this.right.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.right.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.right.Font = new System.Drawing.Font("Consolas", 10F);
            this.right.Image = global::SerialPortTerminal.Properties.Resources.right;
            this.right.Location = new System.Drawing.Point(293, 170);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(132, 76);
            this.right.TabIndex = 1;
            this.right.Tag = "D";
            this.right.UseVisualStyleBackColor = true;
            this.right.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keySend);
            this.right.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyStop);
            this.right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // forward
            // 
            this.forward.BackColor = System.Drawing.Color.Transparent;
            this.forward.FlatAppearance.BorderSize = 0;
            this.forward.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.forward.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.forward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.forward.Font = new System.Drawing.Font("Consolas", 10F);
            this.forward.Image = global::SerialPortTerminal.Properties.Resources.forward;
            this.forward.Location = new System.Drawing.Point(181, 98);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(87, 70);
            this.forward.TabIndex = 0;
            this.forward.Tag = "W";
            this.forward.UseVisualStyleBackColor = false;
            this.forward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseSend);
            this.forward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseStop);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(202, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "20° N";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(175, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Lon: A233F6";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.Location = new System.Drawing.Point(529, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "12";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(177, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Lat: A233F6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(515, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "meters";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManualControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSalmon;
            this.ClientSize = new System.Drawing.Size(629, 435);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.rot_left);
            this.Controls.Add(this.rot_right);
            this.Controls.Add(this.back);
            this.Controls.Add(this.left);
            this.Controls.Add(this.right);
            this.Controls.Add(this.forward);
            this.Name = "ManualControl";
            this.Text = "ManualControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button forward;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button rot_right;
        private System.Windows.Forms.Button rot_left;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
    }
}