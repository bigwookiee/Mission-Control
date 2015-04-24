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
        //set altHold true
        private void debug2(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_altHold_message(true);
        }
        // set HeadingHold hold longitude
        private void debug3(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_headingHold_message(0);
        }
        //set HeadingHold hold latitude
        private void debug4(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_headingHold_message(2);
        }
        //arm button code - sends arm packet
        // Author: Taylor Trabun
        private void debug5(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_arm_message(true);
        }
        //set heading 105
        private void debug6(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_setHeading_message(105);
        }
        //alt hold off
        private void debug7(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_altHold_message(false);
        }
        //headingHold disable longitude hold
        private void debug8(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_headingHold_message(1);
        }
        //headingHold disable latitude hold
        private void debug9(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_headingHold_message(3);
        }

    }
}
