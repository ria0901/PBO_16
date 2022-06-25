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
    public partial class Form3: Form
    {
        private string id = "";
        private int intRow = 0;
        public Form3()
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

        private void Form3_Load(object sender, EventArgs e)
        {
            loadData("");
        }

        private void loadData(string keyword)
        {
            CRUD.sql = "SELECT * FROM slipgaji";

            string strKeyword = string.Format("%{0}%", keyword);

            CRUD.cmd = new NpgsqlCommand(CRUD.sql, CRUD.con);
            CRUD.cmd.Parameters.Clear();
            CRUD.cmd.Parameters.AddWithValue("keyword", strKeyword);

            DataTable dt = CRUD.PerformCRUD(CRUD.cmd);

            if (dt.Rows.Count > 0)
            {
                intRow = Convert.ToInt32(dt.Rows.Count.ToString());
            }
            else
            {
                intRow = 0;
            }

            DataGridView dgv1 = dataGridView1;

            dgv1.MultiSelect = false;
            dgv1.AutoGenerateColumns = true;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv1.DataSource = dt;

            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Tanggal Periode";
            dgv1.Columns[2].HeaderText = "Gaji Pokok";
            dgv1.Columns[3].HeaderText = "Tunjangan";
            dgv1.Columns[4].HeaderText = "THR";
            dgv1.Columns[5].HeaderText = "Lemburan";
            

            dgv1.Columns[0].Width = 50;
            dgv1.Columns[1].Width = 100;
            dgv1.Columns[2].Width = 100;
            dgv1.Columns[3].Width = 100;
            dgv1.Columns[4].Width = 100;
            dgv1.Columns[5].Width = 100;
            

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
            CRUD.cmd.Parameters.AddWithValue("tanggalperiode", textBox1.Text.Trim());
            CRUD.cmd.Parameters.AddWithValue("gajipokok", int.Parse(textBox2.Text.Trim()));
            CRUD.cmd.Parameters.AddWithValue("tunjangan", int.Parse(textBox3.Text.Trim()));
            CRUD.cmd.Parameters.AddWithValue("thr", int.Parse(textBox4.Text.Trim()));
            CRUD.cmd.Parameters.AddWithValue("lemburan", int.Parse(textBox5.Text.Trim()));

            if (str == "Delete" && !string.IsNullOrEmpty(this.id))
            {
                CRUD.cmd.Parameters.AddWithValue("id", this.id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Masukan Gaji");
                return;
            }

            CRUD.sql = "INSERT INTO slipgaji(tanggalperiode, gajipokok, tunjangan, thr, lemburan) values (@tanggalperiode, @gajipokok, @tunjangan, @thr, @lemburan)";

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
                
                button2.Text = "Delete (" + this.id + ")";

                textBox1.Text = Convert.ToString(dgv1.CurrentRow.Cells[1].Value);
                textBox2.Text = Convert.ToString(dgv1.CurrentRow.Cells[2].Value);
                textBox3.Text = Convert.ToString(dgv1.CurrentRow.Cells[3].Value);
                textBox4.Text = Convert.ToString(dgv1.CurrentRow.Cells[4].Value);
                textBox5.Text = Convert.ToString(dgv1.CurrentRow.Cells[5].Value);
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
                MessageBox.Show("Tolong pilih Delete");
                return;
            }

            CRUD.sql = "DELETE FROM slipgaji WHERE id_gaji = @id::integer";

            execute(CRUD.sql, "Delete");
            MessageBox.Show("Telah di hapus");

            loadData("");

            resetMe();
        }
    }
}
