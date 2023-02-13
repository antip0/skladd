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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace skladd
{
    public partial class deleteSklad : Form
    {
        public static bool flag = false;
        string query = "select * from warehouse;";
        string script1 = "select warehouse_address, warehouse_owner from warehouse;";
        public deleteSklad()
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
                dataGridView1.DataSource = table;
                dataGridView1.ClearSelection();
                mySql_dataAdapter.Fill(table);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
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

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string script = "delete from warehouse where warehouse_address = '" + comboBox1.Text + "' and warehouse_owner = '" + comboBox2.Text + "';";
            try
            {
                if (comboBox1.Text != "" && comboBox2.Text != "")
                {
                    get_info(script + script1);
                    button1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        private void deleteSklad_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            try
            {
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmDB.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString("warehouse_address"));
                }
                reader.Close();
                connection.Close();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
            comboBox2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void yfpflToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AEDsklad aEDsklad = new AEDsklad();
            this.Hide();
            aEDsklad.Show();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            comboBox2.Enabled = true;
            comboBox2.Items.Clear();
            try
            {
                string query1 = "select warehouse_owner from warehouse where warehouse_address = '" + comboBox1.Text + "';";
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query1, connection);
                MySqlDataReader reader1 = cmDB.ExecuteReader();
                while (reader1.Read())
                {
                    comboBox2.Items.Add(reader1.GetString("warehouse_owner"));
                }
                reader1.Close();
                connection.Close();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string query = "select warehouse_address, warehouse_owner from warehouse where warehouse_address = '" + comboBox1.Text + "' and warehouse_owner = '" + comboBox2.Text + "';";
                get_info(query);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            flag = true;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox2.Enabled = false;
            if (flag == true)
            {
                button1.Enabled = true;
                flag = false;
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();

                MySqlConnection connection = DBUtils.GetDBConnection();
                try
                {
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmDB.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString("warehouse_address"));
                    }
                    reader.Close();
                    connection.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }

                try
                {
                    string query1 = "select warehouse_owner from warehouse where warehouse_address = '" + comboBox1.Text + "';";
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(query1, connection);
                    MySqlDataReader reader1 = cmDB.ExecuteReader();
                    while (reader1.Read())
                    {
                        comboBox2.Items.Add(reader1.GetString("warehouse_owner"));
                    }
                    reader1.Close();
                    connection.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            get_info(script1);
        }
    }
}
