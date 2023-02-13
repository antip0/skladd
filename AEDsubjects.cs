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
    public partial class AEDsubjects : Form
    {
        public AEDsubjects()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addSubject addSubject = new addSubject();
            addSubject.Owner = this;
            addSubject.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteSubject delete = new deleteSubject();
            this.Hide();
            delete.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            addSubject addSubject = new addSubject();
            this.Hide();
            addSubject.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            deleteSubject delete = new deleteSubject();
            this.Hide();
            delete.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            editSubject edit = new editSubject();
            this.Hide();
            edit.Show();
        }
    }
}
