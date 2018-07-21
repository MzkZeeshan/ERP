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
    public partial class Form8 : Form
    {
        Form2 a = new Form2();

        OleDbCommand cmd;
        OleDbDataReader dr;
        int total = 0;

        public Form8()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();

            cmd = new OleDbCommand("select DCID from DC where status='Open';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr["DCID"]);
            a.oleDbConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select SOID, Cname, DDate from DC where DCID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["SOID"].ToString();
                textBox2.Text = dr["Cname"].ToString();
                textBox3.Text = dr["DDate"].ToString();
                cmd = new OleDbCommand("select TotalAmount from SO where SOID='" + dr["SOID"] + "';", a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    total = Convert.ToInt32(dr["TotalAmount"]);
                textBox4.Text = "Rs." + total.ToString();

            }

            a.oleDbConnection1.Close();
            button1.Enabled = true;

            dataGridView1.Rows.Clear();
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select SOID,PModel, PQty from SOProducts where SOID='" + textBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                dataGridView1.Rows.Add(dr["PModel"].ToString(), dr["PModel"].ToString());
            cmd = new OleDbCommand("select count(InvoiceID) from invoicer;", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
                textBox5.Text = (Convert.ToInt32(dr[0]) + 1).ToString();
            a.oleDbConnection1.Close();
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();

            DateTime RDate = System.DateTime.Now;
            cmd = new OleDbCommand("select GRDate from DC where DCID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
                RDate = Convert.ToDateTime(dr["GRDate"]);
            cmd = new OleDbCommand("select DCDate, DDate, Cname, CID,CContactPerson,SOID,  CCPPH from SO where SOID='" + textBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                // ccmd = new OleDbCommand("insert into Invoice(InvoiceID,VendorID,VendorName,CPPH,DCDate,GRNDate,CDate,AmountPayable,GRNID) Values(@InvoiceID,@VendorID,@VendorName,@CPPH,@DCDate,@GRNDate,@CDate,@AmountPayable,@GRNID);", a.oleDbConnection1);
                cmd = new OleDbCommand("insert into Invoicer(InvoiceID,CustomerID,CustomerName,ContectPerson,CPPH,DChdate,CDate,AmountRecievable,DCID,DCDate) values(@InvoiceID,@CustomerID,@CustomerName,@ContectPerson,@CPPH,@DChdate,@CDate,@AmountRecievable,@DCID,@DCDate)", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@InvoiceID", this.textBox5.Text);
                cmd.Parameters.AddWithValue("@CustomerID", dr["CID"].ToString());
                cmd.Parameters.AddWithValue("@CustomerName", dr["Cname"].ToString());
                cmd.Parameters.AddWithValue("@ContactPerson", dr["CContactPerson"].ToString());
                cmd.Parameters.AddWithValue("@CPPH", dr["CCPPH"].ToString());
                cmd.Parameters.AddWithValue("@DChdate", RDate.ToString());
                cmd.Parameters.AddWithValue("@CDate", System.DateTime.Today.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@AmountRecievable", total);
                cmd.Parameters.AddWithValue("@DCID", this.comboBox1.Text);
                cmd.Parameters.AddWithValue("@DCDate", dr["DCDate"].ToString());
               
               
                // cmd.Parameters.AddWithValue("@", );
    

                //  this.label2.Text += textBox5.Text + "," + dr["VID"].ToString() + "," + dr["VName"].ToString() + "," + dr["VCPPH"].ToString() + "," + dr["PODate"].ToString() + "," + RDate + "," + System.DateTime.Now.ToString() + "," +total+","+this.comboBox1.Text;
                cmd.ExecuteNonQuery();

            }

            cmd = new OleDbCommand("update DC set Status='Close' where DCID='" + this.comboBox1.Text + "';", a.oleDbConnection1);
            cmd.ExecuteNonQuery();
            a.oleDbConnection1.Close();
            MessageBox.Show("Invoice created!");
            comboBox1.Items.Remove(comboBox1.Text);
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
           
            dataGridView1.Rows.Clear();
            button1.Enabled = false;
        }
    }
}
