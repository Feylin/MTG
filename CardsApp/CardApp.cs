using BusinessEntities;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ListBox = System.Windows.Forms.ListBox;
using RadioButton = System.Windows.Forms.RadioButton;

namespace CardsApp
{
    public partial class CardApp : Form
    {
        private object _lastSelectedCard;
        private int _index;
        private readonly BLL_NumbersOnly _bllNumbersOnly;
        private readonly BLL_ScreenCapture _bllSc;
        private readonly Repository _repo = new Repository();
        private string[] _deck;
        private List<string> _deckList = new List<string>();
        private readonly List<string> _singlesList = new List<string>();
        private readonly List<string> _currentHand = new List<string>();
        private readonly List<string> _previousHand = new List<string>();
        private readonly List<string> _tmpList = new List<string>();
        private bool _draw;
        private bool _drawnFromDeck;
        private bool _sealedDrawn;
        private int _hand = 7;
        private const string Title = @"Magic the Gathering Card Application";
        private const string ErrorMessageDeck = @"You can only have a maximum of 4 cards with the same name in each deck";
        private int _deckCount;
        private const string DeckCount = @"Count: ";
        private BE_Card[] _cards;
        private readonly Dictionary<int, BE_Card> _cardDictionary = new Dictionary<int, BE_Card>();
        private delegate void UpdateUiDelegate(bool isDataLoaded);
        private LoadingBox _loadingForm;

        public CardApp()
        {
            InitializeComponent();

            try
            {
                _bllNumbersOnly = BLL_NumbersOnly.Instance;
                _bllSc = BLL_ScreenCapture.Instance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
            }

            InitialSetup();
        }

        private void InitialSetup()
        {
            _cards = _repo.Cards();
            //AutoCompleteStringCollection cardList = new AutoCompleteStringCollection();
            //string[] autocomplete = new string[_cards.Count()];

            //AcceptButton = btnDrawSeven;

            btnMulligan.Enabled = false;
            btnVisualHand.Enabled = false;

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            lblShuffling.Text = string.Empty;

            toolTip.SetToolTip(btnDrawSeven, @"Draw seven cards from the selected deck.");
            toolTip.SetToolTip(btnMulligan, @"Draw a new hand with one card less than your current hand.");
            toolTip.SetToolTip(btnReset, @"Reset your current deck and hands.");
            toolTip.SetToolTip(btnDrawSingle, @"Draw the next card in your card.");
            toolTip.SetToolTip(btnVisualHand, @"Display a new window with images of the cards in your current hand.");

            toolTip.SetToolTip(btnAddToDeck, @"Add the amount of the selected to the deck.");
            toolTip.SetToolTip(btnRemoveFromDeck, @"Remove the selected cards from the deck.");
            toolTip.SetToolTip(btnClearDeck, @"Remove all the cards in your deck.");

            toolTip.SetToolTip(btnSealedCardPool, @"Generate random cards from the selected amount of sets.");

            //int index = 0;

            //foreach (var card in _cards)
            //{
            //    lstMagicCards.Items.Add(card);
            //    //cardList.Add(card.ToString());
            //    //autocomplete[index] = card.ToString();
            //    //index++;
            //    lblMagicCardsCount.Text = @"Count: " + _cards.Count();
            //}

            lstMagicCards.Items.AddRange(_cards);
            lblMagicCardsCount.Text = @"Count: " + _cards.Count();

            //txtQuery.AutoCompleteCustomSource = cardList;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (_deck == null)
                MessageBox.Show(@"Please open a textfile with your deck before continuing", Title);
            else
            {
                if (!string.IsNullOrWhiteSpace(txtShuffleTime.Text) && !txtShuffleTime.Text.Equals("0") &&
                    !txtShuffleTime.Text.Equals("00"))
                {
                    //btnMulligan.Enabled = true;

                    //_draw = false;
                    //lstCards.DataSource = null;
                    //lstCards.Items.Clear();
                    //_cards.Clear();

                    ShuffleAndDraw(7);
                }
                else
                    MessageBox.Show(@"Please specify shuffle time", Title);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_deck == null)
                MessageBox.Show(@"Nothing to reset", Title);
            else
                Clear();
        }

        private void btnMulligan_Click(object sender, EventArgs e)
        {
            Mulligan();
        }

        private void Mulligan()
        {
            if (!string.IsNullOrWhiteSpace(txtShuffleTime.Text) && !txtShuffleTime.Text.Equals("0") &&
                !txtShuffleTime.Text.Equals("00"))
            {
                //Clear();

                //lblCardLeftInDeck.Text = @"Cards left in the deck: " + _deck.Count();

                lstSingleDraws.Items.Clear();
                _deckList.Clear();
                _singlesList.Clear();

                _draw = false;

                lstPreviousHand.DataSource = null;
                lstPreviousHand.Items.Clear();
                _previousHand.Clear();

                foreach (var card in lstCurrentHand.Items)
                    _previousHand.Add(card.ToString());

                lstPreviousHand.DataSource = _previousHand;

                lstCurrentHand.DataSource = null;
                lstCurrentHand.Items.Clear();
                _currentHand.Clear();

                _hand--;

                _deckList.AddRange(_tmpList);
                _tmpList.AddRange(_deckList.GetRange(0, _hand));
                _deckList.RemoveRange(0, _hand);

                ShuffleAndDraw(_hand);

                if (_hand == 1)
                {
                    btnMulligan.Enabled = false;
                    btnReset.Focus();
                    //AcceptButton = btnReset;
                }
            }
            else
                MessageBox.Show(@"Please specify shuffle time", Title);
        }

        private void Clear()
        {
            lstSingleDraws.Items.Clear();
            _deckList.Clear();
            _singlesList.Clear();

            _tmpList.Clear();

            _drawnFromDeck = false;
            _draw = false;
            btnMulligan.Enabled = false;
            btnDrawSingle.Enabled = false;
            btnVisualHand.Enabled = false;

            lblShuffling.Text = string.Empty;
            lblCardLeftInDeck.Text = @"Cards left in the deck: ";
            lblChosenDeck.Text = @"Shuffle Simulator";

            lstCurrentHand.DataSource = null;
            lstCurrentHand.Items.Clear();
            _currentHand.Clear();

            lstPreviousHand.DataSource = null;
            lstPreviousHand.Items.Clear();
            _previousHand.Clear();

            btnDrawSeven.Enabled = true;
            btnDrawSeven.Focus();

            _hand = 7;

            _deck = null;

            chartManaCurve.Legends[1].CustomItems[0].Name = "Curve for This Deck";

            const string zero = "0";

            txtCurveZero.Text = zero;
            txtCurveOne.Text = zero;
            txtCurveTwo.Text = zero;
            txtCurveThree.Text = zero;
            txtCurveFour.Text = zero;
            txtCurveFive.Text = zero;
            txtCurveSix.Text = zero;
            txtCurveSeven.Text = zero;
            txtCurveEight.Text = zero;
        }

        private void ShuffleAndDraw(int cards)
        {
            if (_draw.Equals(false))
            {
                try
                {
                    int shuffleCount = Convert.ToInt32(txtShuffleTime.Text);
                    DateTime start = DateTime.Now;

                    // Shuffle a certain amount of time
                    using (new BLL_CCursor(Cursors.AppStarting))
                    {
                        while (DateTime.Now.Subtract(start).Seconds < shuffleCount)
                        {
                            Application.DoEvents();
                            Thread.Sleep(1);
                            //_bllCards.Shuffle(_beCards.Cards);
                            _repo.Shuffle(_deck);
                            lblShuffling.Text = @"Shuffling - " + DateTime.Now.Subtract(start).ToString("g");
                            btnMulligan.Enabled = false;
                            btnVisualHand.Enabled = false;
                            btnDrawSingle.Enabled = false;
                        }
                    }

                    _deckList = new List<string>(_deck);
                    _tmpList.Clear();
                    _tmpList.AddRange(_deckList.GetRange(0, cards));
                    _deckList.RemoveRange(0, cards);
                    lblCardLeftInDeck.Text = @"Cards left in the deck: " + _deckList.Count();

                    btnVisualHand.Enabled = true;
                    btnDrawSingle.Enabled = true;
                    btnMulligan.Enabled = true;
                    lblShuffling.Text = string.Empty;

                    // Shuffle the deck a certain amount of times
                    //for (int i = 0; i < shuffleCount; i++)
                    //    _bllCards.Shuffle(_beCards.Cards);

                    for (int i = 0; i < cards; i++)
                        //lblCard.Text += string.Join("", _beCards.Cards[i]);
                        _currentHand.Add(_deck[i]);

                    lstCurrentHand.DataSource = _currentHand;

                    _draw = true;
                    _drawnFromDeck = true;
                    btnDrawSeven.Enabled = false;
                    btnMulligan.Focus();
                    //AcceptButton = btnMulligan;
                }
                catch (Exception)
                {
                    _draw = false;
                }
            }
        }

        private void CardApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            switch (MessageBox.Show(@"Are you sure you want to close?", Title, MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTextFile();
        }

        private void OpenTextFile()
        {
            if (_drawnFromDeck.Equals(true))
                MessageBox.Show(@"Reset to choose a new deck", Title);
            else
            {
                using (OpenFileDialog ofd = new OpenFileDialog { Title = @"Open text file", Filter = @"Text file |*.txt" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string deckName = Path.GetFileNameWithoutExtension(ofd.SafeFileName);

                        _deck = File.ReadAllLines(ofd.FileName);
                        lblChosenDeck.Text = @"Chosen Deck: " + deckName + @" - Deck Size: " + _deck.Length;
                        lblCardLeftInDeck.Text = @"Cards left in the deck: " + _deck.Length;
                        chartManaCurve.Legends[1].CustomItems[0].Name = string.Format(@"Curve for {0}", deckName);

                        using (new BLL_CCursor(Cursors.WaitCursor))
                        {
                            UpdateUi(false);
                            _loadingForm = new LoadingBox();

                            Thread t = new Thread(FillManaCurve)
                            {
                                IsBackground = true
                            };
                            t.Start();

                            using (_loadingForm = new LoadingBox())
                                _loadingForm.ShowDialog();
                        }
                    }
                }

                txtShuffleTime.Focus();
            }
        }

        private void FillManaCurve()
        {
            int[] manaCurveArray = _repo.CreateManaCurveForDeck(_deck);

            CultureInfo ci = CultureInfo.InvariantCulture;

            txtCurveZero.Text = manaCurveArray[0].ToString(ci);
            txtCurveOne.Text = manaCurveArray[1].ToString(ci);
            txtCurveTwo.Text = manaCurveArray[2].ToString(ci);
            txtCurveThree.Text = manaCurveArray[3].ToString(ci);
            txtCurveFour.Text = manaCurveArray[4].ToString(ci);
            txtCurveFive.Text = manaCurveArray[5].ToString(ci);
            txtCurveSix.Text = manaCurveArray[6].ToString(ci);
            txtCurveSeven.Text = manaCurveArray[7].ToString(ci);
            txtCurveEight.Text = manaCurveArray[8].ToString(ci);

            Invoke(new UpdateUiDelegate(UpdateUi), new object[] { true });
        }

        private void UpdateUi(bool isDataLoaded)
        {
            if (isDataLoaded)
            {
                if (_loadingForm != null)
                    _loadingForm.Dispose();
            }
        }

        private void txtBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            _bllNumbersOnly.BlockPasteEvent(sender, e);
        }

        private void txtBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            _bllNumbersOnly.SupressInvalidInput(e);
        }

        private void txtManaBase_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(((TextBox) sender).Text))
            //{

            try
            {
                int black = Convert.ToInt32(txtManaBlack.Text);
                int white = Convert.ToInt32(txtManaWhite.Text);
                int blue = Convert.ToInt32(txtManaBlue.Text);
                int green = Convert.ToInt32(txtManaGreen.Text);
                int red = Convert.ToInt32(txtManaRed.Text);

                int mana = black + white + blue + green + red;

                txtManaFinal.Text = mana.ToString(CultureInfo.InvariantCulture);

                int totalSymbols = Convert.ToInt32(txtManaFinal.Text);
                int totalLands = Convert.ToInt32(txtLands.Text);

                lblBlackMana.Text = _repo.CalculateLands(txtManaBlack.Text, totalSymbols, totalLands);
                lblWhiteMana.Text = _repo.CalculateLands(txtManaWhite.Text, totalSymbols, totalLands);
                lblBlueMana.Text = _repo.CalculateLands(txtManaBlue.Text, totalSymbols, totalLands);
                lblGreenMana.Text = _repo.CalculateLands(txtManaGreen.Text, totalSymbols, totalLands);
                lblRedMana.Text = _repo.CalculateLands(txtManaRed.Text, totalSymbols, totalLands);

                string replaceFrom = "lands".Trim();
                string replaceWith = string.Empty;

                double blackMana = Convert.ToDouble(lblBlackMana.Text.Replace(replaceFrom, replaceWith));
                double whiteMana = Convert.ToDouble(lblWhiteMana.Text.Replace(replaceFrom, replaceWith));
                double blueMana = Convert.ToDouble(lblBlueMana.Text.Replace(replaceFrom, replaceWith));
                double greenMana = Convert.ToDouble(lblGreenMana.Text.Replace(replaceFrom, replaceWith));
                double redMana = Convert.ToDouble(lblRedMana.Text.Replace(replaceFrom, replaceWith));

                double[] yValues = { blackMana, whiteMana, blueMana, greenMana, redMana };
                string[] xNames =
                {
                    (blackMana/totalLands).ToString("P"), (whiteMana/totalLands).ToString("P"),
                    (blueMana/totalLands).ToString("P"), (greenMana/totalLands).ToString("P"),
                    (redMana/totalLands).ToString("P")
                };

                chartManaBase.Series[0].Points.DataBindXY(xNames, yValues);

                for (int i = 0; i < 5; i++)
                    chartManaBase.Series[0].Points[i].CustomProperties = "Exploded=true";

                if (black == 0)
                    chartManaBase.Series[0].Points[0].IsEmpty = true;
                if (white == 0)
                    chartManaBase.Series[0].Points[1].IsEmpty = true;
                if (blue == 0)
                    chartManaBase.Series[0].Points[2].IsEmpty = true;
                if (green == 0)
                    chartManaBase.Series[0].Points[3].IsEmpty = true;
                if (red == 0)
                    chartManaBase.Series[0].Points[4].IsEmpty = true;

                Color[] colors =
                {
                    Color.FromArgb(170, 170, 170), Color.FromArgb(255, 255, 102),
                    Color.FromArgb(102, 102, 255), Color.FromArgb(102, 221, 102), Color.FromArgb(255, 102, 102)
                };
                for (int i = 0; i < colors.Count(); i++)
                    chartManaBase.Series[0].Points[i].Color = colors[i];
            }
            catch (FormatException)
            {
            }

            //}
            //else
            //    MessageBox.Show(@"Mana can't be empty", Title);
        }

        private void saveScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Title = @"Save an Image File", Filter = @"Jpeg Image |*.jpg" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (sfd.FileName != string.Empty)
                    {
                        //Rectangle bounds = Bounds;

                        //using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                        //{
                        //    using (Graphics g = Graphics.FromImage(bitmap))
                        //        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

                        //    bitmap.Save(sfd.FileName, ImageFormat.Jpeg);
                        //}

                        _bllSc.CaptureWindowToFile(Handle, sfd.FileName, ImageFormat.Jpeg);
                    }
                }
            }
        }

        private void txtManaCurve_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ccZero = Convert.ToInt32(txtCurveZero.Text);
                int ccOne = Convert.ToInt32(txtCurveOne.Text);
                int ccTwo = Convert.ToInt32(txtCurveTwo.Text);
                int ccThree = Convert.ToInt32(txtCurveThree.Text);
                int ccFour = Convert.ToInt32(txtCurveFour.Text);
                int ccFive = Convert.ToInt32(txtCurveFive.Text);
                int ccSix = Convert.ToInt32(txtCurveSix.Text);
                int ccSeven = Convert.ToInt32(txtCurveSeven.Text);
                int ccEight = Convert.ToInt32(txtCurveEight.Text);

                int[] xValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                int[] yValues = { ccZero, ccOne, ccTwo, ccThree, ccFour, ccFive, ccSix, ccSeven, ccEight };

                chartManaCurve.Series[1].Points.DataBindXY(xValues, yValues);
                chartManaCurve.Series[3].Points.DataBindXY(xValues, yValues);
            }
            catch (FormatException)
            {
            }
        }

        private void btnAddToDeck_Click(object sender, EventArgs e)
        {
            BE_Card selectedCard = lstMagicCards.SelectedItem as BE_Card;
            int value = (int)numCardAmount.Value;
            int count = 1;
            string[] basicLands =
            {
                "Forest", "Plains", "Island", "Swamp", "Mountain",
                "Snow-Covered Forest", "Snow-Covered Plains", "Snow-Covered Island", 
                "Snow-Covered Swamp", "Snow-Covered Mountain", "Shadowborn Apostle"
            };

            if (selectedCard != null && basicLands.Contains(selectedCard.Name))
            {
                for (int i = 0; i < value; i++)
                {
                    lstDeck.Items.Add(selectedCard);
                    _deckCount++;
                    lblDeckCount.Text = DeckCount + _deckCount;
                }
            }
            else
            {
                if (numCardAmount.Value > 4)
                    MessageBox.Show(ErrorMessageDeck, Title);
                else
                {
                    if (value == 2)
                        count = 0;

                    AddCard(selectedCard, count, value);
                }
            }
        }

        private void btnClearDeck_Click(object sender, EventArgs e)
        {
            lstDeck.Items.Clear();
            _deckCount = 0;
            lblDeckCount.Text = DeckCount + _deckCount;
        }

        private void saveDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblDeckCount.Text.Replace(@"Count:".Trim(), string.Empty)) < 60)
                MessageBox.Show(@"A deck must consist of 60 or more cards", Title);
            else
            {
                using (SaveFileDialog sfd = new SaveFileDialog { Title = @"Save a Text File with your deck", Filter = @"Txt File |*.txt" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (sfd.FileName != string.Empty)
                        {
                            using (StreamWriter sw = new StreamWriter(sfd.FileName, false))
                            {
                                foreach (var card in lstDeck.Items)
                                    sw.WriteLine(card);
                            }
                        }
                    }
                }
            }
        }

        private void lstMagicCards_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowCardInfo(sender);
        }

        private void ShowCardInfo(object sender)
        {
            var selectedCard = ((ListBox)sender).SelectedItem as BE_Card;

            if (selectedCard != null)
            {
                var cardId = selectedCard.Id.ToString(CultureInfo.InvariantCulture);

                CardInfo outputForm = new CardInfo(selectedCard, _cardDictionary);

                if (_cardDictionary.ContainsValue(selectedCard))
                    Application.OpenForms[cardId].Activate();
                else
                {
                    _cardDictionary.Add(selectedCard.Id, selectedCard);
                    outputForm.Name = cardId;

                    outputForm.Show();
                }
            }
        }

        private void AddCard(object selectedCard, int count, int value)
        {
            foreach (var card in lstDeck.Items)
            {
                if (card == selectedCard)
                    count += value;
            }

            if (count > 4)
                MessageBox.Show(ErrorMessageDeck, Title);
            else
            {
                for (int i = 0; i < value; i++)
                {
                    lstDeck.Items.Add(selectedCard);
                    _deckCount++;
                    lblDeckCount.Text = DeckCount + _deckCount;
                }
            }
        }

        private void btnDrawSingle_Click(object sender, EventArgs e)
        {
            if (_deckList.Count == 0)
                MessageBox.Show(@"No more cards in the deck", Title);
            else
            {
                lstSingleDraws.Items.Clear();

                _singlesList.Add(_deckList[0]);

                _deckList.RemoveAt(0);

                foreach (var card in _singlesList)
                    lstSingleDraws.Items.Add(card);

                lblCardLeftInDeck.Text = @"Cards left in the deck: " + _deckList.Count();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox outputForm = new AboutBox();
            outputForm.ShowDialog();
        }

        private void txtShuffleTime_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnDrawSeven;

            if (_draw.Equals(true))
                AcceptButton = btnMulligan;

            if (_hand == 1)
                AcceptButton = btnReset;
        }

        private void txtQuery_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            string query = txtQuery.Text.ToLowerInvariant().Trim();

            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(query))
            {
                if (ListContains(query) == true)
                {
                    List<int> indices = new List<int>();

                    foreach (BE_Card card in lstMagicCards.Items)
                    {
                        string cardName = card.Name.ToLowerInvariant();

                        if ((!string.IsNullOrEmpty(cardName)) && cardName.Contains(query))
                            indices.Add(lstMagicCards.Items.IndexOf(card));
                    }

                    if (_index >= indices.Count)
                        _index = 0;

                    int lastSelectedIndex = indices[_index];

                    if (_index != -1)
                        lstMagicCards.SetSelected(lastSelectedIndex, true);

                    if (lstMagicCards.TopIndex != lstMagicCards.SelectedIndex)
                        lstMagicCards.TopIndex = lastSelectedIndex;

                    _index++;
                }
                else
                    MessageBox.Show(@"The search string did not match any items in the list", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ListContains(string str)
        {
            foreach (BE_Card card in lstMagicCards.Items)
                if (card.Name.ToLowerInvariant().Contains(str))
                    return true;
            return false;
        }

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;

            if (btn != null && btn.Checked)
            {
                switch (btn.Name)
                {
                    case "rbtnName":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards);
                        SortSelect();
                        break;
                    case "rbtnColor":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Color));
                        SortSelect();
                        break;
                    case "rbtnPower":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Power));
                        SortSelect();
                        break;
                    case "rbtnToughness":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Toughness));
                        SortSelect();
                        break;
                    case "rbtnType":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Type));
                        SortSelect();
                        break;
                    case "rbtnCMC":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Cmc));
                        SortSelect();
                        break;
                    case "rbtnRarity":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Rarity));
                        SortSelect();
                        break;
                    case "rbtnSet":
                        lstMagicCards.Items.Clear();
                        Array.Sort(_cards, new BE_Card(BE_Card.CardSortOptions.Set));
                        SortSelect();
                        break;
                }
            }
        }

        private void SortSelect()
        {
            lstMagicCards.Items.AddRange(_cards);
            if (_lastSelectedCard != null)
            {
                lstMagicCards.SelectedItem = _lastSelectedCard;
                lstMagicCards.TopIndex = lstMagicCards.Items.IndexOf(_lastSelectedCard);
            }
            txtQuery.Focus();
        }

        private void btnVisualHand_Click(object sender, EventArgs e)
        {
            List<BE_Card> hand =
                (from object card in lstCurrentHand.Items select _repo.GetCardFromCardname(card.ToString())).ToList();

            VisualHand outputForm = new VisualHand(hand)
            {
                StartPosition = FormStartPosition.CenterParent
            };

            outputForm.ShowDialog();
        }

        private void lstMagicCards_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush myBrush = new SolidBrush(Color.Black);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(166, 210, 255)), e.Bounds);

            if (((ListBox)sender).Items.Count > e.Index && e.Index != -1)
            {
                BE_Card card = ((ListBox)sender).Items[e.Index] as BE_Card;

                if (card != null)
                {
                    Color cardColor = Color.FromName(card.Color);

                    switch (card.Color)
                    {
                        case "white":
                            myBrush = new SolidBrush(Color.FromArgb(100, 100, 100));
                            break;
                        case "colorless":
                            myBrush = new SolidBrush(Color.FromArgb(167, 0, 167));
                            break;
                        case "multi":
                            myBrush = new SolidBrush(Color.FromArgb(255, 95, 17));
                            break;
                        default:
                            myBrush = new SolidBrush(cardColor);
                            break;
                    }
                }
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        private void lstDeck_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnRemoveFromDeck_Click(sender, e);
        }

        private void btnRemoveFromDeck_Click(object sender, EventArgs e)
        {
            while (lstDeck.SelectedIndices.Count > 0)
            {
                lstDeck.Items.RemoveAt(lstDeck.SelectedIndices[0]);
                _deckCount--;
                lblDeckCount.Text = DeckCount + _deckCount;
            }
        }

        private void lstMagicCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lastSelectedCard = lstMagicCards.SelectedItem;
        }

        private void btnSealed_Click(object sender, EventArgs e)
        {
            SealedDraft();
        }

        private void SealedDraft()
        {
            if (_sealedDrawn == false)
            {
                DrawSealedPool();
            }
            else if (_sealedDrawn)
            {
                DialogResult result = MessageBox.Show(@"Drawn new sealed pool?", Title, MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    lstSealedDeck.Items.Clear();
                    lstSealedPacks.Items.Clear();
                    DrawSealedPool();
                }
            }
        }

        private void DrawSealedPool()
        {
            List<BE_Card> setOne = new List<BE_Card>();
            List<BE_Card> setTwo = new List<BE_Card>();
            int setOneAmount = (int)numSetOne.Value;
            int setTwoAmount = (int)numSetTwo.Value;

            if (!cmbSetOne.Text.Equals(cmbSetTwo.Text))
            {
                if (cmbSetOne.SelectedIndex != -1 && cmbSetOne.SelectedIndex != -1 && setOneAmount + setTwoAmount == 6)
                {
                    lstSealedPacks.Items.Add(_repo.GetCardFromCardname("Swamp"));
                    lstSealedPacks.Items.Add(_repo.GetCardFromCardname("Forest"));
                    lstSealedPacks.Items.Add(_repo.GetCardFromCardname("Island"));
                    lstSealedPacks.Items.Add(_repo.GetCardFromCardname("Plains"));
                    lstSealedPacks.Items.Add(_repo.GetCardFromCardname("Mountain"));

                    for (int i = 0; i < numSetOne.Value; i++)
                        setOne.AddRange(_repo.Boosterpack(cmbSetOne.Text));

                    for (int i = 0; i < numSetTwo.Value; i++)
                        setTwo.AddRange(_repo.Boosterpack(cmbSetTwo.Text));

                    List<BE_Card> sealedCards = new List<BE_Card>(setOne.Concat(setTwo));
                    sealedCards.Sort();

                    foreach (BE_Card card in sealedCards)
                        lstSealedPacks.Items.Add(card);

                    lblSealedPackCount.Text = @"Count: " + lstSealedPacks.Items.Count;

                    _sealedDrawn = true;
                }
                else
                    MessageBox.Show(@"Sealed consists of six boosterpacks", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(@"The two sets can't be equal to eachother", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnAddToSealedDeck_Click(object sender, EventArgs e)
        {
            BE_Card selectedCard = lstSealedPacks.SelectedItem as BE_Card;

            string[] basicLands =
            {
                "Forest", "Plains", "Island", "Swamp", "Mountain"
            };

            //while (lstSealedPacks.SelectedItems.Count > 0)
            //{
            if (selectedCard != null)
            {
                if (!basicLands.Contains(selectedCard.ToString()))
                {
                    lstSealedDeck.Items.Add(selectedCard);
                    lstSealedPacks.Items.Remove(selectedCard);
                }
                else
                    lstSealedDeck.Items.Add(selectedCard);

                lblSealedDeckCount.Text = @"Count: " + lstSealedDeck.Items.Count;
                lblSealedPackCount.Text = @"Count: " + lstSealedPacks.Items.Count;
            }
            //}
        }

        private void btnRemoveFromSealedDeck_Click(object sender, EventArgs e)
        {
            BE_Card selectedCard = lstSealedDeck.SelectedItem as BE_Card;

            string[] basicLands =
            {
                "Forest", "Plains", "Island", "Swamp", "Mountain"
            };

            //while (lstSealedPacks.SelectedItems.Count > 0)
            //{
            if (selectedCard != null)
            {
                if (!basicLands.Contains(selectedCard.ToString()))
                {
                    lstSealedPacks.Items.Add(selectedCard);
                    lstSealedDeck.Items.Remove(selectedCard);
                }
                else
                    lstSealedDeck.Items.Remove(selectedCard);

                lblSealedDeckCount.Text = @"Count: " + lstSealedDeck.Items.Count;
                lblSealedPackCount.Text = @"Count: " + lstSealedPacks.Items.Count;
            }
            //}
        }
    }
}

