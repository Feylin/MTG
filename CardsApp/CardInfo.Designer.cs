namespace CardsApp
{
    sealed partial class CardInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardInfo));
            this.pictureBoxCardImage = new System.Windows.Forms.PictureBox();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.lblCardNotes = new System.Windows.Forms.Label();
            this.pictureBoxSwitch = new System.Windows.Forms.PictureBox();
            this.txtRarity = new System.Windows.Forms.TextBox();
            this.txtSet = new System.Windows.Forms.TextBox();
            this.lblRarity = new System.Windows.Forms.Label();
            this.lblSet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSwitch)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCardImage
            // 
            this.pictureBoxCardImage.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxCardImage.Name = "pictureBoxCardImage";
            this.pictureBoxCardImage.Size = new System.Drawing.Size(480, 680);
            this.pictureBoxCardImage.TabIndex = 0;
            this.pictureBoxCardImage.TabStop = false;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(506, 37);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(335, 532);
            this.txtNotes.TabIndex = 1;
            this.txtNotes.Text = "";
            this.txtNotes.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.txtNotes_ContentsResized);
            this.txtNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNotes_KeyDown);
            this.txtNotes.MouseLeave += new System.EventHandler(this.txtNotes_MouseLeave);
            this.txtNotes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtNotes_MouseMove);
            // 
            // lblCardNotes
            // 
            this.lblCardNotes.AutoSize = true;
            this.lblCardNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNotes.Location = new System.Drawing.Point(506, 12);
            this.lblCardNotes.Name = "lblCardNotes";
            this.lblCardNotes.Size = new System.Drawing.Size(90, 15);
            this.lblCardNotes.TabIndex = 2;
            this.lblCardNotes.Text = "lblCardNotes";
            // 
            // pictureBoxSwitch
            // 
            this.pictureBoxSwitch.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSwitch.ImageLocation = "";
            this.pictureBoxSwitch.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxSwitch.Name = "pictureBoxSwitch";
            this.pictureBoxSwitch.Size = new System.Drawing.Size(480, 680);
            this.pictureBoxSwitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSwitch.TabIndex = 3;
            this.pictureBoxSwitch.TabStop = false;
            this.pictureBoxSwitch.Click += new System.EventHandler(this.pictureBoxSwitch_Click);
            // 
            // txtRarity
            // 
            this.txtRarity.Location = new System.Drawing.Point(654, 596);
            this.txtRarity.Name = "txtRarity";
            this.txtRarity.Size = new System.Drawing.Size(187, 20);
            this.txtRarity.TabIndex = 4;
            // 
            // txtSet
            // 
            this.txtSet.Location = new System.Drawing.Point(654, 635);
            this.txtSet.Name = "txtSet";
            this.txtSet.Size = new System.Drawing.Size(187, 20);
            this.txtSet.TabIndex = 5;
            // 
            // lblRarity
            // 
            this.lblRarity.AutoSize = true;
            this.lblRarity.Location = new System.Drawing.Point(541, 596);
            this.lblRarity.Name = "lblRarity";
            this.lblRarity.Size = new System.Drawing.Size(34, 13);
            this.lblRarity.TabIndex = 7;
            this.lblRarity.Text = "Rarity";
            // 
            // lblSet
            // 
            this.lblSet.AutoSize = true;
            this.lblSet.Location = new System.Drawing.Point(541, 638);
            this.lblSet.Name = "lblSet";
            this.lblSet.Size = new System.Drawing.Size(23, 13);
            this.lblSet.TabIndex = 8;
            this.lblSet.Text = "Set";
            // 
            // CardInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 702);
            this.Controls.Add(this.lblSet);
            this.Controls.Add(this.lblRarity);
            this.Controls.Add(this.txtSet);
            this.Controls.Add(this.txtRarity);
            this.Controls.Add(this.pictureBoxSwitch);
            this.Controls.Add(this.lblCardNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.pictureBoxCardImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CardInfo";
            this.Text = "Card Info";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardInfo_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSwitch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCardImage;
        private System.Windows.Forms.RichTextBox txtNotes;
        private System.Windows.Forms.Label lblCardNotes;
        private System.Windows.Forms.PictureBox pictureBoxSwitch;
        private System.Windows.Forms.TextBox txtRarity;
        private System.Windows.Forms.TextBox txtSet;
        private System.Windows.Forms.Label lblRarity;
        private System.Windows.Forms.Label lblSet;
    }
}