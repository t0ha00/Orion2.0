using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Orion2._0
{
    public partial class AuthorizationForm : Form
    {
        private string selectedState = null;
        private string filialID = null;
        private SqlConnection sqlConnection = null;
        private DataSet Ds = new DataSet();
        private DataSet Ds2 = new DataSet();
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["OrionDB"].ConnectionString);
            try { 
                sqlConnection.Open();
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(
                    "SELECT Имя, Код FROM Филиалы ORDER BY Имя",
                    sqlConnection);

                dataAdapter2.Fill(Ds2, "Филиалы");

                foreach (DataRow Row in Ds2.Tables["Филиалы"].Rows)
                {
                    comboBoxPlace.Items.Add(Row["Имя"].ToString());
                }

                picSQLstatus.Visible = true;
            }
            catch
            {
                MessageBox.Show("Не удалось подключится к Базе данных!");
                picSQLstatus.Image = System.Drawing.Image.FromFile("Inages/icons8-удалить-базу-данных-48.png");
                picSQLstatus.Visible = true;
                comboBoxLogin.Visible = false;
                button1.Visible = false;
                comboBoxPlace.Visible = false;
                textBoxPass.Visible = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                throw;
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
                foreach (DataRow row in Ds.Tables["Сотрудники"].Rows)
                {
                    if (row["ФИО"].ToString() == selectedState)
                    {
                        if(textBoxPass.Text.ToString() == row["Пароль"].ToString())
                        {
                            DataBank.UserFIO = row["ФИО"].ToString();
                            sqlConnection.Close();
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

            foreach (DataRow Row in Ds2.Tables["Филиалы"].Rows)
            {
                if (Row["Имя"].ToString() == comboBoxPlace.SelectedItem.ToString())
                {
                    filialID = Row["Код"].ToString();
                }

            }

            //Очищение списка
            Ds.Clear();
            comboBoxLogin.Items.Clear();
            comboBoxLogin.ResetText();

            SqlDataAdapter dataAdapter2 = new SqlDataAdapter(
                    "SELECT ФИО, Пароль FROM Сотрудники WHERE Код_филиала =" + filialID + " ORDER BY ФИО",
                    sqlConnection);
            dataAdapter2.Fill(Ds, "Сотрудники");
            
            foreach (DataRow Row in Ds.Tables["Сотрудники"].Rows)
            {
                comboBoxLogin.Items.Add(Row["ФИО"].ToString());
            }
        }

        private void comboBoxLogin_Click(object sender, EventArgs e)
        {
            if (filialID == null)
            {
                MessageBox.Show("Сначала выберите Филиал!");
            }
        }
    }
}
