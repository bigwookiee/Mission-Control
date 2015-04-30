namespace SerialPortTerminal
{
    partial class DemoWinow
    {
       
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoWinow));
            this.headingIndicatorInstrumentControl1 = new SerialPortTerminal.HeadingIndicatorInstrumentControl();
            this.horizonInstrumentControl1 = new SerialPortTerminal.AttitudeIndicatorInstrumentControl();
            this.SuspendLayout();
            // 
            // headingIndicatorInstrumentControl1
            // 
            this.headingIndicatorInstrumentControl1.Location = new System.Drawing.Point(57, 267);
            this.headingIndicatorInstrumentControl1.Name = "headingIndicatorInstrumentControl1";
            this.headingIndicatorInstrumentControl1.Size = new System.Drawing.Size(200, 200);
            this.headingIndicatorInstrumentControl1.TabIndex = 4;
            this.headingIndicatorInstrumentControl1.Text = "headingIndicatorInstrumentControl1";
            // 
            // horizonInstrumentControl1
            // 
            this.horizonInstrumentControl1.Location = new System.Drawing.Point(31, 12);
            this.horizonInstrumentControl1.Name = "horizonInstrumentControl1";
            this.horizonInstrumentControl1.Size = new System.Drawing.Size(250, 250);
            this.horizonInstrumentControl1.TabIndex = 3;
            this.horizonInstrumentControl1.Text = "horizonInstrumentControl1";
            // 
            // DemoWinow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 479);
            this.Controls.Add(this.headingIndicatorInstrumentControl1);
            this.Controls.Add(this.horizonInstrumentControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DemoWinow";
            this.Text = "Drone Instrument Panel";
            this.ResumeLayout(false);

        }

        #endregion

        private AttitudeIndicatorInstrumentControl horizonInstrumentControl1;
        private HeadingIndicatorInstrumentControl headingIndicatorInstrumentControl1;

        
    }
}

