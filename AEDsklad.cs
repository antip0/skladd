using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace skladd
{
    public partial class AEDsklad : Form
    {
        string query = "select * from warehouse;";
        public AEDsklad()
        {
            InitializeComponent();
            get_info(query);
        }

        public void get_info(string query)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            MySqlDataAdapter mySql_dataAdapter = new MySqlDataAdapter(query, connection);
            try
            {
                connection.Open();
                DataTable table = new DataTable();
                mySql_dataAdapter.Fill(table);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addSklad addSklad = new addSklad();
            this.Hide();
            addSklad.Show();
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainMenu mainMenu = new mainMenu();
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "mainMenu")
                    Application.OpenForms[i].Close();
            }
            mainMenu.Show();
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteSklad deleteSklad = new deleteSklad();
            this.Hide();
            deleteSklad.Show();
        }

        private void AEDsklad_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            editSklad editSklad = new editSklad();
            this.Hide();
            editSklad.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string script = "select warehouse_address, warehouse_owner from warehouse;";
            get_info(script);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string script = "select warehouse_address, warehouse_owner from warehouse;";
            get_info(script);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            addSklad addSklad = new addSklad();
            this.Hide();
            addSklad.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            editSklad editSklad = new editSklad();
            this.Hide();
            editSklad.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            deleteSklad deleteSklad = new deleteSklad();
            this.Hide();
            deleteSklad.Show();
        }
    }
}
