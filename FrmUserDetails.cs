using System;
using System.Windows.Forms;

namespace ArmourySystem
{
    public partial class FrmUserDetails : Form
    {
        public string Username => txtUsername.Text;
        public string Password => mtxtPassword.Text;
        public bool IsAdmin => chkIsAdmin.Checked;

        public FrmUserDetails(string strType)
        {
            InitializeComponent();
            if (strType == "LogIn")
            {
                this.Text = "Log In"; // Set title for login
                this.chkIsAdmin.Visible = false; // Hide admin checkbox
                this.lblConfirmPassword.Visible = false; // Hide confirm password label
                this.mTxtConfirmPassword.Visible = false; // Hide confirm password field
                this.btnLogin.Text = "Login"; // Set button text for login
            }
            else if (strType == "AddUser")
            {
                this.Text = "Add User"; // Change title for adding user
                this.chkIsAdmin.Visible = true; // Show admin checkbox
                this.lblConfirmPassword.Visible = true; // Show confirm password label
                this.mTxtConfirmPassword.Visible = true; // Show confirm password field
                this.btnLogin.Text = "Add User"; // Set button text for user addition
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // TODO: Remove development defaults
            txtUsername.Text = "admin";
            mtxtPassword.Text = "admin";

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(mtxtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (mTxtConfirmPassword.Visible && mtxtPassword.Text != mTxtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtPassword.Text = "";
                mTxtConfirmPassword.Text = "";
                KeyEventArgs kEvtArgs = new KeyEventArgs(Keys.Enter);
                TxtUsername_KeyDown(sender, kEvtArgs);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                this.BeginInvoke((MethodInvoker)(() => this.mtxtPassword.Focus()));
            }
        }

        private void MtxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //prevent beep

                if (mTxtConfirmPassword.Visible)
                {
                    this.BeginInvoke((MethodInvoker)(() => this.mTxtConfirmPassword.Focus()));
                }
                else
                {
                    this.BeginInvoke((MethodInvoker)(() => this.BtnLogin_Click(sender, e)));
                }
            }
        }

        private void MTxtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                this.BeginInvoke((MethodInvoker)(() => this.BtnLogin_Click(sender, e)));
            }

        }
    }
}
