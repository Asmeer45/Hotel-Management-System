using Guna.UI2.WinForms;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Hotel_Management.All_user_control
{
    public partial class UC_AddRoom : UserControl
    {
        private int selectedRoomId;

        public static UC_AddRoom insender;
        public DataGridView DataGridView11;
        function fn = new function();
        String query;
        public UC_AddRoom()
        {
            InitializeComponent();
            insender = this;
            DataGridView11 = DataGridView1;

        }



        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
            DataGridView1.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtRoomNo.Text != "" && txtType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                String roomno = txtRoomNo.Text;
                String type = txtType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "insert into rooms (RoomNo,RoomType,Bed,Price) values ('" + roomno + "', '" + type + "', '" + bed + "'," + price + ")";
                fn.setData(query, "Room Added");
                clearAll();

                UC_AddRoom_Load(this, null);

            }
            else
            {
                MessageBox.Show("Fill All Fields", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void clearAll()
        {
            txtRoomNo.Clear();
            txtType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();

        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (txtRoomNo.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation...!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from rooms where RoomNo ='" + txtRoomNo.Text + "'";
                    fn.setData(query, "Record Delete.");
                    UC_AddRoom_Load(this, null);
                    txtRoomNo.Clear();








                }

          

            }

        }

        

    }
}

