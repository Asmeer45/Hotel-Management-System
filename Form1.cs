using System.Data;

namespace Hotel_Management
{
    public partial class form1 : Form
    {
        function fn = new function();
        String query;
        public form1()
        {
            InitializeComponent();
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            query = "select UserName, Pass from users where UserName = '" + txtusername.Text + "' and Pass = '" + txtpassword.Text + "' ";
            DataSet ds = fn.getData(query);

            if (ds.Tables[0].Rows.Count != 0)
            {
                lblerror.Visible = false;
                Dashboard dash = new Dashboard();
                this.Hide();
                dash.Show();
            }
            else
            {
                lblerror.Visible = true;
                txtpassword.Clear();
            }
        }
    }
}