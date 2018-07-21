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
    public partial class Form7 : Form
    {
        Form2 a = new Form2();

        OleDbCommand cmd;
        OleDbDataReader dr;



        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("Select SOID from SO where Status='Open'", a.oleDbConnection1);
             dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["SOID"]);
            }
            a.oleDbConnection1.Close();


            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select PModel, PQty,PP from SOProducts where SOID='" + this.comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                dataGridView1.Rows.Add(dr["PModel"].ToString(), dr["PQty"].ToString(),dr["PP"].ToString());
            a.oleDbConnection1.Close();
            textBox1.Text = "DC/" + comboBox1.Text;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("update SO set Status='close' where SOID='" + comboBox1.Text + "';", a.oleDbConnection1);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("select count(DCID) from DC;", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            int sno = 0;
            if (dr.Read())
                sno = Convert.ToInt32(dr[0]) + 1;
            cmd = new OleDbCommand("select CName, DDate, CCPPH from SO where SOID='" + comboBox1.Text + "'", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.label1.Text = "ok";
                cmd = new OleDbCommand("insert into DC(DCID,SOID,Status,Cname,GRDate,DDate) Values(@DCID,@SOID,'Open',@Cname,@GRDate,@DDate)", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@DCID", textBox1.Text);
                cmd.Parameters.AddWithValue("@SOID", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Cname", dr["CName"].ToString());
                //cmd.Parameters.AddWithValue("@DCDate", dr["PODate"].ToString());
           cmd.Parameters.AddWithValue("@DDate", dr["DDate"].ToString());
                cmd.Parameters.AddWithValue("@GRDate", System.DateTime.Now.ToString());
                //cmd.Parameters.AddWithValue("@SNO", sno);
                cmd.ExecuteReader();
            }
            cmd = new OleDbCommand("select PModel, PQty from SOProducts where SOID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmd = new OleDbCommand("insert into DCProducts Values(@DCID,@PModel,@PQty);", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@DCID", textBox1.Text);
                cmd.Parameters.AddWithValue("@PModel", dr["PModel"]);
                cmd.Parameters.AddWithValue("@PQty", dr["PQty"]);
                cmd.ExecuteNonQuery();
            }
            a.oleDbConnection1.Close();
            MessageBox.Show("Record updated!");
            textBox1.Text = "";
            comboBox1.Items.Remove(comboBox1.Text);
            comboBox1.Text = "";
            dataGridView1.Rows.Clear();
        }
    }
}
