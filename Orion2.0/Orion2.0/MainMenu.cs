using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orion2._0
{
    public partial class MainMenu : Form
    {
        private bool clickedBtn = false;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //Загрузка имени пользователя к статус-бару
            IsMdiContainer = true;
            toolStripStatusLabel1.Text = DataBank.UserFIO;
        }
        //Кнопки меню

        private void оформлениеГруппToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeGroupForm makeGroupForm = new MakeGroupForm();
            makeGroupForm.MdiParent = this;
            makeGroupForm.Show();
        }

    }
}
