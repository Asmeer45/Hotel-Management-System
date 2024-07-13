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

namespace Hotel_Management.All_user_control
{
    public partial class CustomerDetails : UserControl
    {
        public static CustomerDetails insender;
        public DataGridView DataGridView11;

        function fn = new function();
        String query;
        public CustomerDetails()
        
        {
            InitializeComponent();
            insender = this;
            DataGridView11 = guna2DataGridView1;
        }

        private void txtsearchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtsearchby.SelectedIndex == 0)
            {
                query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId ";
                DataSet ds = fn.getData(query);
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            else if (txtsearchby.SelectedIndex == 1)
            {              
                 query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is null";
                 DataSet ds = fn.getData(query);
                 guna2DataGridView1.DataSource=ds.Tables[0];               
            }
            else if (txtsearchby.SelectedIndex == 2)
            {
                query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is not null";
                DataSet ds = fn.getData(query);
                guna2DataGridView1.DataSource = ds.Tables[0];
            }

        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            if (txtCustomerId.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation...!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from customer where Cid =" + txtCustomerId.Text + "";
                    fn.setData(query, "Record Delete.");                   
                    txtCustomerId.Clear();

                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD1HT8P;Initial Catalog=MyHotel;Integrated Security=True");

                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is not null ", con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    CustomerDetails.insender.DataGridView11.DataSource = dt;
                    con.Close();

                }
            }
            
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("data source= DESKTOP-FD1HT8P; database= MyHotel; integrated security = True"))

            for (int item = 0; item< guna2DataGridView1.Rows.Count-1; item++)
            {
                    SqlCommand cmd = new SqlCommand("update customer set CName = @CName, Mobile = @Mobile, Nationality = @Nationality, Gender = @Gender, DOB = @DOB, IdProof = @IdProof, Addres = @Addres, Checkin = @Checkin where Cid =@Cid",connection);
                    cmd.Parameters.AddWithValue("@CName", guna2DataGridView1.Rows[item].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@Mobile", guna2DataGridView1.Rows[item].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@Nationality", guna2DataGridView1.Rows[item].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@Gender", guna2DataGridView1.Rows[item].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@DOB", guna2DataGridView1.Rows[item].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@IdProof", guna2DataGridView1.Rows[item].Cells[6].Value);
                    cmd.Parameters.AddWithValue("@Addres", guna2DataGridView1.Rows[item].Cells[7].Value);
                    cmd.Parameters.AddWithValue("@Checkin", guna2DataGridView1.Rows[item].Cells[8].Value);
                    cmd.Parameters.AddWithValue("Cid", guna2DataGridView1.Rows[item].Cells[0].Value);


                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

            }
            MessageBox.Show("Recode Update Successfuly");
        }
    }
}
