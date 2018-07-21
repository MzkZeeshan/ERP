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
    public partial class Form6 : Form
    {
        Form2 a = new Form2();

        int i;
        int counter = 0;
        string[] P = new string[50];
        string[] Productid = new string[50];
        int[] PP = new int[50];
        string[] PQ = new string[50];
        
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            i = 0;
            this.dateTimePicker2.Visible = false;
            //this.label.Text = this.dateTimePicker2.Value.ToString;
            a.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select VGroup from Vendor", a.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["VGroup"]);
            }
            a.oleDbConnection1.Close(); 

           


            a.oleDbConnection1.Open();
            OleDbCommand cmd1 = new OleDbCommand("Select Pid from Products", a.oleDbConnection1);
            OleDbDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1["Pid"]);
            }
            a.oleDbConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();

            
            OleDbCommand cmd = new OleDbCommand("Select * from Vendor where VID='"+this.comboBox1.Text+"'", a.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.textBox4.Text = dr["VName"].ToString();
                this.textBox7.Text = dr["PH1"].ToString();
              // this.textBox11.Text = dr["VGroup"].ToString();
             

            }
            
            a.oleDbConnection1.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();


            OleDbCommand cmd = new OleDbCommand("Select * from Products where Pid='" + this.comboBox2.Text + "'", a.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.textBox1.Text = dr["PName"].ToString();
                this.textBox2.Text = dr["BasePrice"].ToString();
                // this.textBox11.Text = dr["VGroup"].ToString();



            }

            a.oleDbConnection1.Close();
        }
        private void DynamicButton_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Dynamic button is clicked");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            ++counter;

            Productid[i] = this.comboBox2.Text;
            PP[i] = Convert.ToInt32(this.textBox2.Text)*Convert.ToInt32(this.textBox9.Text);
            PQ[i] = this.textBox9.Text;

            
                this.textBox3.Text += Environment.NewLine+Productid[i] ;
             //   this.textBox5.Text += Environment.NewLine + [i];
                this.textBox6.Text += Environment.NewLine + PP[i];
                this.textBox8.Text += Environment.NewLine + PQ[i];


                ++i;



                int add=0;
            foreach(int z in PP)
            {
                add += z;
            }

            this.label.Text = "Total Ammount RS " + add.ToString() + "PKR";






                Button dynamicButton = new Button();


            /*
                // Set Button properties

                dynamicButton.Height = 40;

                dynamicButton.Width = 40;

                dynamicButton.BackColor = Color.White;

              //  dynamicButton.ForeColor = Color.Blue;

                dynamicButton.Location = new Point(320, 203);

                dynamicButton.Text = "Delete";

                dynamicButton.Name = "DynamicButton"+i;

                dynamicButton.Font = new Font("Georgia", 16);



                // Add a Button Click Event handler

                dynamicButton.Click += new EventHandler(DynamicButton_Click);



                // Add Button to the Form. Placement of the Button

                // will be based on the Location and Size of button

                Controls.Add(dynamicButton);  
             * 
         */
        }

        private void button1_Click(object sender, EventArgs e)
        {
















            a.oleDbConnection1.Open();

            OleDbCommand md = new OleDbCommand("insert into PO(POID,PODate,DDate,Status,VDept,VName,VID,VCPPH,TotalAmount,Approve,GoodRecieved) Values(@POID,@PODate,@DDate,'Open',@VDept,@VName,@VID,@VCPPH,@TotalAmount,'Not Approved','No');", a.oleDbConnection1);


            //@POID,@PODate,'Open',@VDept,@VName,@VID,@VCPPH,'100','Not Approved'
            //'1','2/2/2',acyt,'bekar depart',mzk,'321','0342342',''
            md.Parameters.AddWithValue("@POID", this.textBox11.Text);
            md.Parameters.AddWithValue("@PODate", System.DateTime.Now.ToString());
          md.Parameters.AddWithValue("@DDate", this.dateTimePicker1.Value.ToString());
            md.Parameters.AddWithValue("@VDept", this.comboBox3.Text);
            md.Parameters.AddWithValue("@VName", textBox4.Text);
            md.Parameters.AddWithValue("@VID", comboBox1.Text);
            //cmd.Parameters.AddWithValue("@VContactPerson", textBox8.Text);
            md.Parameters.AddWithValue("@VCPPH", textBox7.Text);
            int addd = 0;
            foreach (int z in PP)
            {
                addd += z;
            }
           
            this.label1.Text = addd.ToString();
            md.Parameters.AddWithValue("@TotalAmount", addd.ToString());
            //cmd.Parameters.AddWithValue("@SNO", sno);
            md.ExecuteNonQuery();

            for (int ii = 0; ii < counter; ++ii)
            {
                 md = new OleDbCommand("insert into POProducts Values(@POID,@Pid,@PQty,@PP);", a.oleDbConnection1);

                md.Parameters.AddWithValue("@POID", this.textBox11.Text);
                md.Parameters.AddWithValue("@pid", Productid[ii]);
                md.Parameters.AddWithValue("@PQty", PQ[ii]);
                md.Parameters.AddWithValue("@PP", PP[ii]);
                md.ExecuteNonQuery();

            }
           
        a.oleDbConnection1.Close();
        MessageBox.Show("Perchase Order Has been done");


        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            a.oleDbConnection1.Open();
            OleDbCommand cmd = new OleDbCommand("Select VID from Vendor where VGroup='"+this.comboBox3.Text+"'", a.oleDbConnection1);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["VID"]);
            }
            a.oleDbConnection1.Close();




              a.oleDbConnection1.Open();
              OleDbCommand ccmd = new OleDbCommand("Select Count(VDept) from PO where VDept='" + this.comboBox3.Text + "'", a.oleDbConnection1);
            OleDbDataReader ddr = ccmd.ExecuteReader();
           if(ddr.Read())
           {
               int id = Convert.ToInt32(ddr[0]) +1;

              
               this.textBox11.Text = this.comboBox3.Text + "_0" + id.ToString()+"_2018";
           }
           a.oleDbConnection1.Close();
            //DateTime aa = new DateTime();
          //  this.textBox10.Text = aa.Date.noe;
            

        }
    }
}
