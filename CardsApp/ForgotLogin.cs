using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;

namespace CardsApp
{
    public partial class ForgotLogin : Form
    {
        private readonly Repository _repo = new Repository();
        private const string Title = "Forgot Login";
        private const string Error = "Error";

        public ForgotLogin()
        {
            InitializeComponent();

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            AcceptButton = btnRequest;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string secret = txtSecret.Text.Trim();
            List<TextBox> textBoxs = new List<TextBox> { txtUsername, txtSecret };

            var user = _repo.GetUserByUsername(username);

            if (user != null)
            {
                if (user.Secret == secret)
                {
                    string tempPass = _repo.ForgotLogin(username, secret);

                    foreach (var textBox in textBoxs)
                        textBox.Text = string.Empty;

                    MessageBox.Show(@"Temporary Password: " + tempPass, Title);
                }
                else
                    MessageBox.Show(@"Secret does not match", Title + @" " + Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(username + @" does not exist", Title + @" " + Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
