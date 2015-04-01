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
    public partial class ManualControl : Form
    {
        frmTerminal parentSerialTerminal;

        public ManualControl(frmTerminal parentTerminal)
        {
            this.KeyPreview = true;
            parentSerialTerminal = parentTerminal;
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(keySend);
            this.KeyUp += new KeyEventHandler(keyStop);
        }

        private void keySend(object sender, KeyEventArgs e)
        {
            String key = e.KeyCode.ToString();
            String name;

            // Highlight control in window
            foreach(Button b in Controls.OfType<Button>()) {
                if ((String)b.Tag == key)
                {
                    name = "/../../Resources/" + b.Name + "_pressed.png";
                    //Get image path
                    String imgUrl = System.IO.Directory.GetCurrentDirectory() + name;
                    b.Image = new Bitmap(imgUrl);
                    break;
                }
            }
                
            sendCommand(key);
        }

        private void keyStop(object sender, KeyEventArgs e)
        {
            String key = e.KeyCode.ToString();
            String name;

            // Highlight control in window
            foreach (Button b in Controls.OfType<Button>())
            {
                if ((String)b.Tag == key)
                {
                    name = "/../../Resources/" + b.Name + ".png";
                    //Get image path
                    String imgUrl = System.IO.Directory.GetCurrentDirectory() + name;
                    b.Image = new Bitmap(imgUrl);
                    break;
                }
            }
            //send stop code
        }

        private void mouseSend(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            String key = b.Tag.ToString();

            String name = "/../../Resources/" + b.Name + "_pressed.png";
            //Get image path
            String imgUrl = System.IO.Directory.GetCurrentDirectory() + name;
            b.Image = new Bitmap(imgUrl);

            sendCommand(key);
        }

        private void mouseStop(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;

            String name = "/../../Resources/" + b.Name + ".png";
            //Get image path
            String imgUrl = System.IO.Directory.GetCurrentDirectory() + name;
            b.Image = new Bitmap(imgUrl);

            //send stop code
        }

        private void sendCommand(String k)
        {

            switch (k)
            {
                case "W":
                    //send forward command
                    break;
                case "A":
                    //send left command
                    break;
                case "S":
                    //send back command
                    break;
                case "D":
                    //send right command
                    break;
                case "Q":
                    //send rotate left command
                    break;
                case "E":
                    //send rotate right command
                    break;
                case "Up":
                    //send Up command
                    break;
                case "Down":
                    //send Down command
                    break;
                default:
                    break;
            }
        }

        private void previewKey(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                    e.IsInputKey = true;
                    break;
            }
        }

    }
}
