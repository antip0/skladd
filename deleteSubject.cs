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
    public partial class deleteSubject : Form
    {
        string script = "select item_name, item_desc, item_cat, item_warehouse, item_amount from items;";
        public static bool flag = false;
        public deleteSubject()
        {
            InitializeComponent();
            get_info(script);
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
            AEDsubjects aEDsubjects = new AEDsubjects();
            this.Hide();
            aEDsubjects.Show();
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void deleteSubject_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox3.Enabled = true;
            comboBox2.Enabled = false;
            string query = "select * from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse;";
            string query1 = "select * from warehouse;";
            string query2 = "select * from category;";

            try
            {
                MySqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query1, connection);
                MySqlDataReader reader1 = cmDB.ExecuteReader();
                while (reader1.Read())
                {
                    comboBox2.Items.Add(reader1.GetString("warehouse_address"));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Непредвиденная ошибка!" + Environment.NewLine + ex.Message);
            }

            try
            {
                MySqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query2, connection);
                MySqlDataReader reader2 = cmDB.ExecuteReader();
                while (reader2.Read())
                {
                    comboBox3.Items.Add(reader2.GetString("cat_name"));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Непредвиденная ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            string script = "delete items from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where items.item_name = '" + comboBox1.Text + "' and category.cat_name = '" + comboBox3.Text + "' and warehouse.warehouse_address = '" + comboBox2.Text + "';";
            string script1 = "select item_name, item_desc, item_cat, item_warehouse, item_amount from items;";
            string query1 = "select items.item_name from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where category.cat_name = '" + comboBox3.Text + "' and warehouse.warehouse_address = '" + comboBox2.Text + "';";
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                comboBox1.Items.Clear();
                get_info(script + script1);
                try
                {                   
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(query1, connection);
                    MySqlDataReader reader1 = cmDB.ExecuteReader();
                    while (reader1.Read())
                    {
                        comboBox1.Items.Add(reader1.GetString("item_name"));
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
            else
            {
                MessageBox.Show("Заполните все пустые поля!");
            }
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = false;
            string query = "select items.item_name, category.cat_name, warehouse.warehouse_address from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where items.item_name = '" + comboBox1.Text + "' and category.cat_name = '" + comboBox3.Text + "' and warehouse.warehouse_address = '" + comboBox2.Text + "';";
            get_info(query);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select items.item_name, category.cat_name, warehouse.warehouse_address from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where category.cat_name = '" + comboBox3.Text + "' and warehouse.warehouse_address = '" + comboBox2.Text + "';";
            comboBox3.Enabled = false;
            comboBox1.Enabled = true;
            comboBox1.Items.Clear();
            MySqlConnection connection = DBUtils.GetDBConnection();
            if (comboBox2.Text != "")
            {
                get_info(query);
                if (dataGridView1.Rows.Count == 1)
                {
                    MessageBox.Show("Ничего не найдено!");
                    button1.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                }
                else
                {
                    try
                    {
                        string query1 = "select items.item_name from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where category.cat_name = '" + comboBox3.Text + "' and warehouse.warehouse_address = '" + comboBox2.Text + "';";
                        connection.Open();
                        MySqlCommand cmDB = new MySqlCommand(query1, connection);
                        MySqlDataReader reader1 = cmDB.ExecuteReader();
                        while (reader1.Read())
                        {
                            comboBox1.Items.Add(reader1.GetString("item_name"));
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

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            get_info(script);
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AEDsubjects aEDsubjects = new AEDsubjects();
            this.Hide();
            aEDsubjects.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string script = "select item_name, item_desc, item_cat, item_warehouse, item_amount from items;";
            flag = true;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            if (flag == true)
            {
                get_info(script);
                comboBox3.Enabled = true;
                comboBox2.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = true;
                flag = false;
            }
        }
    }
}
