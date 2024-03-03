namespace CardsApp
{
    partial class CreateUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateUser));
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblVerifyPass = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtVerifyPass = new System.Windows.Forms.TextBox();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.lblHumanTest = new System.Windows.Forms.Label();
            this.txtHumanTest = new System.Windows.Forms.TextBox();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.lblSecret = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(45, 64);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(45, 90);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // lblVerifyPass
            // 
            this.lblVerifyPass.AutoSize = true;
            this.lblVerifyPass.Location = new System.Drawing.Point(45, 116);
            this.lblVerifyPass.Name = "lblVerifyPass";
            this.lblVerifyPass.Size = new System.Drawing.Size(62, 13);
            this.lblVerifyPass.TabIndex = 2;
            this.lblVerifyPass.Text = "Verify Pass.";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(139, 61);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(139, 87);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtVerifyPass
            // 
            this.txtVerifyPass.Location = new System.Drawing.Point(139, 113);
            this.txtVerifyPass.Name = "txtVerifyPass";
            this.txtVerifyPass.Size = new System.Drawing.Size(100, 20);
            this.txtVerifyPass.TabIndex = 3;
            this.txtVerifyPass.UseSystemPasswordChar = true;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(48, 215);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(75, 23);
            this.btnCreateUser.TabIndex = 6;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // lblHumanTest
            // 
            this.lblHumanTest.AutoSize = true;
            this.lblHumanTest.Location = new System.Drawing.Point(45, 190);
            this.lblHumanTest.Name = "lblHumanTest";
            this.lblHumanTest.Size = new System.Drawing.Size(72, 13);
            this.lblHumanTest.TabIndex = 7;
            this.lblHumanTest.Text = "lblHumanTest";
            // 
            // txtHumanTest
            // 
            this.txtHumanTest.Location = new System.Drawing.Point(139, 187);
            this.txtHumanTest.Name = "txtHumanTest";
            this.txtHumanTest.Size = new System.Drawing.Size(100, 20);
            this.txtHumanTest.TabIndex = 5;
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(139, 139);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(100, 20);
            this.txtSecret.TabIndex = 4;
            // 
            // lblSecret
            // 
            this.lblSecret.AutoSize = true;
            this.lblSecret.Location = new System.Drawing.Point(45, 142);
            this.lblSecret.Name = "lblSecret";
            this.lblSecret.Size = new System.Drawing.Size(38, 13);
            this.lblSecret.TabIndex = 10;
            this.lblSecret.Text = "Secret";
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(45, 168);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(127, 13);
            this.lblQuestion.TabIndex = 11;
            this.lblQuestion.Text = "Prove you are human";
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 373);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblSecret);
            this.Controls.Add(this.txtSecret);
            this.Controls.Add(this.txtHumanTest);
            this.Controls.Add(this.lblHumanTest);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.txtVerifyPass);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblVerifyPass);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateUser";
            this.Text = "Create User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblVerifyPass;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtVerifyPass;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Label lblHumanTest;
        private System.Windows.Forms.TextBox txtHumanTest;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.Label lblSecret;
        private System.Windows.Forms.Label lblQuestion;
    }
}