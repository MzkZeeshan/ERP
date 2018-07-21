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
    public partial class Form4 : Form
    {
        Form2 a = new Form2();

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //this.textBox2.ReadOnly=true;
            a.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select Count(VID) from Vendor",a.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
           if(dr.Read())
           {
               int id = Convert.ToInt16(dr[0]);
               ++id;

              textBox2.Text= "0"+id.ToString();
           }
           a.oleDbConnection1.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
         // OleDbCommand cmd = new OleDbCommand("insert into Vendor(VName,VID,VCity,PH1,PH2,VAddress,CPName,CPPH,VEmail,VFax,VGroup,VStatus) values(@VName,@VID,@VCity,@PH1,@PH2,@VAddress,@CPName,@CPPH,@VEmail,@VFax,@VGroup,'unactive')",a.oleDbConnection1);
          OleDbCommand md = new OleDbCommand("insert into Vendor(VID,VName,VCity,VEmail,VFax,VAddress,VGroup,VCode,CPName,CPPH,PH1,PH2,VStatus)  VALUES(@VID,@VName,@VCity,@VEmail,@VFax,@VAddress,@VGroup,@VCode,@CPName,@CPPH,@PH1,@PH2,'Unactive')", a.oleDbConnection1);
            md.Parameters.AddWithValue("@VName", this.textBox1.Text);
            md.Parameters.AddWithValue("@VID", this.textBox2.Text);
            md.Parameters.AddWithValue("@VCity", this.textBox3.Text);
            md.Parameters.AddWithValue("@PH1", this.textBox4.Text);
            md.Parameters.AddWithValue("@PH2",this.textBox5.Text);
            md.Parameters.AddWithValue("@VAddress", this.textBox6.Text);
            md.Parameters.AddWithValue("@CPName", this.textBox7.Text);
            md.Parameters.AddWithValue("@CPPH", this.textBox8.Text);
            md.Parameters.AddWithValue("@VEmail", this.textBox9.Text);
            md.Parameters.AddWithValue("@VFax", this.textBox10.Text);
            md.Parameters.AddWithValue("@VGroup", this.textBox11.Text);
          //  cmd.Parameters.AddWithValue("@VStatus", this.textBox11.Text);
            md.Parameters.AddWithValue("@VCode", this.textBox12.Text);


           md.ExecuteNonQuery();
            MessageBox.Show("Record Has been Inserted And The Vender Id is " + textBox2.Text);

            a.oleDbConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            OleDbCommand cmd=new OleDbCommand("Delete from Vendor where VID='"+this.textBox2.Text+"'",a.oleDbConnection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted");
            a.oleDbConnection1.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.ReadOnly = true;
        }
    }
}
