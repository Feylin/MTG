using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessEntities;
using BusinessLogic;

namespace CardsApp
{
    public sealed partial class VisualHand : Form
    {
        public VisualHand(IReadOnlyList<BE_Card> card)
        {
            InitializeComponent();

            BackColor = Color.FromArgb(212, 208, 200);

            InitialSetup(card);
        }

        private void InitialSetup(IReadOnlyList<BE_Card> card)
        {
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Text = @"Visual spoiler";

            List<PictureBox> myPicBoxArray = Controls.Cast<PictureBox>().ToList();
            
            for (int i = 0; i < card.Count; i++)
            {
                myPicBoxArray[i].ImageLocation = card[i].Image;
                myPicBoxArray[i].SizeMode = PictureBoxSizeMode.Zoom;
            }
            

            Width = (pictureCardOne.Width * card.Count) + (((card.Count - 1) * 6) + 41);
        }

        private void VisualHand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
