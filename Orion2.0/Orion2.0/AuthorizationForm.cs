using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Orion2._0
{
    public partial class AuthorizationForm : Form
    {
        private string selectedState = null;
        string filialID = null;
        dynamic array_tp;
        dynamic array_tp_names;
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            bool server_status = false;
            try {
                using (var webClient = new WebClient())
                {
                    if (webClient.DownloadString("http://localhost:5000/") == "OK")
                    {
                        server_status = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось подключится к Базе данных!");
                //picSQLstatus.Image = System.Drawing.Image.FromFile("Inages/icons8-удалить-базу-данных-48.png");
                picSQLstatus.Visible = true;
                comboBoxLogin.Visible = false;
                button1.Visible = false;
                comboBoxPlace.Visible = false;
                textBoxPass.Visible = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
            }
            
            if (server_status) {
                using (var webClient = new WebClient())
                {
                    var json_tp = webClient.DownloadString("http://localhost:5000/get_tp");
                    array_tp = JsonConvert.DeserializeObject(json_tp);
                    foreach (var item in array_tp)
                    {
                        comboBoxPlace.Items.Add(item.NAME.ToString());
                    }
                    picSQLstatus.Visible = true;
                }
            }
        }

        void comboBoxLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filialID == null)
            {
                MessageBox.Show("Сначала выберите Филиал!");
            }
            else
            { 
                selectedState = comboBoxLogin.SelectedItem.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedState != null && textBoxPass.Text != "")
            {
                foreach (var item in array_tp_names)
                {
                    if (item.FIO.ToString() == selectedState)
                    {
                        if(textBoxPass.Text.ToString() == item.PASS.ToString())
                        {
                            DataBank.UserFIO = item.FIO.ToString();
                            this.Hide();
                            MainMenu mainMenu = new MainMenu();
                            mainMenu.Show();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
        }

        private void comboBoxPlace_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (var item in array_tp)
            {
                if (item.NAME.ToString() == comboBoxPlace.SelectedItem)
                {
                    filialID = item.CODE.ToString();
                }
            }

            //Очищение списка
            comboBoxLogin.Items.Clear();
            comboBoxLogin.ResetText();
            _ = get_Names();

            async Task get_Names() {
                picSQLstatus.Image = Properties.Resources.Iphone_spinner_2;
                
                using (WebClient webClient = new WebClient())
                {
                    string req_str = "http://localhost:5000/get_tp_names/" + filialID;
                    var json_tp_name = await webClient.DownloadStringTaskAsync(req_str);
                    array_tp_names = JsonConvert.DeserializeObject(json_tp_name);
                    foreach (var item in array_tp_names)
                    {
                        comboBoxLogin.Items.Add(item.FIO.ToString());
                    }
                }
                picSQLstatus.Image = Properties.Resources.free_icon_avatar_126491;
            }
            
        }

        private void comboBoxLogin_Click(object sender, EventArgs e)
        {
            if (filialID == null)
            {
                MessageBox.Show("Сначала выберите Филиал!");
            }
        }

        private void textBoxPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button1_Click(sender, e);
            }
        }
    }
}
