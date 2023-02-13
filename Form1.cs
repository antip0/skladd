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
    public partial class mainMenu : Form
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listItems listItems = new listItems();
            this.Hide();
            listItems.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AEDsubjects aEDsubjects = new AEDsubjects();
            this.Hide();
            aEDsubjects.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AEDsklad aEDsklad = new AEDsklad();
            this.Hide();
            aEDsklad.Show();
        }
    }
}
