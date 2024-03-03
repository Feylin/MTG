using System.Collections.Generic;
using System.Windows.Forms;
using BusinessEntities;

namespace BusinessLogic
{
    public interface IRepository
    {
        /// <summary>
        /// BE_Card object array - Cards.
        /// </summary>
        /// <returns>BE_Card[].</returns>
        BE_Card[] Cards();

        /// <summary>
        /// String array - MagicCards
        /// </summary>
        /// <returns>System.String[].</returns>
        string[] MagicCards();

        /// <summary>
        /// Updates the card.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="rarity">The rarity.</param>
        /// <param name="set">The set.</param>
        /// <returns>
        ///   <c>true</c> if XXXX, <c>false</c> otherwise.
        /// </returns>
        bool UpdateCard(BE_Card card, string notes, string rarity, string set);

        /// <summary>
        /// Gets the card by cardname.
        /// </summary>
        /// <param name="cardName">Name of the card.</param>
        /// <returns>BE_Card.</returns>
        BE_Card GetCardFromCardname(string cardName);

        /// <summary>
        /// Returns a boosterpack containing random cards from the specificed set.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        List<BE_Card> Boosterpack(string set);

        /// <summary>
        /// Creates the mana curve for deck.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <returns>System.Int32[].</returns>
        int[] CreateManaCurveForDeck(IEnumerable<string> deck);

        /// <summary>
        /// Returns a random card from the deck.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <param name="deckSize">Size of the deck.</param>
        /// <returns>System.String.</returns>
        string RandomCard(string[] deck, int deckSize);

        /// <summary>
        /// Randoms the specified minimum.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        int Random(int min, int max);

        /// <summary>
        /// Shuffles the specified deck.
        /// </summary>
        /// <param name="deck">The deck.</param>
        void Shuffle(string[] deck);

        /// <summary>
        /// Calculates the lands.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="totalSymbols">The total symbols.</param>
        /// <param name="totalLands">The total lands.</param>
        /// <returns>System.String.</returns>
        string CalculateLands(string color, int totalSymbols, int totalLands);

        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <returns></returns>
        bool CheckConnection(string hostName);

        /// <summary>
        /// Pings the network.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns></returns>
        bool PingNetwork(string hostNameOrAddress);

        /// <summary>
        /// Cardnotes.
        /// </summary>
        /// <returns>Collections.Generic.Dictionary</returns>
        Dictionary<string, string> CardNotes();

        /// <summary>
        /// Highlights the words with color, bold and italic.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        void HighlightWords(RichTextBox textBox);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <param name="secret">The secret.</param>
        /// <returns></returns>
        bool AddUser(string name, string password, string secret);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        BE_User GetUserByUsername(string user);

        /// <summary>
        /// Returns a temporary password string.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="secret">The secret.</param>
        /// <returns></returns>
        string ForgotLogin(string user, string secret);

        /// <summary>
        /// Checks the login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool CheckLogin(string username, string password);

        /// <summary>
        /// Returns true if the user exists, otherwise false.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        bool UserExist(string user);
    }
}
