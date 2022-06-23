using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace GajiKu
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "123")
            {
                new GajiKu().Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Username atau Password salah !!");
                textBox1.Clear();
                textBox2.Clear();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "karyawan" && textBox2.Text == "123")
            {
                new Form5().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau Password salah !!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
