using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJECT
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            POCreation a = new POCreation();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 obj = new Form6();
            obj.Show();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GRN obj = new GRN();
            obj.Show();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer("add");
            obj.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer("search");
            obj.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer("update");
            obj.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer("approve");
            obj.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Vendor v = new Vendor("add");
            v.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Vendor obj = new Vendor("approve");
            obj.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Vendor obj = new Vendor("search");
            obj.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
             Vendor obj = new Vendor("update");
            obj.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            POCreation obj = new POCreation();
            obj.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            POApproval obj = new POApproval();
            obj.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GRN obj = new GRN();
            obj.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Invoice obj = new Invoice();
            obj.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form7 obj = new Form7();
            obj.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form5 obj = new Form5();
            obj.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form8 obj = new Form8();
            obj.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SOCreation obj = new SOCreation();
            obj.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            System.Drawing.Rectangle rect = Screen.GetWorkingArea(this);
            this.MaximizedBounds = Screen.GetWorkingArea(this);
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
