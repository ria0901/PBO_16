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
using System.Numerics;

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
            CRUD.sql = "SELECT * FROM karyawan";

            string strKeyword = string.Format("%{0}%", keyword);

            CRUD.cmd = new NpgsqlCommand(CRUD.sql, CRUD.con);
            CRUD.cmd.Parameters.Clear();
            CRUD.cmd.Parameters.AddWithValue("keyword", strKeyword);

            DataTable dt = CRUD.PerformCRUD(CRUD.cmd);

            if (dt.Rows.Count > 0)
            {
                intRow = Convert.ToInt32(dt.Rows.Count.ToString());
            } else
            {
                intRow = 0;
            }

            DataGridView dgv1 = dataGridView1;

            dgv1.MultiSelect = false;
            dgv1.AutoGenerateColumns = true;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv1.DataSource = dt;

            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Nama";
            dgv1.Columns[2].HeaderText = "Alamat";
            dgv1.Columns[3].HeaderText = "No HP";
            dgv1.Columns[4].HeaderText = "E - Mail";
            dgv1.Columns[5].HeaderText = "Password";
            dgv1.Columns[6].HeaderText = "Jenis Kelamin";

            dgv1.Columns[0].Width = 50;
            dgv1.Columns[1].Width = 100;
            dgv1.Columns[2].Width = 100;
            dgv1.Columns[3].Width = 100;
            dgv1.Columns[4].Width = 100;
            dgv1.Columns[5].Width = 100;
            dgv1.Columns[6].Width = 100;

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
            CRUD.cmd.Parameters.AddWithValue("nama", textBox1.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("alamat", textBox2.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("nomorhp", BigInteger.Parse(textBox3.Text.Trim()));
            CRUD.cmd.Parameters.AddWithValue("email", textBox4.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("jenkel", textBox5.Text.Trim());

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

            CRUD.sql = "INSERT INTO karyawan(nama, alamat, nomorhp, email, jenkel) values (@nama, @alamat, @nomorhp, @email, @jenkel)";

            execute(CRUD.sql, "Insert");

            loadData("");

            resetMe();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridView dgv1 = dataGridView1;

                this.id = Convert.ToString(dgv1.CurrentRow.Cells[0].Value);
                button2.Text = "Update (" + this.id + ")";
                button3.Text = "Delete (" + this.id + ")";

                textBox1.Text = Convert.ToString(dgv1.CurrentRow.Cells[1].Value);
                textBox2.Text = Convert.ToString(dgv1.CurrentRow.Cells[2].Value);
                textBox3.Text = Convert.ToString(dgv1.CurrentRow.Cells[3].Value);
                textBox4.Text = Convert.ToString(dgv1.CurrentRow.Cells[4].Value);
                textBox5.Text = Convert.ToString(dgv1.CurrentRow.Cells[6].Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.id))
            {
                MessageBox.Show("Tolong pilih Update");
                return;
            }

            CRUD.sql = "UPDATE karyawan SET nama = @nama, alamat = @alamat, nomorhp = @nomorhp, email = @email, jenkel = @jenkel WHERE id_karyawan = @id::integer";

            execute(CRUD.sql, "Update");
            MessageBox.Show("Telah di update");

            loadData("");

            resetMe();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.id))
            {
                MessageBox.Show("Tolong pilih Delete");
                return;
            }

            CRUD.sql = "DELETE FROM karyawan WHERE id_karyawan = @id::integer";

            execute(CRUD.sql, "Delete");
            MessageBox.Show("Telah di hapus");

            loadData("");

            resetMe();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hrd fm2 = new Hrd();
            fm2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form fm3 = new Form3();
            fm3.ShowDialog();
        }
    }
}
