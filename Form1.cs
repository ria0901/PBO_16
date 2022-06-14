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
    public partial class GajiKu : Form
    {
        private string id = "";
        private int intRow = 0;

        public GajiKu()
        {
            InitializeComponent();
            resetMe();
        }

        private void resetMe()
        {
            this.id = string.Empty;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void GajiKu_Load(object sender, EventArgs e)
        {
            loadData("");
        }

        private void loadData(string keyword)
        {
            //akan juga
        }

        private void execute(string mySQL, string param)
        {
            CRUD.cmd = new NpgsqlCommand(mySQL, CRUD.con);
            addParameters(param);
            CRUD.PerformCRUD(CRUD.cmd);

        }

        private void addParameters(string str)
        {
            CRUD.cmd.Parameters.Clear();
            CRUD.cmd.Parameters.AddWithValue("Nama", textBox1.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("Alamat", textBox2.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("Nomor HP", textBox3.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("E - Mail", textBox4.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("Jenis Kelamin", textBox5.Text.Trim());

            if (str == "Update" || str == "Delete" && !string.IsNullOrEmpty(this.id))
            {
                CRUD.cmd.Parameters.AddWithValue("id", this.id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Masukan Nama");
                return;
            }

            CRUD.sql = "INSERT INTO karyawan(nama, alamat, email, jenisKelamin) values (@nama, @alamat, @email, @jenisKelamin)";

            execute(CRUD.sql, "Insert");

            loadData("");

            resetMe();
        }
    }
}
