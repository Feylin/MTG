using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessLogic;

namespace CardsApp
{
    public partial class CreateUser : Form
    {
        private readonly Repository _repo = new Repository();
        private const string Title = "Create User";
        private const string Error = "Error";
        private int _result;
        //static readonly Random Rnd = new Random();

        public CreateUser()
        {
            InitializeComponent();

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            MathCaptcha();

            AcceptButton = btnCreateUser;
        }

        private void MathCaptcha()
        {
            //double numberOne = Rnd.Next(9, 17);
            //double numberTwo = Rnd.Next(5, 13);
            //int mathOperator = Rnd.Next(1, 5);
            int numberOne = _repo.Random(9, 17);
            int numberTwo = _repo.Random(5, 13);
            int mathOperator = _repo.Random(1, 4);
            string operation = null;

            switch (mathOperator)
            {
                case 1:
                    _result = numberOne + numberTwo;
                    operation = "+";
                    break;
                case 2:
                    _result = numberOne - numberTwo;
                    operation = "-";
                    break;
                case 3:
                    operation = "*";
                    _result = numberOne * numberTwo;
                    break;
            }

            lblHumanTest.Text = string.Format("{0} {1} {2} =", numberOne, operation, numberTwo);
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string verifyPassword = txtVerifyPass.Text;
            string secret = txtSecret.Text.Trim();
            string humanTest = txtHumanTest.Text.Trim();
            List<TextBox> textBoxs = new List<TextBox> {txtPassword, txtHumanTest, txtSecret, txtUsername, txtVerifyPass};

            if (!_repo.UserExist(username))
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && !password.Contains(" ") && !string.IsNullOrWhiteSpace(secret) && !string.IsNullOrWhiteSpace(humanTest))
                {
                    if (txtPassword.Text.Equals(verifyPassword))
                    {
                        if (password.Length >= 6)
                        {
                            if (int.Parse(humanTest) == _result)
                            {
                                if (_repo.AddUser(username, password, secret))
                                {
                                    foreach (var textbox in textBoxs)
                                        textbox.Text = string.Empty;

                                    MessageBox.Show(@"User added successfully", Title);
                                }
                                else
                                    MessageBox.Show(@"User not added", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show(@"Human test failed", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show(@"Password must be 6 characters or longer", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show(@"Password do not match", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(@"Whitespaces not allowed", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(@"User already exists", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
