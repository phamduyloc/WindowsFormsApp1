using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet.TBDemo' table. You can move, or remove it, as needed.
            this.tBDemoTableAdapter.Fill(this.databaseDataSet.TBDemo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dtStart.Value.ToShortDateString();
        }

        private string KhoTruc(string input)
        {
            string strResut = "";
            if (input.Length > 1)
            {
                strResut = input.Substring(0, input.Length - 1) + "." + input.Substring(input.Length - 1, 1);
            }
            else if (input.Length == 1)
            {
                strResut = input + ".0";
            }
            return strResut;
        }

        private string Khotruc1(string input)
        {
            string strResult = "";

            if (input.Contains("."))
            {
                strResult = input.Replace(".", "");
            }
            else
            {
                strResult = input;
            }
            return strResult;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Cong viec cua thang 1";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = "Cong viec cua thang 2";
        }

        private void tBDemoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tBDemoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string sql = "SELECT * FROM TBDemo " +
            //    "WHERE Datetime >='" + dtStart.Value + "' AND Datetime <= '" + dtEnd.Value + "'";


            string sql = "SELECT * FROM TBDemo " +
                "WHERE Datetime BETWEEN '" +  dtStart.Value.ToShortDateString() + " 12:00:00 AM' AND '" + dtEnd.Value.ToShortDateString() + " 12:00:00 PM'";


            int i = 0;
            string strConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
            SqlConnection sqlConnection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            sqlConnection = new SqlConnection(strConnection);
            try
            {
                sqlConnection.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, sqlConnection);
                dataAdapter.Fill(ds);
                sqlConnection.Close();


                //for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    MessageBox.Show(ds.Tables[0].Rows[i].ItemArray[1].ToString());
                //}

                tBDemoDataGridView.DataSource = ds.Tables[0];                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
