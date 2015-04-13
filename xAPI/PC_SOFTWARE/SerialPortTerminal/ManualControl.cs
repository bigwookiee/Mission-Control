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
        Boolean armed = false;

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
            if (!armed) return; //only run event if drone is armed

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
            if (!armed) return; //only run event if drone is armed

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
            if (!armed) return; //only run event if drone is armed

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
            if (!armed) return; //only run event if drone is armed

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
                case "W": //forward
                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_FORWARD,
                                                           (int)DRONE_movement_metric.METRIC_FEET,
                                                            Int32.Parse("1"));
                    break;
                case "A": //left
                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_LEFT,
                                                           (int)DRONE_movement_metric.METRIC_FEET,
                                                            Int32.Parse("1"));
                    break;
                case "S": //back
                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_BACKWARD,
                                                           (int)DRONE_movement_metric.METRIC_FEET,
                                                            Int32.Parse("1"));
                    break;
                case "D": //right
                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_RIGHT,
                                                           (int)DRONE_movement_metric.METRIC_FEET,
                                                            Int32.Parse("1"));
                    break;
                case "Q":
                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_ROTATE_CLOCKWISE,
                                                           (int)DRONE_movement_metric.METRIC_DEGREES,
                                                            Int32.Parse("-5"));
                    break;
                case "E":
                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_ROTATE_CLOCKWISE,
                                                           (int)DRONE_movement_metric.METRIC_DEGREES,
                                                            Int32.Parse("5"));
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

        private void activateKeys()
        {
            armed = true;
            //Get image path to update status indicator
            String imgUrl = System.IO.Directory.GetCurrentDirectory() + "MC_activated.png";
            MC_Status.Image = new Bitmap(imgUrl);
        }
        private void deactivateKeys()
        {
            armed = false;
            //Get image path to update status indicator
            String imgUrl = System.IO.Directory.GetCurrentDirectory() + "MC_deactivated.png";
            MC_Status.Image = new Bitmap(imgUrl);
        }

    }
}
