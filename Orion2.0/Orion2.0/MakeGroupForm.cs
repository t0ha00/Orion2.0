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
    public partial class MakeGroupForm : Form
    {
        public delegate void ChangeV();
        public event ChangeV ChangeV1;

        public MakeGroupForm()
        {
            InitializeComponent();
            MainMenu mainMenu = new MainMenu();
            ChangeV1 += mainMenu.ChangeVisibility;
        }

        private void MakeGroupForm_Load(object sender, EventArgs e)
        {
            
        }

        private void openedGrBtn_Click(object sender, EventArgs e)
        {

        }

        private void closedGrBtn_Click(object sender, EventArgs e)
        {
            ChangeV1();
        }

        private void openedGrBtn_Click_1(object sender, EventArgs e)
        {
            ChangeV1();
        }
    }
}
