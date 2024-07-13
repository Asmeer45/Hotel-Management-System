using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Hotel_Management.All_user_control
{
    public partial class Report : UserControl
    {
        
        function fn = new function();
        String query;
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            
            /*//get datas from database and show the datas
            query = "Select customer.Cid, customer.CName, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.Price, customer.StayDays, customer.Amount, customer.Payment from customer inner join rooms on customer.RoomId = rooms.RoomId";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];*/
        }

            
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            DataGridView dataGridView = guna2DataGridView1; 

            // Set the desired font for the printed content
            Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);

            // Set the starting position for printing
            int xPos = e.MarginBounds.Left;
            int yPos = e.MarginBounds.Top;

            // Print the column headers
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                e.Graphics.DrawString(column.HeaderText, font, Brushes.Black, xPos, yPos);
                xPos += column.Width;
            }

            yPos += dataGridView.ColumnHeadersHeight;

            // Print the rows
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                xPos = e.MarginBounds.Left;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible)
                    {
                        string cellValue = cell.Value != null ? cell.Value.ToString() : string.Empty;
                        e.Graphics.DrawString(cellValue, font, Brushes.Black, xPos, yPos);
                        xPos += cell.Size.Width;
                    }
                }
                yPos += font.Height;

                // Check if more pages are needed to print the remaining rows
                if (yPos + font.Height > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
        }





        private void btnprint_Click(object sender, EventArgs e)
        {
                //DataGridView yourDataGridView = new DataGridView();

                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintDocument_PrintPage;

                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            

        }

        private void txtreport_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtreport.SelectedIndex == 0)
            {
                query = "Select * from rooms";
                DataSet ds = fn.getData(query);
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            else if (txtreport.SelectedIndex == 1)
            {
                query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, customer.Checkout, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price, customer.StayDays, customer.Amount from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is not null";
                DataSet ds = fn.getData(query);
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            else if (txtreport.SelectedIndex == 2)
            {
                query = "Select customer.Cid, customer.CName, customer.Mobile, customer.Nationality, customer.Gender, customer.DOB, customer.IdProof, customer.Addres, customer.Checkin, rooms.RoomNo, rooms.RoomType, rooms.Bed, rooms.Price from customer inner join rooms on customer.RoomId = rooms.RoomId where Checkout is null";
                DataSet ds = fn.getData(query);
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
