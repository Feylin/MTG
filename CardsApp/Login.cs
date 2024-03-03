using System;
using System.Windows.Forms;
using BusinessLogic;

namespace CardsApp
{
    public partial class Login : Form
    {
        private readonly Repository _repo = new Repository();
        private const string Title = @"Login";

        public Login()
        {
            InitializeComponent();

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            CreateUser outputForm = new CreateUser
            {
                StartPosition = FormStartPosition.CenterParent
            };

            outputForm.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (_repo.CheckLogin(username, password))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show(@"Login failed", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnForgotLogin_Click(object sender, EventArgs e)
        {
            ForgotLogin outputForm = new ForgotLogin
            {
                StartPosition = FormStartPosition.CenterParent
            };

            outputForm.ShowDialog();
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                //if (e.KeyCode == Keys.Enter)
                //    btnLogin_Click(sender, e);
                AcceptButton = btnLogin;
            }
        }
    }
}
