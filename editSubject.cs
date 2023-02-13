using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace skladd
{
    public partial class editSubject : Form
    {
        string query = "select items.item_id, items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, items.item_amount from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse;";
        public editSubject()
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

        private void LoadProduct()
        { 
            string script = "select items.item_id, items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, items.item_amount from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where item_name like '" + textBox5.Text + "%'";
            MySqlConnection connection = DBUtils.GetDBConnection();
            DataTable table = new DataTable();
            dataGridView1.DataSource = table;
            table.Clear();
                try
                {
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(script, connection);
                    MySqlDataReader reader = cmDB.ExecuteReader();
                    while (reader.Read())
                    {
                        get_info(script);
                    }
                    reader.Close();
                    connection.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void editSubject_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
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
                    comboBox1.Items.Add(reader2.GetString("cat_name"));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Непредвиденная ошибка!" + Environment.NewLine + ex.Message);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string script = "update items set item_name = '" + textBox2.Text + "', item_desc = '" + textBox3.Text + "', item_cat = (select cat_id from category where cat_name = " +
                "'" + comboBox1.Text + "'), item_warehouse = (select warehouse_id from warehouse where warehouse_address = '" + comboBox2.Text + "'), " +
                " item_amount = '" + textBox4.Text + "' where item_id = '" + textBox1.Text + "';";
            try
            {
                if (textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && textBox4.Text != "")
                {
                    get_info(script + query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex.Message);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            get_info(query);
        }

        private void yfpflToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AEDsubjects aEDsubjects = new AEDsubjects();
            this.Hide();
            aEDsubjects.Show();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); 
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox4.Clear();
        }
    }
}
