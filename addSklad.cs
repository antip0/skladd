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
    public partial class addSklad : Form
    {
        string script1 = "select warehouse_address, warehouse_owner from warehouse;";
        public addSklad()
        {
            InitializeComponent();
            get_info(script1);
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

            string script = "insert into warehouse (warehouse_address, warehouse_owner) values ('" + textBox1.Text + "', '" + textBox2.Text + "');";
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                get_info(script + script1);
            }

            else if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Заполните пустые поля!");
            }

            else if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните адрес!");
            }

            else if (textBox2.Text == "")
            {
                MessageBox.Show("Заполните ФИО собственника!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addSklad_Load(object sender, EventArgs e)
        {

        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AEDsklad aEDsklad = new AEDsklad();
            this.Hide();
            aEDsklad.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            get_info(script1);
        }
    }
}
