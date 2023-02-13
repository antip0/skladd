using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
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
    public partial class addSubject : Form
    {
        public static bool flag = false;
        string query = "select * from category;";
        string query1 = "select * from warehouse;";
        string query2 = "select item_name, item_desc, category.cat_name, warehouse.warehouse_address, item_amount from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_id desc;";
        public addSubject()
        {
            InitializeComponent();
            get_info(query2);

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
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            string script2 = "insert into items (item_cat, item_warehouse) select cat_id, warehouse_id from category join warehouse where cat_name = '" + comboBox1.Text + "' and warehouse_address = '" + comboBox2.Text + "';";          
            string script = "update items set item_name = '" + textBox1.Text + "', item_desc = '" + textBox2.Text + "', item_amount = '" + textBox3.Text + "' order by item_id desc limit 1;";
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox2.Text != "" && comboBox1.Text != "")
            {
                get_info(script2 + script + query2);
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Необходимо заполнить все пустые поля!");
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            get_info(query2);
        }

        private void addSubject_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();

            try
            {
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmDB.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString("cat_name"));
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
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query1, connection);
                MySqlDataReader reader1 = cmDB.ExecuteReader();
                while (reader1.Read())
                {
                    comboBox2.Items.Add(reader1.GetString("warehouse_address"));
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            flag = true;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            if (flag == true)
            {
                button1.Enabled = true;
                flag = false;
            }
        }

        private void назадToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AEDsubjects aEDsubjects = new AEDsubjects();
            this.Hide();
            aEDsubjects.Show();
        }
    }
}
