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
        private SqlConnection sqlConnection = null;
        private DataSet Ds = new DataSet();
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["OrionDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Не удалось подключится к Базе данных!");
            }

            else
            {
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(
                    "SELECT ФИО, Пароль FROM Сотрудники",
                    sqlConnection);
                
                dataAdapter2.Fill(Ds, "Сотрудники");

                foreach (DataRow Row in Ds.Tables["Сотрудники"].Rows)
                {
                    comboBoxLogin.Items.Add(Row["ФИО"].ToString());
                }
            }

            
        }

        void comboBoxLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedState = comboBoxLogin.SelectedItem.ToString();
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
                            sqlConnection.Close();
                            this.Hide();
                            Form1 form1 = new Form1();
                            form1.Show();
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
    }
}
