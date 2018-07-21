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
    public partial class Invoice : Form
    {
        Form2 a = new Form2();
         
        OleDbCommand cmd;
        OleDbDataReader dr;
        int total = 0;
      

        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
     
            cmd = new OleDbCommand("select GRNID from GRN where status='Open';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr["GRNID"]);
            a.oleDbConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select POID, VName, DDate from GRN where GRNID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["POID"].ToString();
                textBox2.Text = dr["VName"].ToString();
                textBox3.Text = dr["DDate"].ToString();
                cmd = new OleDbCommand("select TotalAmount from PO where POID='" + dr["POID"] + "';", a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    total = Convert.ToInt32(dr["TotalAmount"]);
                textBox4.Text = "Rs." + total.ToString();
               
                             }
     
            a.oleDbConnection1.Close();
            button1.Enabled = true;

            dataGridView1.Rows.Clear();
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select POID,pid, PQty from POProducts where POID='" + textBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                dataGridView1.Rows.Add(dr["pid"].ToString(), dr["PQty"].ToString());
            cmd = new OleDbCommand("select count(InvoiceID) from Invoice;", a.oleDbConnection1);
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
            cmd = new OleDbCommand("select GRDate from GRN where GRNID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
                RDate = Convert.ToDateTime(dr["GRDate"]);
            cmd = new OleDbCommand("select PODate, DDate, VName, VID,  VCPPH from PO where POID='" + textBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
             
               // ccmd = new OleDbCommand("insert into Invoice(InvoiceID,VendorID,VendorName,CPPH,DCDate,GRNDate,CDate,AmountPayable,GRNID) Values(@InvoiceID,@VendorID,@VendorName,@CPPH,@DCDate,@GRNDate,@CDate,@AmountPayable,@GRNID);", a.oleDbConnection1);
               cmd = new OleDbCommand("insert into Invoice(InvoiceID,VendorID,VendorName,ContectPerson,CPPH,DCDate,GRNDate,CDate,AmountPayable,GRNID) values(@InvoiceID,@VendorID,@VendorName,@ContectPerson,@CPPH,@DCDate,@GRNDate,@CDate,@AmountPayable,@GRNID)", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@InvoiceID", this.textBox5.Text);
                cmd.Parameters.AddWithValue("@VendorID", dr["VID"].ToString());
                cmd.Parameters.AddWithValue("@VendorName", dr["VName"].ToString());
                cmd.Parameters.AddWithValue("@ContactPerson", dr["VName"].ToString());
                cmd.Parameters.AddWithValue("@CPPH", dr["VCPPH"].ToString());
                cmd.Parameters.AddWithValue("@DCDate", dr["PODate"].ToString());
                cmd.Parameters.AddWithValue("@GRNDate", RDate);
                cmd.Parameters.AddWithValue("@CDate", System.DateTime.Today.ToString("dd/MM/yyyy"));
               // cmd.Parameters.AddWithValue("@DDate", dr["DDate"].ToString());
               // cmd.Parameters.AddWithValue("@", );
                cmd.Parameters.AddWithValue("@AmountPayable", total);
                this.label2.Text = System.DateTime.Now.ToString();
  
                 
                cmd.Parameters.AddWithValue("@GRNID", this.comboBox1.Text);
                this.label2.Text=System.DateTime.Today.ToString("dd-MM-yyyy");
        
              //  this.label2.Text += textBox5.Text + "," + dr["VID"].ToString() + "," + dr["VName"].ToString() + "," + dr["VCPPH"].ToString() + "," + dr["PODate"].ToString() + "," + RDate + "," + System.DateTime.Now.ToString() + "," +total+","+this.comboBox1.Text;
               cmd.ExecuteNonQuery();

            }

             cmd = new OleDbCommand("update GRN set Status='Close' where GRNID='" + comboBox1.Text + "';", a.oleDbConnection1);
            //cmd.ExecuteNonQuery();
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

    

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
