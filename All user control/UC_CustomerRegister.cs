using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using MySqlX.XDevAPI.Relational;
//using static Google.Protobuf.Collections.MapField<TKey, TValue>;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Guna.UI2.WinForms;

namespace Hotel_Management.All_user_control
{
    public partial class UC_CustomerRegister : UserControl
    {

        function fn = new function();
        String query;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD1HT8P;Initial Catalog=MyHotel;Integrated Security=True");
        public UC_CustomerRegister()
        {
            InitializeComponent();
        }

        //set the combo box
        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }


        private void txtRoomtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select bed and room type and after show the available rooms
            txtRoomno.Items.Clear();
            query = "select RoomNo from rooms where Bed = '" + txtBed.Text + "' and RoomType = '" + txtRoomtype.Text + "' and Booked = 'No' ";
            setComboBox(query, txtRoomno);
        }


        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear previous data
            txtRoomtype.SelectedIndex = -1;
            txtRoomno.Items.Clear();
            txtPrice.Clear();
        }

        int rid;
        private void txtRoomno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select the roomNo and after show that room price
            query = "select Price,RoomId from rooms where RoomNo = '" + txtRoomno.Text + "' ";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
        }


        private void btnAlloteRoom_Click(object sender, EventArgs e)
        {
            
            //check all text box selected or not
            if (txtName.Text != "" && txtMobile.Text != "" && txtNationilty.Text != "" && txtGender.Text != "" && txtDOB.Text != "" && txtID.Text != "" && txtAddress.Text != "" && txtCheckin.Text != "" && txtPrice.Text != "")
            {
                String name = txtName.Text;
                String mobile = txtMobile.Text;
                String national = txtNationilty.Text;
                String gender = txtGender.Text;
                String dob = txtDOB.Text;
                String idproof = txtID.Text;
                String address = txtAddress.Text;
                String checkin = txtCheckin.Text;

                // Validate mobile number (digits only)
                if (!IsNumberValid(mobile))
                {
                    MessageBox.Show("Invalid mobile number. Please enter digits only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                con.Open();
                SqlCommand cmd = new SqlCommand("insert into customer (CName,Mobile,Nationality,Gender,DOB,IdProof,Addres,Checkin,RoomId) values ('" + name + "','" + mobile + "','" + national + "','" + gender + "','" + dob + "','" + idproof + "','" + address +"','" + checkin + "', " + rid + " ) update rooms set Booked = 'Yes' where RoomNo = '" + txtRoomno.Text + "' ",con);
                cmd.ExecuteNonQuery();

                
                con.Close();
                MessageBox.Show("Allocation Successful.");
                clearAll();
               
            }
            else
            {
                MessageBox.Show("Fill in all the details.", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private bool IsNumberValid(string number)
        {
            // Regular expression pattern to match digits only
            string pattern = @"^\d+$";

            // Check if the number matches the pattern
            return Regex.IsMatch(number, pattern);
        }


        private void clearAll()
        {
            txtName.Clear();
            txtMobile.Clear();
            txtNationilty.Clear();
            txtGender.SelectedIndex = -1;
            txtDOB.ResetText();
            txtID.Clear();
            txtAddress.Clear();
            txtCheckin.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoomno.SelectedIndex = -1;
            txtRoomtype.SelectedIndex = -1;
            txtPrice.Clear();

        }

        private void UC_CustomerRegister_Load(object sender, EventArgs e)
        {
            
        }

        private void UC_CustomerRegister_Load_1(object sender, EventArgs e)
        {
            
        }

        private void UC_CustomerRegister_Leave(object sender, EventArgs e)
        {
            clearAll();
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {

            /*String name = txtName.Text;
            Int64 mobile = Int64.Parse(txtMobile.Text);
            String national = txtNationilty.Text;
            String gender = txtGender.Text;
            String dob = txtDOB.Text;
            String idproof = txtID.Text;
            String address = txtAddress.Text;
            String checkin = txtCheckin.Text;
            Int64 id = Int64.Parse(txtCid.Text);

            query = "update customer set (Mobile,Nationality,Gender,DOB,IdProof,Addres,Checkin) values (" + mobile + ", '" + national + "', '" + gender + "', '" + dob + "','" + idproof + "','" + address + "','" + checkin + "') where Cid = " + id +" ";
            fn.getData(query);
            clearAll();*/













        }
    }
}
