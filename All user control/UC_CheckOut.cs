using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management.All_user_control
{
    public partial class UC_CheckOut : UserControl
    {
        public static UC_CheckOut instance;
        public DataGridView guna2DataGridView11;

        function fn = new function();
        String query;

        public UC_CheckOut()
        {
            InitializeComponent();
            instance = this;
            guna2DataGridView11 = guna2DataGridView1;
        }

        private void UC_CheckOut_Load(object sender, EventArgs e)
        {
            //get datas from database and show the datas
            query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Chekout = 'NO'";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            //search the customer
            query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where CName like '"+txtName.Text+"%' and Chekout = 'NO'";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }


        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // when click the customer row and after show that customer name and room no
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCname.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                txtRoomNo.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());

            }
            guna2DataGridView1.Refresh();
        }


        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (txtCname.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confiramation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtCheckOutDate.Text;
                    query = "update customer set Chekout = 'YES', Checkout = '" + cdate + "' where Cid = " + id + " update rooms set Booked = 'NO' where RoomNo = '" + txtRoomNo.Text + "'";
                    fn.setData(query, "Check Out Successfully.");
                    UC_CheckOut_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearAll()
        {
            txtName.Clear();
            txtCname.Clear();
            txtRoomNo.Clear();
            txtCheckOutDate.ResetText();
        }

        private void UC_CheckOut_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
