using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
           
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


            dataGridView1.ColumnCount = 0;
            int column___count;
            column___count = (dataGridView1.Width - 40) / 32 ;
            for (int i = 0; i <= column___count; i++)
            {
                var imageColumn = new DataGridViewImageColumn();
                dataGridView1.Columns.Add(imageColumn);
            }
            dataGridView1.ColumnCount = column___count;
            dataGridView1.Width = dataGridView1.ColumnCount * 32 + 3;
            dataGridView1.RowCount = (dataGridView1.Height - 40) / 32 ;
            dataGridView1.Height = dataGridView1.RowCount * 32 + 3;
            
             
            for (int i = 0;i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns[i].Resizable = DataGridViewTriState.False;
                dataGridView1.Columns[i].Width = 32;
                
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dataGridView1.Rows[i].Resizable = DataGridViewTriState.False;
                dataGridView1.Rows[i].Height = 32;
            }
          for (int j = 0; j<dataGridView1.ColumnCount; j++)
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[j, i].Value = fubber_button.Image;
            }
        }
        
        void unselected()
        {
            button1.BackColor = button2.BackColor = button3.BackColor = button4.BackColor = button5.BackColor = button6.BackColor = button7.BackColor = button8.BackColor = button9.BackColor = button10.BackColor = button11.BackColor = button12.BackColor = button13.BackColor = button14.BackColor = button15.BackColor = button16.BackColor = button17.BackColor = button18.BackColor = button19.BackColor = button20.BackColor = button21.BackColor = button22.BackColor = button23.BackColor = button24.BackColor = button26.BackColor = Color.White;
        }

       
        private void selected(object sender, EventArgs e)
        {
           buffer_label.Image = (sender as Button).Image;
            unselected();
            (sender as Button).BackColor = Color.Green;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button12_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void cell_selected(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = buffer_label.Image;
        }

        private void rec_selected(object sender, EventArgs e)
        {
            buffer_label.Image = fubber_button.Image;
            unselected();
             buffer_label.Text = "";
            (sender as Button).BackColor = Color.Green;
        }

        private void стеретьНахренToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[j, i].Value = fubber_button.Image;
                }
        }

        private void app_start(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.LightGreen;
            button_start.Visible = false;
            button_stop.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void app_stop(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.White;
            button_start.Visible = true;
            button_stop.Visible = false;
            panel1.Visible = true;
            panel2.Visible = true;
        }

        private void image_rotate(object sender, EventArgs e)
        {
            
            button24.BackColor = Color.Green;
            buffer_label.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            
        }
    }
}
