using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace GajiKu
{
    class CRUD
    {
        private static string getConnectionString()
        {
            string host = "Host=localhost;";
            string port = "Port=5432;";
            string db = "Database=gaji;";
            string user = "Username=postgres;";
            string pass = "Password=jetbus3;";

            string conString = string.Format("{0}{1}{2}{3}{4}", host, port, db, user, pass);
            return conString;
        }

        public static NpgsqlConnection con = new NpgsqlConnection(getConnectionString());
        public static NpgsqlCommand cmd = default(NpgsqlCommand);
        public static string sql = string.Empty;

        public static DataTable PerformCRUD(NpgsqlCommand com)
        {
            NpgsqlDataAdapter da = default(NpgsqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {

                da = new NpgsqlDataAdapter();
                da.SelectCommand = com;
                da.Fill(dt);

                return dt;

            } catch (Exception ex)
            {
                MessageBox.Show("Ada error !!" + ex.Message);
                dt = null;
            }

            return dt;
        }
    }
}
