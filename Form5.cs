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
    public partial class Form5 : Form
    {
        private string id = "";
        private int intRow = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData("");
        }


        private void addParameters(string str)
        {
            
            CRUD.cmd.Parameters.Clear();
            CRUD.cmd.Parameters.AddWithValue("@id", textBox1.Text);
            

            if (str == "Cari" && !string.IsNullOrEmpty(this.id))
            {
                CRUD.cmd.Parameters.AddWithValue("id", this.id);
            }
        }


        private void loadData(string keyword)
        {
            CRUD.sql = "SELECT * FROM slipgaji WHERE id_gaji = " + textBox1.Text;

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
    }
}
