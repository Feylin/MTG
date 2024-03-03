using System.IO;
using System.Net;
using System.Security.Policy;
using BusinessEntities;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CardsApp
{
    public sealed partial class CardInfo : Form
    {
        private readonly Dictionary<int, BE_Card> _dictionary;
        private readonly BE_Card _card;
        private readonly Repository _repo = new Repository();
        private bool _showSecond = false;
        private string[] _images;

        readonly ToolTip _tt = new ToolTip();
        int _k;
        int _textHeight;
        const string Punctuations = " ,.:;!?'\")]}\n";

        public CardInfo(BE_Card beCard, Dictionary<int, BE_Card> newDictionary)
        {
            InitializeComponent();

            Color white = Color.FromArgb(212, 208, 200);

            BackColor = white;
            txtNotes.BackColor = white;

            _card = beCard;
            _dictionary = newDictionary;

            InitialSetup(beCard);
        }

        private void InitialSetup(BE_Card card)
        {
            _images = _card.Name.Split('/');

            switch (card.Color)
            {
                case "white":
                    lblCardNotes.ForeColor = Color.FromArgb(100, 100, 100);
                    break;
                case "colorless":
                    lblCardNotes.ForeColor = Color.FromArgb(167, 0, 167);
                    break;
                case "multi":
                    lblCardNotes.ForeColor = Color.FromArgb(255, 95, 17);
                    break;
                default:
                    lblCardNotes.ForeColor = Color.FromName(card.Color);
                    break;
            }

            if (_images.Count() == 2)
                lblCardNotes.Text = _images[0] + @" - Card Rules and Notes";
            else
            {
                lblCardNotes.Text = card.Name + @" - Card Rules and Notes";
                pictureBoxSwitch.Enabled = false;
                pictureBoxSwitch.Visible = false;
            }

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            txtNotes.BorderStyle = BorderStyle.None;
            txtNotes.SelectionBackColor = Color.Transparent;
            txtNotes.Cursor = DefaultCursor;

            Text = string.Format("{0} info", card.Name);
            pictureBoxCardImage.ImageLocation = card.Image;
            pictureBoxSwitch.Parent = pictureBoxCardImage;
            pictureBoxSwitch.ImageLocation = @"refresh_3.png";

            txtNotes.Text = card.Notes;
            txtRarity.Text = card.Rarity;
            txtSet.Text = card.Set;

            //if (string.IsNullOrWhiteSpace(card.Notes))
            //    Width = 520;
            
            _repo.HighlightWords(txtNotes);

            txtNotes.DeselectAll();
        }

        private void CardInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_repo.UpdateCard(_card, txtNotes.Text, txtRarity.Text, txtSet.Text);
            _repo.UpdateCard(_card, txtSet.Text);
            _dictionary.Remove(_card.Id);
        }

        private void pictureBoxSwitch_Click(object sender, EventArgs e)
        {
            if (_images.Count() >= 2)
            {
                string imageOne = "http://mtgimage.com/card/" + _images[0] + ".jpg";
                string imageTwo = "http://mtgimage.com/card/" + _images[1] + ".jpg";

                pictureBoxCardImage.ImageLocation = _showSecond ? _card.Image : imageTwo;
                _showSecond = !_showSecond;
            }
        }

        private void txtNotes_MouseMove(object sender, MouseEventArgs e)
        {
            if (txtNotes.TextLength == 0)
                return;

            Point lastCharPoint = txtNotes.GetPositionFromCharIndex(txtNotes.TextLength - 1);

            if (e.Y > _textHeight || (e.Y >= lastCharPoint.Y && e.X > lastCharPoint.X + _textHeight - lastCharPoint.Y))
            {
                _tt.Hide(txtNotes);
                _k = -1;
                return;
            }

            int i = txtNotes.GetCharIndexFromPosition(e.Location);
            int m = i, n = i;

            while (m > -1 && !Punctuations.Contains(txtNotes.Text[m]))
                m--;

            m++;

            while (n < txtNotes.TextLength && !Punctuations.Contains(txtNotes.Text[n]))
                n++;

            if (n > m)
            {
                string word = txtNotes.Text.Substring(m, n - m);

                if (_repo.CardNotes().ContainsKey(word))
                {
                    if (_k != m)
                    {
                        _tt.Show(_repo.CardNotes()[word], txtNotes, e.X, e.Y + 10);
                        _k = m;
                    }
                }
                else
                {
                    _tt.Hide(txtNotes);
                    _k = -1;
                }
            }
        }

        private void txtNotes_MouseLeave(object sender, EventArgs e)
        {
            _tt.Hide(txtNotes);
            _k = -1;
        }

        private void txtNotes_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            _textHeight = e.NewRectangle.Height;
        }

        private void txtNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
