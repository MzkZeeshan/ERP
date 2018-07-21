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

    public partial class POApproval : Form
    {
        Form2 a = new Form2();
        OleDbCommand cmd;
        OleDbCommand cmd1;
        OleDbDataReader dr;
        OleDbDataReader dr1;
        int gtotal = 0;
        public POApproval()
        {
            InitializeComponent();
        }

        private void POApproval_Load(object sender, EventArgs e)
        {
     
  a.oleDbConnection1.Open();
  cmd = new OleDbCommand("select POID from PO where Status='Open' and Approve='Not Approved';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr["POID"]);
            a.oleDbConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select DDate, VName, VID from PO where POID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["VID"].ToString();
                textBox2.Text = dr["VName"].ToString();
                textBox3.Text = dr["DDate"].ToString();
            }
            cmd = new OleDbCommand("select pid, PQty from POProducts where POID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmd1 = new OleDbCommand("select PName, BasePrice from Products where pid='" + dr["pid"].ToString() + "';", a.oleDbConnection1);
                dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                    dataGridView1.Rows.Add(dr1["PName"].ToString(), dr1["PName"].ToString(), dr1["BasePrice"].ToString(), dr["PQty"].ToString(), Convert.ToInt32(dr1["BasePrice"]) * Convert.ToInt32(dr["PQty"]));
                gtotal += Convert.ToInt32(dr1["BasePrice"]) * Convert.ToInt32(dr["PQty"]);
            }
            label7.Text = "Rs." + gtotal.ToString();
            a.oleDbConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("update PO set Approve='Approved' where POID='" + comboBox1.Text + "';", a.oleDbConnection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order approved!");
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label7.Text = "Rs.0";
            cmd = new OleDbCommand("select POID from PO where Status='Open' and Approve='Not Approved';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr["POID"]);
            a.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
