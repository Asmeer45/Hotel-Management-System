using Hotel_Management.All_user_control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            user1.Visible = false;
            uC_AddRoom1.Visible = false;
            uC_CustomerRegister1.Visible = false;
            btnaddroom.PerformClick();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btncr_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = btncr.Left + 18;
            uC_CustomerRegister1.Visible = true;
            uC_CustomerRegister1.BringToFront();
            
        }

        private void btncheckout_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = btncheckout.Left + 18;
            uC_CheckOut1.Visible = true;
            uC_CheckOut1.BringToFront();

            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Chekout = 'NO'", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            UC_CheckOut.instance.guna2DataGridView11.DataSource = dt;
            con.Close();

        }

        private void btncd_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = btncd.Left + 18;
            customerDetails1.Visible = true;
            customerDetails1.BringToFront();
            
        }

        private void btnemployee_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = btnemployee.Left + 18;
            user1.Visible= true;
            user1.BringToFront();
            
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD1HT8P;Initial Catalog=MyHotel;Integrated Security=True");

        private void btnaddroom_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = btnaddroom.Left + 18;
            uC_AddRoom1.Visible = true;
            uC_AddRoom1.BringToFront();

            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from rooms", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            UC_AddRoom.insender.DataGridView11.DataSource = dt;
            con.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnpayment_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = btnpayment.Left + 18;
            payment1.Visible = true;
            payment1.BringToFront();

            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price,customer.Payment from customer inner join rooms on customer.RoomId = rooms.RoomId where Chekout = 'Yes'", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            Payment.insender.DataGridView11.DataSource = dt;
            con.Close();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Close();
            form1 LoginForm = new form1();
            LoginForm.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MovingPanel.Left = guna2Button1.Left + 18;
            report1.Visible = true;
            report1.BringToFront();
        }
    }
}
