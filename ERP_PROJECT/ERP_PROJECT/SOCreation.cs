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
    public partial class SOCreation : Form
    {
        Form2 a = new Form2();
        OleDbCommand cmd;
        OleDbDataReader dr;
        string[] prds = new string[50];
        int[] qty = new int[50];
        int counter = 0;
        int price = 0;
        int total = 0;

        public SOCreation()
        {
            InitializeComponent();
        }

        private void SOCreation_Load(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
         ;
         cmd = new OleDbCommand("select CID from Customer where CStatus='Active';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr["CID"]);
            cmd = new OleDbCommand("select PID from Products;", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox2.Items.Add(dr["PID"]);
            a.oleDbConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select * from Customer where CID='" + comboBox1.Text + "';",a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Cname"].ToString();
                textBox3.Text = dr["CAddress"].ToString();
                textBox4.Text = dr["City"].ToString();
                textBox5.Text = dr["PH1"].ToString();
                textBox6.Text = dr["PH2"].ToString();
                textBox7.Text = dr["ContectPerson"].ToString();
                textBox8.Text = dr["CPPH"].ToString();
                textBox9.Text = dr["CEmail"].ToString();
                textBox10.Text = dr["CreditLimit"].ToString();
                textBox11.Text = dr["CGroup"].ToString();
            }
            cmd = new OleDbCommand("select count(SOID) from SO where CDept='" + textBox11.Text + "';",a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            int c = 1;
            if (dr.Read())
                c = Convert.ToInt32(dr[0]);
            a.oleDbConnection1.Close();
            if (textBox11.Text == "Consumer")
                textBox1.Text = "SO/CON-00" + c.ToString() + "/" + System.DateTime.Now.Year;
            else if (textBox11.Text == "HR")
                textBox1.Text = "SO/HR-00" + c.ToString() + "/" + System.DateTime.Now.Year;
            else if (textBox11.Text == "Sales")
                textBox1.Text = "SO/SAL-00" + c.ToString() + "/" + System.DateTime.Now.Year;
            else if (textBox11.Text == "Marketing")
                textBox1.Text = "SO/MRK-00" + c.ToString() + "/" + System.DateTime.Now.Year;
            dateTimePicker1.Enabled = true;
            comboBox2.Enabled = true;
           a.oleDbConnection1.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select PName, BasePrice from Products where PID='" + comboBox2.Text + "';", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox12.Text = dr["PName"].ToString();
                price = Convert.ToInt32(dr["BasePrice"].ToString());
            }
            textBox13.Text = "Rs." + price.ToString();
            a.oleDbConnection1.Close();
            textBox14.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            (new SOCreation()).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "")
            {
                prds[counter] = comboBox2.Text;
                qty[counter] = Convert.ToInt32(textBox14.Text);
                dataGridView1.Rows.Add(comboBox2.Text, price * Convert.ToInt32(textBox14.Text));
                total= price * Convert.ToInt32(textBox14.Text);
                
                label20.Text = (Convert.ToInt32(label20.Text) +total.ToString());
                counter++;
                comboBox2.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox14.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
                MessageBox.Show("Please enter product quantity.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sno = 1;
            a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select count(SOID) from SO;", a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
                sno = Convert.ToInt32(dr[0]);
            cmd = new OleDbCommand("insert into SO(SOID,DDate,DCDate,Status,Approve,CDept,CName,CID,CContactPerson,SNO,GoodsDeliver,TotalAmount) Values(@SOID,@DDate,@DCDate,'Open','Not Approved',@CDept,@CName,@CID,@CContactPerson,@SNO,'No',@TotalAmount);", a.oleDbConnection1);
            cmd.Parameters.AddWithValue("@SOID", textBox1.Text);
            cmd.Parameters.AddWithValue("@DCDate", dateTimePicker1.Value.ToString());
            cmd.Parameters.AddWithValue("@DDate", dateTimePicker1.Value.ToString());
            cmd.Parameters.AddWithValue("@CDept", textBox11.Text);
            cmd.Parameters.AddWithValue("@CName", textBox2.Text);
            cmd.Parameters.AddWithValue("@CID", comboBox1.Text);
            cmd.Parameters.AddWithValue("@CContactPerson", textBox7.Text);
        cmd.Parameters.AddWithValue("@CCPPH", Convert.ToInt64( textBox8.Text));
            cmd.Parameters.AddWithValue("@TotalAmount",Convert.ToInt64( label20.Text));
            cmd.Parameters.AddWithValue("@SNO", sno);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < counter; i++)
            {
                cmd = new OleDbCommand("insert into SOProducts Values(@SOID,@PModel,@PQty,@PP);", a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@SOID", textBox1.Text);
                cmd.Parameters.AddWithValue("@PModel", prds[i]);
                cmd.Parameters.AddWithValue("@PQty", qty[i]);
                cmd.Parameters.AddWithValue("@PP", total);
                cmd.ExecuteNonQuery();
            } a.oleDbConnection1.Close();
            MessageBox.Show("Order inserted!");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
