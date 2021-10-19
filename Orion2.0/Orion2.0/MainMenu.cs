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
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            string[] name = DataBank.UserFIO.Split(' ');
            if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 10)
            {
                lableWelcomeFio.Text = "Доброе утро, " + name[1] + " " + name[2] + "!";
            } 
            else if (DateTime.Now.Hour > 10 && DateTime.Now.Hour <= 16)
            {
                lableWelcomeFio.Text = "Добрый день, " + name[1] + " " + name[2] + "!";
            }
            else
            {
                lableWelcomeFio.Text = "Добрый вечер, " + name[1] + " " + name[2] + "!";
            }
        }

        private void SetingBtn_Click(object sender, EventArgs e)
        {

        }

    }
}
