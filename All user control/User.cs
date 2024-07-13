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
    public partial class User : UserControl
    {
        function fn = new function();
        String query;
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            getMaxID();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtMobile.Text != "" && txtGender.Text != "" && txtEmail.Text != "" && txtUserName.Text != "" && txtPassword.Text != "")
            {
                String name = txtName.Text;
                String mobile = txtMobile.Text;
                String gender = txtGender.Text;
                String email = txtEmail.Text;
                String username = txtUserName.Text;
                String password = txtPassword.Text;
                txtPassword.PasswordChar = '*';

                query = "insert into users (UName,Mobile,Gender,EmailId,UserName,Pass) values ('"+name+ "','"+mobile+"','"+gender+"', '"+email+"', '"+username+"', '"+password+"')";
                fn.setData(query, "Employee Registered.");
                clearAll();
                getMaxID();
            }
            else
            {
                MessageBox.Show("Fill All Fields.", "Warning...!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabUsers_SelectedIndexChanged(object sender, EventArgs e)
        {    
            //show users details
            if (tabUsers.SelectedIndex == 1)
            {
                query = "select * from users";
                DataSet ds = fn.getData(query);
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            //show users details for delete
            else if (tabUsers.SelectedIndex == 2)
            {
                query = "select * from users";
                DataSet ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            txtid.Clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if(txtid.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation...!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from users where Id =" + txtid.Text + "";
                    fn.setData(query, "Record Delete.");
                    tabUsers_SelectedIndexChanged(this, null);
                    txtid.Clear();
                }
            }
        }

        private void User_Leave(object sender, EventArgs e)
        {
            clearAll();
        }



        //==========================REQUIRED METHOD==========================
        private void getMaxID()
        {
            query = "select max(Id) from users";
            DataSet ds = fn.getData(query);

            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                Int64 num = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                labelToSet.Text = (num + 1).ToString();
            }
        }
        public void clearAll()
        {
            txtName.Clear();
            txtMobile.Clear();
            txtGender.SelectedIndex = -1;
            txtEmail.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            txtid.Clear();
        }

        
    }
    
    
}
