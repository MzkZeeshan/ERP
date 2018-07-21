using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJECT
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        Form2 a = new Form2();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {/*
            comboBox1.Items.Add("Employee");
            comboBox1.Items.Add("Admin");
            a.oleDbConnection1.Open();

            OleDbCommand cmd = new OleDbCommand("select * from Vendor", a.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;




            a.oleDbConnection1.Close();
          * */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a=textBox1.Text;
            string b=textBox2.Text;
   

            if(a=="admin" && b=="admin" )
            {
                Form3 obj = new Form3();

                obj.Show();
                this.Hide();
            }
      
            else
            { 
            MessageBox.Show("User & password is not correct");
            }
        }
    }
}
