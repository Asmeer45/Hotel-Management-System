using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management.All_user_control
{

    public partial class Payment : UserControl
    {

        public static Payment insender;
        public DataGridView DataGridView11;

        private int invoice = 100;
        function fn = new function();
        String query;

        public Payment()
        {
            InitializeComponent();
            insender = this;
            DataGridView11 = guna2DataGridView1;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price, customer.Payment from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is not null";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            //search the customer
            query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin,customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is not null and CName like '" + txtSearchName.Text + "%' ";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        int id;
        private int customerId;

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtName.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                txtRoomNo.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());
                txtCheckin.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
                txtCheckout.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                txtPrice.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString());


            }
        }



        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            DateTime checkInDate = txtCheckin.Value;
            DateTime checkOutDate = txtCheckout.Value;
            decimal roomPrice = decimal.Parse(txtPrice.Text);

            //calculate the numbers of data
            int days = (int)(checkOutDate - checkInDate).TotalDays;
            lblDays.Text = days.ToString();

            //calculate the total amound
            decimal totalAmount = roomPrice * days;
            lblTotal.Text = totalAmount.ToString();

            using (SqlConnection connection = new SqlConnection("data source= DESKTOP-FD1HT8P; database= MyHotel; integrated security = True"))
            {

                String customerName = txtName.Text;
                connection.Open();

                    string sql = "UPDATE customer SET StayDays = @days, Amount = @totalAmount WHERE CName = @customerName";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@days", days);
                    command.Parameters.AddWithValue("@totalAmount", totalAmount);
                    command.Parameters.AddWithValue("@customerName", customerName);
                    command.ExecuteNonQuery();



            }
            }  

        private void clearAll()
        {
            lblDays.Text = "";
            lblTotal.Text = "";
            txtName.Clear();
            txtRoomNo.Clear();
            txtCheckin.ResetText();
            txtCheckout.ResetText();
            txtPrice.Clear();
        }

        private void Payment_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        private void btnBill_Click(object sender, EventArgs e)
        {

            invoice++;
            MprintPreviewDialog1.Document = MprintDocument1;
            MprintPreviewDialog1.ShowDialog();
            UpdatePayment();
            
            
        }

        private void MprintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Retrieve the values from the receipt
            string hotelName = "LEMONDEN HOTEL";
            string customerName = txtName.Text;
            decimal roomPrice = decimal.Parse(txtPrice.Text);
            int days = int.Parse(lblDays.Text);
            decimal totalAmount = decimal.Parse(lblTotal.Text);



            // Print the receipt content

            Bitmap bitmap = Properties.Resources.hotel_invoice;
            Image image = new Bitmap(bitmap);
            e.Graphics.DrawImage(image, 25, 25, 800, 1000);
            e.Graphics.DrawString(customerName, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(116, 505));
            e.Graphics.DrawString(days.ToString(), new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(359, 505));
            e.Graphics.DrawString("Rs. " + roomPrice.ToString(), new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(495, 505));
            e.Graphics.DrawString("Rs. " + totalAmount.ToString(), new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(678, 505));
            e.Graphics.DrawString("Rs. " + totalAmount.ToString(), new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(665, 724));
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(580, 350));
            e.Graphics.DrawString("Invoice No#" + invoice.ToString(), new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(580, 370));
        }


        private void UpdatePayment()
        {
            string customerName = txtName.Text;

            using (SqlConnection connection = new SqlConnection("data source= DESKTOP-FD1HT8P; database= MyHotel; integrated security = True"))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE customer SET Payment  = 'Success' WHERE CName = '" + customerName + "'", connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Stay details updated successfully.");
                    clearAll();


                    SqlCommand cmd = new SqlCommand("Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price, customer.Payment from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is not null", connection);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    guna2DataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating stay details: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            Refresh();
        }






    }
}