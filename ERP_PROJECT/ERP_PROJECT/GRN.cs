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
    public partial class GRN : Form
    {
        public GRN()
        {
            InitializeComponent();
        }

        Form2 a = new Form2();
        OleDbCommand cmd;
        OleDbDataReader dr;
        private void GRN_Load(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select POID from PO where  Status='Open'",  a.oleDbConnection1);
             dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr["POID"]);
            a.oleDbConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select pid, PQty,POID from POProducts where POID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                dataGridView1.Rows.Add(dr["POID"].ToString(), dr["pid"].ToString());
            a.oleDbConnection1.Close();
            textBox1.Text = "GRN/" + comboBox1.Text;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("update PO set Status='close' where POID='" + comboBox1.Text + "';", a.oleDbConnection1);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("select count(GRNID) from GRN;", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            int sno = 0;
            if (dr.Read())
                sno = Convert.ToInt32(dr[0]) + 1;
            cmd = new OleDbCommand("select VName, PODate, VCPPH from PO where POID='" + comboBox1.Text + "'", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cmd = new OleDbCommand("insert into GRN Values(@GRNID,@POID,'Open',@VName,@DDate,@GRDate,0)", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@GRNID", textBox1.Text);
                cmd.Parameters.AddWithValue("@POID", comboBox1.Text);
                cmd.Parameters.AddWithValue("@VName", dr["VName"].ToString());
                //cmd.Parameters.AddWithValue("@DCDate", dr["PODate"].ToString());
                cmd.Parameters.AddWithValue("@DDate", dr["PODate"].ToString());
                cmd.Parameters.AddWithValue("@GRDate", System.DateTime.Now.ToString());
                //cmd.Parameters.AddWithValue("@SNO", sno);
                cmd.ExecuteReader();
            }
            cmd = new OleDbCommand("select pid, PQty,POID from POProducts where POID='" + comboBox1.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmd = new OleDbCommand("insert into GRNProducts Values(@GRNID,@PModel,@PQty);", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@GRNID", textBox1.Text);
                cmd.Parameters.AddWithValue("@PModel", dr["POID"]);
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
