namespace CardsApp
{
    sealed partial class VisualHand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualHand));
            this.pictureCardOne = new System.Windows.Forms.PictureBox();
            this.pictureCardFour = new System.Windows.Forms.PictureBox();
            this.pictureCardFive = new System.Windows.Forms.PictureBox();
            this.pictureCardSix = new System.Windows.Forms.PictureBox();
            this.pictureCardSeven = new System.Windows.Forms.PictureBox();
            this.pictureCardThree = new System.Windows.Forms.PictureBox();
            this.pictureCardTwo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardFour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardFive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardSix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardSeven)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardTwo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureCardOne
            // 
            this.pictureCardOne.Location = new System.Drawing.Point(1398, 12);
            this.pictureCardOne.Name = "pictureCardOne";
            this.pictureCardOne.Size = new System.Drawing.Size(225, 319);
            this.pictureCardOne.TabIndex = 0;
            this.pictureCardOne.TabStop = false;
            // 
            // pictureCardFour
            // 
            this.pictureCardFour.Location = new System.Drawing.Point(1167, 12);
            this.pictureCardFour.Name = "pictureCardFour";
            this.pictureCardFour.Size = new System.Drawing.Size(225, 319);
            this.pictureCardFour.TabIndex = 1;
            this.pictureCardFour.TabStop = false;
            // 
            // pictureCardFive
            // 
            this.pictureCardFive.Location = new System.Drawing.Point(936, 12);
            this.pictureCardFive.Name = "pictureCardFive";
            this.pictureCardFive.Size = new System.Drawing.Size(225, 319);
            this.pictureCardFive.TabIndex = 2;
            this.pictureCardFive.TabStop = false;
            // 
            // pictureCardSix
            // 
            this.pictureCardSix.Location = new System.Drawing.Point(705, 12);
            this.pictureCardSix.Name = "pictureCardSix";
            this.pictureCardSix.Size = new System.Drawing.Size(225, 319);
            this.pictureCardSix.TabIndex = 3;
            this.pictureCardSix.TabStop = false;
            // 
            // pictureCardSeven
            // 
            this.pictureCardSeven.Location = new System.Drawing.Point(474, 12);
            this.pictureCardSeven.Name = "pictureCardSeven";
            this.pictureCardSeven.Size = new System.Drawing.Size(225, 319);
            this.pictureCardSeven.TabIndex = 4;
            this.pictureCardSeven.TabStop = false;
            // 
            // pictureCardThree
            // 
            this.pictureCardThree.Location = new System.Drawing.Point(243, 12);
            this.pictureCardThree.Name = "pictureCardThree";
            this.pictureCardThree.Size = new System.Drawing.Size(225, 319);
            this.pictureCardThree.TabIndex = 5;
            this.pictureCardThree.TabStop = false;
            // 
            // pictureCardTwo
            // 
            this.pictureCardTwo.Location = new System.Drawing.Point(12, 12);
            this.pictureCardTwo.Name = "pictureCardTwo";
            this.pictureCardTwo.Size = new System.Drawing.Size(225, 319);
            this.pictureCardTwo.TabIndex = 6;
            this.pictureCardTwo.TabStop = false;
            // 
            // VisualHand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1636, 343);
            this.Controls.Add(this.pictureCardTwo);
            this.Controls.Add(this.pictureCardThree);
            this.Controls.Add(this.pictureCardSeven);
            this.Controls.Add(this.pictureCardSix);
            this.Controls.Add(this.pictureCardFive);
            this.Controls.Add(this.pictureCardFour);
            this.Controls.Add(this.pictureCardOne);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisualHand";
            this.Text = "Visual Spoiler";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisualHand_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardFour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardFive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardSix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardSeven)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCardTwo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureCardOne;
        private System.Windows.Forms.PictureBox pictureCardFour;
        private System.Windows.Forms.PictureBox pictureCardFive;
        private System.Windows.Forms.PictureBox pictureCardSix;
        private System.Windows.Forms.PictureBox pictureCardSeven;
        private System.Windows.Forms.PictureBox pictureCardThree;
        private System.Windows.Forms.PictureBox pictureCardTwo;
    }
}