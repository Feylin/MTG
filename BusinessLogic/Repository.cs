using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using BusinessEntities;
using DataAccess;
using MagicModel;
using PWDTK_DOTNET451;

namespace BusinessLogic
{
    public class Repository : IRepository
    {
        private readonly RNGCryptoServiceProvider _gen;
        private readonly Magic_the_GatheringEntities _dbContext;
        private readonly DAL_WordLists _wordlists;
        private readonly bool _shareContext;
        private const int Iteration = 23600;

        public Repository()
        {
            _gen = new RNGCryptoServiceProvider();
            _dbContext = new Magic_the_GatheringEntities();
            _wordlists = DAL_WordLists.Instance;
        }

        public Repository(Magic_the_GatheringEntities dbContext)
        {
            _dbContext = dbContext;
            _shareContext = true;
        }

        public void Dispose()
        {
            if (_shareContext.Equals(true) && (_dbContext != null))
                _dbContext.Dispose();
        }

        public BE_Card[] Cards()
        {
            Card[] ef_Cards = _dbContext.Cards.ToArray();
            BE_Card[] cards = new BE_Card[ef_Cards.Count()];

            foreach (var card in ef_Cards)
            {
                cards[card.id - 1] = new BE_Card(card.id, card.name, card.color, card.power, card.toughness, card.cmc, card.image, card.type, card.notes, card.rarity, card.set);
            }

            //Array.Sort(cards, (card1, card2) => card1.Name.CompareTo(card2.Name));
            // Implement IComparable
            Array.Sort(cards);
            
            return cards;
        }

        public string[] MagicCards()
        {
            string[] magicCards = _dbContext.Cards.Select(card => card.name).ToArray();

            Array.Sort(magicCards);

            return magicCards;
        }

        public bool UpdateCard(BE_Card card, string notes, string rarity, string set)
        {
            List<Card> cards = _dbContext.Cards.ToList();

            var selectedCard = (from c in cards
                                where c.name == card.Name
                                select c).FirstOrDefault();

            if (selectedCard != null)
            {
                bool cardUpdated;
                try
                {
                    selectedCard.notes = notes;
                    selectedCard.rarity = rarity;
                    selectedCard.set = set;

                    _dbContext.SaveChanges();

                    cardUpdated = true;
                }
                catch
                {
                    cardUpdated = false;
                }

                return cardUpdated;
            }

            return false;
        }

        public BE_Card GetCardFromCardname(string cardName)
        {
            List<Card> cardList = _dbContext.Cards.ToList();
            List<BE_Card> beCards = new List<BE_Card>();

            foreach (var c in cardList)
            {
                BE_Card card = new BE_Card(c.id, c.name, c.color, c.power, c.toughness, c.cmc, c.image, c.type, c.notes, c.rarity, c.set);
                beCards.Add(card);
            }

            var selectedCard = (from cards in beCards
                                where cards.Name == cardName
                                select cards).SingleOrDefault();

            return selectedCard;
        }

        private List<BE_Card> GetRandomCards(string rarity, string set, int amount)
        {
            List<Card> cardList = _dbContext.Cards.ToList();
            List<BE_Card> beCards = new List<BE_Card>();

            foreach (var c in cardList)
            {
                BE_Card card = new BE_Card(c.id, c.name, c.color, c.power, c.toughness, c.cmc, c.image, c.type, c.notes, c.rarity, c.set);
                beCards.Add(card);
            }

            var selectedCard = //(from card in beCards
                               beCards.Where(card => card.Set.Contains(set) && card.Rarity == rarity)
                               .OrderBy(card => Guid.NewGuid())
                               .Take(amount).ToList();
                               //select card).SingleOrDefault();

            return selectedCard;
        }

        public List<BE_Card> Boosterpack(string set)
        {
            int rnd = Random(1, 1001);

            List<BE_Card> common = GetRandomCards("Common", set, 11);
            List<BE_Card> uncommon = GetRandomCards("Uncommon", set, 3);
            //875 in 1000 to get a rare, 87.5% chance - 125 in 1000 to get a mythic rare, 12.5% chance
            List<BE_Card> rare = GetRandomCards(rnd < 875 ? "Rare" : "Mythic Rare", set, 1);

            List<BE_Card> booster = new List<BE_Card>(common.Concat(uncommon).Concat(rare));

            return booster;
        }

        public int[] CreateManaCurveForDeck(IEnumerable<string> deck)
        {
            int cmcZero = 0, cmcOne = 0, cmcTwo = 0, cmcThree = 0, cmcFour = 0, cmcFive = 0, cmcSix = 0, cmcSeven = 0, cmcEight = 0;
            int[] cmc = new int[9];

            List<BE_Card> beCards = deck.Select(GetCardFromCardname).ToList();

            foreach (var card in beCards)
            {
                switch (card.Cmc)
                {
                    case 0:
                        cmcZero++;
                        break;
                    case 1:
                        cmcOne++;
                        break;
                    case 2:
                        cmcTwo++;
                        break;
                    case 3:
                        cmcThree++;
                        break;
                    case 4:
                        cmcFour++;
                        break;
                    case 5:
                        cmcFive++;
                        break;
                    case 6:
                        cmcSix++;
                        break;
                    case 7:
                        cmcSeven++;
                        break;
                }
                if (card.Cmc >= 8)
                    cmcEight++;
            }

            cmc[0] = cmcZero;
            cmc[1] = cmcOne;
            cmc[2] = cmcTwo;
            cmc[3] = cmcThree;
            cmc[4] = cmcFour;
            cmc[5] = cmcFive;
            cmc[6] = cmcSix;
            cmc[7] = cmcSeven;
            cmc[8] = cmcEight;

            return cmc;
        }

        public string RandomCard(string[] deck, int deckSize)
        {
            var card = deck[Random(0, deckSize)]; // min and max without rng
            //var card = deck[Random(deckSize, _gen)]; max with rng

            return card;
        }

        // Original - backup
        //private static int Random(int max, RandomNumberGenerator rand)
        //{
        //    byte[] buffer = new byte[sizeof(Int32)];
        //    rand.GetBytes(buffer);
        //    int random = BitConverter.ToInt32(buffer, 0);

        //    return (Math.Abs(random % (max - 0)) + 0);
        //}

        public int Random(int min, int max)
        {
            // switch min and max around
            if (min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }
            if (min == max)
                throw new DivideByZeroException("The two parameteres can't be equal to each other");

            byte[] buffer = new byte[sizeof(Int32)];
            //RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            //rand.GetBytes(buffer);
            _gen.GetBytes(buffer);
            int random = BitConverter.ToInt32(buffer, 0);

            return (Math.Abs(random % (max - min)) + min);
        }

        public void Shuffle(string[] deck)
        {
            for (int n = deck.Length - 1; n > 0; --n)
            {
                int k = Random(0, n + 1); // min and max without rng
                //int k = Random(n + 1, _gen); max with rng
                //int k = _rnd.Next(n + 1); max without rng

                string temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }
        }

        public string CalculateLands(string color, int totalSymbols, int totalLands)
        {
            double symbols = Convert.ToDouble(color);

            double recommendedLands = ((symbols / totalSymbols) * totalLands);

            double mana = Math.Truncate(((recommendedLands) * 100)) / 100;

            CultureInfo ci = new CultureInfo("da-DK");

            return $"{mana:N2}" + @" lands";
        }

        public Dictionary<string, string> CardNotes()
        {
            Dictionary<string, string> cardNotes = _wordlists.CardNotes();

            return cardNotes;
        }

        public bool CheckConnection(string hostName)
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead(hostName))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool PingNetwork(string hostNameOrAddress)
        {
            bool pingStatus;

            using (Ping p = new Ping())
            {
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                try
                {
                    PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                    pingStatus = (reply.Status == IPStatus.Success);
                }
                catch (Exception)
                {
                    pingStatus = false;
                }
            }

            return pingStatus;
        }

        public void HighlightWords(RichTextBox textBox)
        {
            //List<String> coloredWords = _wordlists.ColoredWordList();
            Dictionary<string, Color> manaWords = _wordlists.ManaList();
            var italicWords = _wordlists.ItalicWordList();
            var boldWords = _wordlists.BoldWordList();

            Color color = Color.FromArgb(139, 0, 0);

            Font fontItalic = new Font(textBox.Font, FontStyle.Italic);
            Font fontBold = new Font(textBox.Font, FontStyle.Bold);

            foreach (var word in CardNotes().Keys)
                if (textBox.Text.ToLowerInvariant().Contains(word.ToLowerInvariant()))
                    HighLightTextColor(textBox, word, color);

            foreach (var word in manaWords)
                ManaColor(textBox, word.Key, word.Value);

            foreach (var word in italicWords)
                if (textBox.Text.ToLowerInvariant().Contains(word.ToLowerInvariant()))
                    HighLightTextFont(textBox, word, fontItalic);

            foreach (var word in boldWords)
                if (textBox.Text.ToLowerInvariant().Contains(word.ToLowerInvariant()))
                    HighLightTextFont(textBox, word, fontBold);
        }

        private void ManaColor(RichTextBox textBox, string mana, Color color)
        {
            if (textBox.Text.Contains(mana))
                switch (mana)
                {
                    case "{R}":
                        HighLightTextColor(textBox, mana.ToLowerInvariant(), color);
                        break;
                    case "{B}":
                        HighLightTextColor(textBox, mana.ToLowerInvariant(), color);
                        break;
                    case "{W}":
                        HighLightTextColor(textBox, mana.ToLowerInvariant(), color);
                        break;
                    case "{U}":
                        HighLightTextColor(textBox, mana.ToLowerInvariant(), color);
                        break;
                    case "{G}":
                        HighLightTextColor(textBox, mana.ToLowerInvariant(), color);
                        break;
                }
        }

        private void HighLightTextFont(RichTextBox textBox, string word, Font font)
        {
            int stIndex = 0;
            stIndex = textBox.Find(word, stIndex, RichTextBoxFinds.WholeWord);

            while (stIndex != -1)
            {
                if (textBox.SelectedText.ToLowerInvariant() == word)
                {
                    textBox.SelectionLength = word.Length;
                    textBox.SelectionFont = font;
                }
                stIndex = textBox.Find(word, stIndex + 1, RichTextBoxFinds.WholeWord);
            }
        }

        private void HighLightTextColor(RichTextBox textBox, string word, Color color)
        {
            int stIndex = 0;
            stIndex = textBox.Find(word, stIndex, RichTextBoxFinds.WholeWord);

            while (stIndex != -1)
            {
                if (textBox.SelectedText.ToLowerInvariant() == word)
                {
                    textBox.SelectionLength = word.Length;
                    textBox.SelectionColor = color;
                }
                stIndex = textBox.Find(word, stIndex + 1, RichTextBoxFinds.WholeWord);
            }
        }

        public bool AddUser(string name, string password, string secret)
        {
            bool userAdded;
            byte[] saltBytes = PWDTK.GetRandomSalt();
            byte[] hashbBytes = PWDTK.PasswordToHash(saltBytes, password, Iteration);

            try
            {
                BE_User user = new BE_User(name, Convert.ToBase64String(saltBytes), Convert.ToBase64String(hashbBytes), secret);
                User us = new User
                {
                    username = user.Name, 
                    salt = user.Salt,
                    password = user.Password,
                    secret =  user.Secret
                };

                _dbContext.Users.Add(us);

                _dbContext.SaveChanges();

                userAdded = true;
            }
            catch
            {
                userAdded = false;
            }

            return userAdded;
        }

        public BE_User GetUserByUsername(string user)
        {
            List<User> usr = _dbContext.Users.ToList();
            List<BE_User> userss = new List<BE_User>();

            foreach (var users in usr)
            {
                BE_User u = new BE_User(users.userid, users.username, users.salt, users.password, users.secret);
                userss.Add(u);
            }

            var selectedUser = (from users in userss
                                where users.Name == user
                                select users).SingleOrDefault();

            return selectedUser;
        }

        public string ForgotLogin(string user, string secret)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZØÆÅ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 8)
                                    .Select(s => s[random.Next(s.Length)])
                                    .ToArray());

            byte[] saltBytes = PWDTK.GetRandomSalt();
            byte[] hashbBytes = PWDTK.PasswordToHash(saltBytes, result, Iteration);

            List<User> users = _dbContext.Users.ToList();

            var selectedUser = (from c in users
                                where c.username == user && c.secret == secret
                                select c).FirstOrDefault();

            if (selectedUser != null)
            {
                selectedUser.password = Convert.ToBase64String(hashbBytes);
                selectedUser.salt = Convert.ToBase64String(saltBytes);

                _dbContext.SaveChanges();

                return result;
            }

            return null;
        }

        public bool CheckLogin(string username, string password)
        {
            BE_User user = GetUserByUsername(username);

            if (UserExist(username))
            {
                byte[] salt = Convert.FromBase64String(user.Salt);
                byte[] hash = Convert.FromBase64String(user.Password);

                return PWDTK.ComparePasswordToHash(salt, password, hash, Iteration);
            }
            return false;
        }

        public bool UserExist(string user)
        {
            bool userExists = false;

            List<User> usr = _dbContext.Users.ToList();
            List<BE_User> userss = new List<BE_User>();

            foreach (var users in usr)
            {
                BE_User u = new BE_User(users.userid, users.username, users.salt, users.password, users.secret);
                userss.Add(u);
            }

            var selectedUser = (from u in userss
                                where u.Name == user
                                select u).SingleOrDefault();

            if (selectedUser != null)
            {
                userExists = true;
            }

            return userExists;
        }

        public bool UpdateCard(BE_Card card, string set)
        {
            bool cardUpdated = false;

            List<Card> cards = _dbContext.Cards.ToList();

            var selectedCard = (from c in cards
                                where c.name == card.Name
                                select c).FirstOrDefault();

            if (selectedCard != null)
            {
                try
                {
                    selectedCard.set = set;

                    _dbContext.SaveChanges();

                    cardUpdated = true;
                }
                catch
                {
                    cardUpdated = false;
                }

                return cardUpdated;
            }

            return cardUpdated;
        }
    }
}
