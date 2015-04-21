using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class DebugPanel : Form
    {
        frmTerminal parentSerialTerminal;

        public DebugPanel(frmTerminal parentTerminal)
        {
            parentSerialTerminal = parentTerminal;
            InitializeComponent();
        }
        //disarm button - sends disarm packet
        // Author: Taylor Trabun
        private void debug0(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_arm_message(false);
        }
        //send takeoff with altitue 160
        private void debug1(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_takeoff_packet(160);
        }

        private void debug2(object sender, EventArgs e)
        {

        }

        private void debug3(object sender, EventArgs e)
        {

        }

        private void debug4(object sender, EventArgs e)
        {

        }
        //arm button code - sends arm packet
        // Author: Taylor Trabun
        private void debug5(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_arm_message(true);
        }

        private void debug6(object sender, EventArgs e)
        {

        }

        private void debug7(object sender, EventArgs e)
        {

        }

        private void debug8(object sender, EventArgs e)
        {

        }

        private void debug9(object sender, EventArgs e)
        {

        }

    }
}
