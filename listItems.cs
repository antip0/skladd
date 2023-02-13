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
using Org.BouncyCastle.Crypto.Generators;

namespace skladd
{
    public partial class listItems : Form
    {

        string query = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse";
        public listItems()
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
        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void LoadProduct()
        {
            string script = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where item_name like '" + textBox3.Text + "%'";
            string script1 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where item_desc like '" + textBox3.Text + "%'";
            string script2 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where cat_name like '" + textBox3.Text + "%'";
            string script3 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address like '" + textBox3.Text + "%'";
            string script4 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_owner like '" + textBox3.Text + "%'";
            MySqlConnection connection = DBUtils.GetDBConnection();
            DataTable table = new DataTable();
            dataGridView1.DataSource = table;
            table.Clear();
            if (comboBox3.Text == "наименованию")
            {
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

            else if (comboBox3.Text == "описанию")
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(script1, connection);
                    MySqlDataReader reader1 = cmDB.ExecuteReader();
                    while (reader1.Read())
                    {
                        get_info(script1);
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

            else if (comboBox3.Text == "категории")
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(script2, connection);
                    MySqlDataReader reader2 = cmDB.ExecuteReader();
                    while (reader2.Read())
                    {
                        get_info(script2);
                    }
                    reader2.Close();
                    connection.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }

            else if (comboBox3.Text == "адресу склада")
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(script3, connection);
                    MySqlDataReader reader3 = cmDB.ExecuteReader();
                    while (reader3.Read())
                    {
                        get_info(script3);
                    }
                    reader3.Close();
                    connection.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }

            else if (comboBox3.Text == "ФИО собственника склада")
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmDB = new MySqlCommand(script4, connection);
                    MySqlDataReader reader4 = cmDB.ExecuteReader();
                    while (reader4.Read())
                    {
                        get_info(script4);
                    }
                    reader4.Close();
                    connection.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }
            else if (comboBox3.Text == "")
            {
                label6.Visible = true;
                label6.Text = "Выберите критерий поиска!";
                textBox3.Clear();
            }
        }
        private void listItems_Load(object sender, EventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 5000;
            t.Start();
            t.Tick += new EventHandler(t_Tick);

            comboBox1.SelectedItem = "Общий список";

            try
            {
                MySqlConnection connection = DBUtils.GetDBConnection();
                string query1 = "select * from warehouse;";
                connection.Open();
                MySqlCommand cmDB = new MySqlCommand(query1, connection);
                MySqlDataReader reader5 = cmDB.ExecuteReader();
                while (reader5.Read())
                {
                    comboBox1.Items.Add(reader5.GetString("warehouse_address"));
                }
                reader5.Close();
                connection.Close();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            label6.Visible = false;
            label1.Visible = false;
        }
        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainMenu mainMenu = new mainMenu();
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "mainMenu")
                {
                    Application.OpenForms[i].Close();
                }
            }
            mainMenu.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string script = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_name;";
            get_info(script);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            get_info(query);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Clear();
            comboBox1.Text = "Общий список";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Очищать нечего!");
            }
            else
            {
                textBox3.Clear();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string script = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by item_name;";
            string script1 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by item_desc;";
            string script2 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by warehouse_address;";
            string script3 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by warehouse_owner;";
            if (comboBox1.Text == "Общий список")
            {
                if (checkBox1.Checked)
                {
                    string script4 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_name;";
                    get_info(script4);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию наименований прошла успешно!";
                }

                else if (checkBox2.Checked)
                {
                    string script5 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_desc;";
                    get_info(script5);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию описаний прошла успешно!";
                }

                else if (checkBox3.Checked)
                {
                    string script6 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by warehouse_address;";
                    get_info(script6);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию адресов прошла успешно!";
                }

                else if (checkBox4.Checked)
                {
                    string script7 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by warehouse_owner;";
                    get_info(script7);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию собственников прошла успешно!";
                }


                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            }
            else if (comboBox1.Text != "Общий список")
            {
                if (checkBox1.Checked && comboBox1.Text != "Общий список")
                {
                    get_info(script);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию наименований прошла успешно!";
                }

                else if (checkBox2.Checked && comboBox1.Text != "Общий список")
                {
                    get_info(script1);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию описаний прошла успешно!";
                }

                else if (checkBox4.Checked && comboBox1.Text != "Общий список")
                {
                    get_info(script3);
                    label1.Visible = true;
                    label1.Text = "Сортировка по возрастанию собственников прошла успешно!";
                }

                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            }
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string script = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by item_name desc;";
            string script1 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by item_desc desc;";
            string script2 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by warehouse_address desc;";
            string script3 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by warehouse_owner desc;";
            string script4 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_name desc;";
            string script5 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_desc desc;";
            string script6 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by warehouse_address desc;";
            string script7 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by warehouse_owner desc;";
            if (comboBox1.Text == "Общий список")
            {
                if (checkBox1.Checked)
                {                  
                    get_info(script4);
                    label1.Visible = true;
                    label1.Text = "Сортировка по убыванию наименований прошла успешно!";
                }

                else if (checkBox2.Checked)
                {
                    get_info(script5);
                    label1.Visible = true;
                    label1.Text = "Сортировка по убыванию описаний прошла успешно!";
                }

                else if (checkBox3.Checked)
                {
                    get_info(script6);
                    label1.Visible = true;
                    label1.Text = "Сортировка по убыванию адресов прошла успешно!";
                }

                else if (checkBox4.Checked)
                {
                   
                    get_info(script7);
                    label1.Visible = true;
                    label1.Text = "Сортировка по убыванию собственников прошла успешно!";
                }

                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            } 

            else if (checkBox1.Checked)
            {
                get_info(script);
                label1.Visible = true;
                label1.Text = "Сортировка по убыванию наименований прошла успешно!";
            }

            else if (checkBox2.Checked)
            {
                get_info(script1);
                label1.Visible = true;
                label1.Text = "Сортировка по убыванию описаний прошла успешно!";
            }

            else if (checkBox3.Checked)
            {
                get_info(script2);
                label1.Visible = true;
                label1.Text = "Сортировка по убыванию адресов прошла успешно!";
            }

            else if (checkBox4.Checked)
            {
                get_info(script3);
                label1.Visible = true;
                label1.Text = "Сортировка по убыванию собственников прошла успешно!";
            }

            else
            {
                MessageBox.Show("Выберите критерий сортировки!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            if (checkBox1.Checked == false)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            if (checkBox2.Checked == false)
            {
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            if (checkBox3.Checked == false)
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox5.Enabled = false;
            if (checkBox4.Checked == false)
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox5.Enabled = true;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false)
            {
                MessageBox.Show("Очищать нечего!");
            }
            else
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            get_info(query);
            comboBox1.SelectedItem = "Общий список";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query1 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "';";
            if (comboBox1.Text == "Общий список")
            {
                checkBox3.Enabled = true;
                get_info(query);
            }
            else
            {
                checkBox3.Enabled = false;
                get_info(query1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_info(query);
        }

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            pictureBox3.Enabled = false;
            pictureBox4.Enabled = false;
            if (comboBox1.Text == "Общий список")
            {
                string script8 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse order by item_id desc;";
                get_info(script8);
                if (checkBox5.Checked == false)
                {
                    get_info(query);
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                    checkBox4.Enabled = true;
                    pictureBox3.Enabled = true;
                    pictureBox4.Enabled = true;
                }
            }
            else 
            {
                string script8 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "' order by item_id desc;";
                string script9 = "select items.item_name, items.item_desc, category.cat_name, warehouse.warehouse_address, warehouse.warehouse_owner from items join category on category.cat_id = items.item_cat join warehouse on warehouse.warehouse_id = items.item_warehouse where warehouse_address = '" + comboBox1.Text + "';";
                get_info(script8);
                if (checkBox5.Checked == false)
                {
                    get_info(script9);
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                    checkBox4.Enabled = true;
                    pictureBox3.Enabled = true;
                    pictureBox4.Enabled = true;
                }
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
