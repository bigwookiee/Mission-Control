using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mission_Control
{
    public partial class Form1 : Form
    {
        int instructs = 0;
        public Form1()
        {
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
                dir.Items.Add("North");
                dir.Items.Add("South");
                dir.Items.Add("East");
                dir.Items.Add("West");
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
    }
}
