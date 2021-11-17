using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Orion2._0
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private DataSet Ds = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["OrionDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State != ConnectionState.Open) {
               MessageBox.Show("Не удалось подключится к Базе данных!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Группы",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];


        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String searchGroupeText = textBox1.Text;
            if (searchGroupeText == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(
                    "SELECT Код_группы, ФИО, Организация, Должность FROM Состав_группы WHERE Код_группы =" + searchGroupeText,
                    sqlConnection);

                DataSet dataSet2 = new DataSet();

                dataAdapter2.Fill(dataSet2);
                dataGridView2.DataSource = dataSet2.Tables[0];
                dataAdapter2.Fill(Ds, "Состав_группы");
            }
        }



    }

    
}
