﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class GUIform : Form
    {
        int instructs = 0;

        frmTerminal parentSerialTerminal;
        ManualControl mc;
        DebugPanel dbg;

        bool Takeoff = false;

        public GUIform(frmTerminal parentTerminal)
        {
            parentSerialTerminal = parentTerminal;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        /**
         * Add an istruction to the list.
         * @author Joe Higley
         **/
        private void addInstruction(object sender, EventArgs e)
        {
            // make new panel
            Panel p = new Panel();
            p.Visible = true;
            p.Width = 295;
            p.Height = 50;
            p.BackColor = Color.PeachPuff;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = new Point(1, 52 * instructs);
            string nm = "instruction" + instructs.ToString();
            p.Name = nm;

            // add actions field to panel
            ComboBox action = new ComboBox();
            action.Visible = true;
            action.Width = 65;
            action.Location = new Point(6, 14);
            action.Items.Add("Move");
            action.Items.Add("Rotate");
            action.Items.Add("GPS");
            action.Text = "Action";
            action.SelectedValueChanged += fillFields;
            p.Controls.Add(action);

            // add delete button to panel
            Button del = new Button();
            del.Visible = true;
            del.Width = 25;
            del.Height = 25;
            del.Location = new Point(260, 12);
            del.Text = "-";
            del.FlatStyle = FlatStyle.Flat;
            del.FlatAppearance.BorderColor = Color.DimGray;
            del.FlatAppearance.BorderSize = 2;
            del.FlatAppearance.MouseOverBackColor = Color.Silver;
            del.FlatAppearance.MouseDownBackColor = Color.DimGray;
            del.Click += delInstruction;
            p.Controls.Add(del);

            instructs++;
            instructionP.Controls.Add(p);
        }

        /**
         * Delete chosen instruction
         * @author Joe Higley
         **/
        private void delInstruction(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Control p = b.Parent;
            Control P = p.Parent;
            string nm = p.Name;

            // move instructions up.
            for (int i = 0; i < P.Controls.Count; i++)
            {
                while (P.Controls[i] != p)
                { // get beyond deleted instruction
                    i++;
                }
                i++;
                while (i < P.Controls.Count)
                { // move all instructions up.
                    P.Controls[i].Location = new Point(1, P.Controls[i].Location.Y - 52);
                    i++;
                }
            }

            p.Parent.Controls.Remove(p);
            // decrement instructs
            instructs--;
        }

        /**
         * Add correct fields in instruction
         * @author Joe Higley
         **/
        private void fillFields(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string action = (string)cb.SelectedItem;

            // clear other fields if existing.
            Control control = cb.Parent;
            for (int i = 0; i < 3 && control.Controls.Count > 2; i++)
            {
                control.Controls.Remove(control.Controls[2]);
            }
       

            if (action == "Move") {
                // add directions field to panel
                ComboBox dir = new ComboBox();
                dir.Visible = true;
                dir.Width = 70;
                dir.Location = new Point(78, 14);
                dir.Items.Add("Up");
                dir.Items.Add("Down");
                dir.Items.Add("Forward");
                dir.Items.Add("Back");
                dir.Items.Add("Left");
                dir.Items.Add("Right");
                dir.Text = "Direction";
                dir.Name = "dir";
                cb.Parent.Controls.Add(dir);

                // add number field to panel
                NumericUpDown dist = new NumericUpDown();
                dist.Visible = true;
                dist.Width = 45;
                dist.Location = new Point(155, 15);
                dist.Name = "dist";
                cb.Parent.Controls.Add(dist);

                // add units label to panel
                Label unit = new Label();
                unit.Visible = true;
                unit.Location = new Point(208, 16);
                unit.Text = "meter";
                unit.Name = "unit";
                cb.Parent.Controls.Add(unit);
            }
            else if (action == "Rotate") {
                // add directions field to panel
                ComboBox dir = new ComboBox();
                dir.Visible = true;
                dir.Width = 70;
                dir.Location = new Point(78, 14);
                dir.Items.Add("Left");
                dir.Items.Add("Right");
                dir.Text = "Direction";
                dir.Name = "dir";
                cb.Parent.Controls.Add(dir);

                // add number field to panel
                NumericUpDown dist = new NumericUpDown();
                dist.Visible = true;
                dist.Width = 45;
                dist.Location = new Point(155, 15);
                dist.Name = "dist";
                cb.Parent.Controls.Add(dist);

                // add units label to panel
                Label unit = new Label();
                unit.Visible = true;
                unit.Location = new Point(208, 16);
                unit.Text = "degrees";
                unit.Name = "unit";
                cb.Parent.Controls.Add(unit);
            }
            else if (action == "GPS")
            {
                // add longitude field to panel
                TextBox lon = new TextBox();
                lon.Visible = true;
                lon.Width = 52;
                lon.Location = new Point(78, 14);
                lon.Text = "Long";
                lon.Name = "lon";
                cb.Parent.Controls.Add(lon);

                // add latitude field to panel
                TextBox lat = new TextBox();
                lat.Visible = true;
                lat.Width = 52;
                lat.Location = new Point(137, 14);
                lat.Text = "Lat";
                lat.Name = "lat";
                cb.Parent.Controls.Add(lat);

                // add altitude field to panel
                TextBox alt = new TextBox();
                alt.Visible = true;
                alt.Width = 52;
                alt.Location = new Point(196, 14);
                alt.Text = "Alt";
                alt.Name = "alt";
                cb.Parent.Controls.Add(alt);
            }
        }

        /**
         *  Land drone now
         *  @author Taylor Trabun
         *  
         * Function is under construction
         **/
        private void LandNow(object sender, EventArgs e)
        {
            parentSerialTerminal.Send_land_packet();
        }

        /**
         * Submit flight plan to drone.
         * @author Joe Higley
         **/
        private void SubmitFlightPlan(object sender, EventArgs e)
        {
            if (!Takeoff)
            {
                parentSerialTerminal.Send_takeoff_packet(4);
                //Commented for snapshot demo, uncomment when this is encountered
                //Takeoff = true;
            }

            //parentSerialTerminal.SendString_LCD("0,0,TestData");

            if (instructionP.Controls.Count == 0)
            {
                return; //nothing to submit
            }
            //Animate the submition.
            Control p = instructionP.Controls[0];
            if (checkInstructCompletion(p))
            {
                p.BackColor = Color.LightCoral;
                if (p.Controls[1] is Button) // only remove - button if this is a button
                {
                    p.Controls.Remove(p.Controls[1]);
                }

            }

            //Send data to drone
            for (int i = 0; i < instructionP.Controls.Count; i++)
            {
                p = instructionP.Controls[i];
                if (checkInstructCompletion(p)) //Only submit complete instructions
                {
                    int j = 1; //Starting control of parsed data
                    if (p.Controls[1] is Button) //If control is not first control
                    {
                        j = 2;
                    }

                    //Parse data and send to drone
                    String field0, field1, field2, field3;
                    field0 = p.Controls[0].Text;        // Packet Type
                    field1 = p.Controls[j].Text;        // Move/Rotate: direction  ... GPS Longitude
                    field2 = p.Controls[j + 1].Text;    // Move/Rotate: value ... GPS Latitude

                    switch (field0)
                    {
                        case "Move":
                            /** Create Move Packet to send to drone **/
                            switch (field1)
                            {
                                case "Up":
                                    //calculate new coords.
                                    break;

                                case "Down":
                                    //calculate new coords.
                                    break;

                                case "Forward":
                                    parentSerialTerminal.Send_move_specifc((int) DRONE_movement_dir.MOVE_FORWARD,
                                                                           (int) DRONE_movement_metric.METRIC_METERS,
                                                                           Int32.Parse(field2));
                                    break;

                                case "Back":
                                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_BACKWARD,
                                                                           (int)DRONE_movement_metric.METRIC_METERS,
                                                                           Int32.Parse(field2));
                                    break;

                                case "Left":
                                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_LEFT,
                                                                           (int)DRONE_movement_metric.METRIC_METERS,
                                                                           Int32.Parse(field2));
                                    break;

                                case "Right":
                                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_RIGHT,
                                                                           (int)DRONE_movement_metric.METRIC_METERS,
                                                                           Int32.Parse(field2));
                                    break;
                            }
                            break;


                        case "Rotate":
                            /** Create Rotate Packet to send to drone **/
                            switch (field1)
                            {
                                case "Left":
                                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_ROTATE_CLOCKWISE,
                                                                           (int)DRONE_movement_metric.METRIC_DEGREES,
                                                                           Int32.Parse("-" + field2));
                                    break;

                                case "Right":
                                    parentSerialTerminal.Send_move_specifc((int)DRONE_movement_dir.MOVE_ROTATE_CLOCKWISE,
                                                                           (int)DRONE_movement_metric.METRIC_DEGREES,
                                                                           Int32.Parse(field2));
                                    break;
                            }
                            break;


                        case "GPS":
                            field3 = p.Controls[j + 2].Text; //only used for GPS packet.
                            /** Create GPS Packet to send to drone **/
                            break;

                        default:
                            break;
                    }

                    // FORM/SEND PACKET
                }
            }

        }

        private bool checkInstructCompletion(Control p)
        {
            if (!(p.Controls[1] is Button)) return false;
            if ((p.Controls[0].Text == "Move" || p.Controls[0].Text == "Rotate" || p.Controls[0].Text == "GPS")     &&
                (p.Controls[2].Text != "Long" && p.Controls[2].Text != ""       || (p.Controls[2].Text == "Forward" || 
                 p.Controls[2].Text == "Back" || p.Controls[2].Text == "Left"   || p.Controls[2].Text == "Right"))  &&
                (p.Controls[3].Text != "Lat"  && p.Controls[3].Text != ""       && p.Controls[3].Text != "0")       &&
                (p.Controls[4].Text != "Alt"  && p.Controls[4].Text != ""))
            {
                return true;
            }
            else return false;
        }

        private void openManualControls(object sender, EventArgs e)
        {
            mc = new ManualControl(parentSerialTerminal);
            mc.Show();
        }

        private void openDebugPanel(object sender, EventArgs e)
        {
            dbg = new DebugPanel(parentSerialTerminal);
            dbg.Show();
        }


    }
}
