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
    public partial class Customer : Form
    {
        Form2 a = new Form2();
        OleDbCommand cmd;
        OleDbDataReader dr;
        string selection;
        public Customer(string s)
        {
            InitializeComponent();
            selection = s;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            if (selection == "add")
            {
                this.Text = "Add Customer";
                textBox11.Text = "Inactive";
                textBox11.ReadOnly = true;
                comboBox2.Visible = false;

               
                cmd = new OleDbCommand("select deptname from Dept",  a.oleDbConnection1);
               dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["deptname"]);
                }
            }
            else if (selection == "approve")
            {
                this.Text = "Approve Customer";
                cmd = new OleDbCommand("select CID from Customer where CStatus='Inactive';",  a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["CID"]);
                }
                a.oleDbConnection1.Close();
                button2.Text = "Approve";
                textBox1.Visible = false;
                comboBox2.Visible = true;
                comboBox1.Enabled = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                textBox11.ReadOnly = true;
            }
            else if (selection == "search")
            {
                this.Text = "Search Customer";
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                textBox1.Visible = false;
                comboBox2.Visible = true;
                comboBox1.Enabled = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                textBox11.ReadOnly = true;
                cmd = new OleDbCommand("select CID from Customer;", a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["CID"]);
                }
            }
            else if (selection == "update")
            {
                this.Text = "Update Customer";
                button2.Text = "Update";
                cmd = new OleDbCommand("select CID from Customer;",  a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["CID"]);
                }
                textBox1.Visible = false;
                comboBox1.Enabled = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox9.ReadOnly = false;
                textBox10.ReadOnly = false;
                textBox11.ReadOnly = true;
            }
           a.oleDbConnection1.Close();
        }



        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
           // Program.f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selection == "add")
            {
                 a.oleDbConnection1.Open();
                cmd = new OleDbCommand("insert into Customer Values(@CID,@Cname,@CAddress,@City,@PH1,@PH2,@ContactPerson,@CPPH,@CEmail,@CreditLimit,@CStatus,@CGroup)",  a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@CID", textBox1.Text);
                cmd.Parameters.AddWithValue("@Cname", textBox2.Text);
                cmd.Parameters.AddWithValue("@CAddress", textBox3.Text);
                cmd.Parameters.AddWithValue("@City", textBox4.Text);
                cmd.Parameters.AddWithValue("@PH1", textBox5.Text);
                cmd.Parameters.AddWithValue("@PH2", textBox6.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", textBox7.Text);
                cmd.Parameters.AddWithValue("@CPPH", textBox8.Text);
                cmd.Parameters.AddWithValue("@CEmail", textBox9.Text);
                cmd.Parameters.AddWithValue("@CreditLimit", textBox10.Text);
                cmd.Parameters.AddWithValue("@CStatus", textBox11.Text);
                cmd.Parameters.AddWithValue("@CGroup", comboBox1.Text);
                cmd.ExecuteNonQuery();
                a.oleDbConnection1.Close();
                MessageBox.Show("Record Inserted!");
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                comboBox1.Enabled = false;
            }

            else if (selection == "approve")
            {
                a.oleDbConnection1.Open();
                cmd = new OleDbCommand("update Customer set CStatus='Active' where CID='" + comboBox2.Text + "';",  a.oleDbConnection1);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Activated!");
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                comboBox2.Items.Clear();
                cmd = new OleDbCommand("select CID from Customer where CStatus='Inactive';", a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["CID"]);
                }
                a.oleDbConnection1.Close();
            }
            else if (selection == "update")
            {
                a.oleDbConnection1.Open();
                cmd = new OleDbCommand("update Customer set Cname=@Cname, CAddress=@CAddress, City=@City, PH1=@PH1, PH2=@PH2, ContectPerson=@ContectPerson, CPPH=@CPPH, CEmail=@CEmail, CreditLimit=@CreditLimit where CID=@CID;",  a.oleDbConnection1);
                cmd.Parameters.AddWithValue("@Cname", textBox2.Text);
                cmd.Parameters.AddWithValue("@CAddress", textBox3.Text);
                cmd.Parameters.AddWithValue("@City", textBox4.Text);
                cmd.Parameters.AddWithValue("@PH1", textBox5.Text);
                cmd.Parameters.AddWithValue("@PH2", textBox6.Text);
                cmd.Parameters.AddWithValue("@ContectPerson", textBox7.Text);
                cmd.Parameters.AddWithValue("@CPPH", textBox8.Text);
                cmd.Parameters.AddWithValue("@CEmail", textBox9.Text);
                cmd.Parameters.AddWithValue("@CreditLimit", textBox10.Text);
                cmd.Parameters.AddWithValue("@CID", comboBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated!");
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                cmd = new OleDbCommand("select CID from Customer where CStatus='Inactive';",  a.oleDbConnection1);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["CID"]);
                }
                a.oleDbConnection1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Program.f2.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selection == "add")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                comboBox1.Text = "";
                comboBox1.Enabled = true;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox9.ReadOnly = false;
                textBox10.ReadOnly = false;
            }
            else if (selection == "approve" || selection == "update")
            {
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c = 1;
             a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select count(CID) from Customer where CGroup='" + comboBox1.Text + "';",  a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
                c = Convert.ToInt32(dr[0])+1;
            if (comboBox1.Text == "Consumer")
                textBox1.Text = "CUS/CON-00" + c.ToString() + "/" + System.DateTime.Today.Year;
            else if (comboBox1.Text == "HR")
                textBox1.Text = "CUS/HR-00" + c.ToString() + "/" + System.DateTime.Today.Year;
            else if (comboBox1.Text == "Marketing")
                textBox1.Text = "CUS/MRK-00" + c.ToString() + "/" + System.DateTime.Today.Year;
            else if (comboBox1.Text == "Sales")
                textBox1.Text = "CUS/SAL-00" + c.ToString() + "/" + System.DateTime.Today.Year;
            a.oleDbConnection1.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
             a.oleDbConnection1.Open();
            cmd = new OleDbCommand("select * from Customer where CID='" + comboBox2.Text + "';",  a.oleDbConnection1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                comboBox1.Text = dr["CGroup"].ToString();
                textBox2.Text = dr["Cname"].ToString();
                textBox3.Text = dr["CAddress"].ToString();
                textBox4.Text = dr["City"].ToString();
                textBox5.Text = dr["PH1"].ToString();
                textBox6.Text = dr["PH2"].ToString();
                textBox7.Text = dr["ContectPerson"].ToString();
                textBox8.Text = dr["CPPH"].ToString();
                textBox9.Text = dr["CEmail"].ToString();
                textBox10.Text = dr["CreditLimit"].ToString();
                textBox11.Text = dr["CStatus"].ToString();
            }
            a.oleDbConnection1.Close();
        }



    }

}
