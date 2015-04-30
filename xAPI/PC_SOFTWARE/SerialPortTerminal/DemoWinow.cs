/*****************************************************************************/
/* Project  : SerialPortTerminal                                  */
/* File     : DemoWondow.cs                                                  */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : Start window of the project, use to test the instruments       */
/* Creation : 30/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class DemoWinow : Form
    {
        frmTerminal parentSerialTerminal;
        public DemoWinow(frmTerminal parentTerminal)
        {
            parentSerialTerminal = parentTerminal;
            InitializeComponent();
        }

        public void updatePitchRoll(double P, double R)
        {
            horizonInstrumentControl1.SetAttitudeIndicatorParameters(P, R);
        }


        public void udpateHeading(int H) 
        {
            headingIndicatorInstrumentControl1.SetHeadingIndicatorParameters(H);
        }

        public void horizon_refresh() {
            horizonInstrumentControl1.Refresh();
           
        }

        public void heading_refresh(){
             headingIndicatorInstrumentControl1.Refresh();

        }
    }
}