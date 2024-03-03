using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BE_Card : IComparable, IComparer<BE_Card>
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public int? Cmc { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }

        private static CardSortOptions _sortOption;

        /// <summary>
        /// Enum CardSortOptions
        /// </summary>
        public enum CardSortOptions
        {
            Color, Power, Toughness, Type, Cmc, Rarity, Set
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BE_Card"/> class.
        /// </summary>
        /// <param name="sortOption">The sort option.</param>
        public BE_Card(CardSortOptions sortOption)
        {
            _sortOption = sortOption;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BE_Card" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        /// <param name="power">The power.</param>
        /// <param name="toughness">The toughness.</param>
        /// <param name="cmc">The CMC.</param>
        /// <param name="image">The image.</param>
        /// <param name="type">The type.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="rarity">The rarity.</param>
        /// <param name="set">The set.</param>
        public BE_Card(int id, string name, string color, int? power, int? toughness, int? cmc, string image, string type, string notes, string rarity, string set)
        {
            Id = id;
            Name = name.Trim();
            Color = color.Trim();
            Power = power;
            Toughness = toughness;
            Cmc = cmc;
            Image = image;
            Type = type.Trim();
            if (!string.IsNullOrWhiteSpace(notes))
                Notes = notes.Trim();
            Rarity = rarity.Trim();
            Set = set.Trim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BE_Card" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        /// <param name="power">The power.</param>
        /// <param name="toughness">The toughness.</param>
        /// <param name="cmc">The CMC.</param>
        /// <param name="image">The image.</param>
        /// <param name="type">The type.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="rarity">The rarity.</param>
        /// <param name="set">The set.</param>
        public BE_Card(string name, string color, int? power, int? toughness, int? cmc, string image, string type, string notes, string rarity, string set)
        {
            Name = name.Trim();
            Color = color.Trim();
            Power = power;
            Toughness = toughness;
            Cmc = cmc;
            Image = image.Trim();
            Type = type.Trim();
            if (!string.IsNullOrWhiteSpace(notes))
                Notes = notes.Trim();
            Rarity = rarity.Trim();
            Set = set.Trim();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="o">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object o)
        {
            if (ReferenceEquals(this, o)) return true;
            if (o.GetType() != typeof(BE_Card)) return false;

            var other = o as BE_Card;

            return other != null && (other.Name == Name
                                     && other.Color == Color
                                     && other.Power == Power
                                     && other.Toughness == Toughness
                                     && other.Cmc == Cmc
                                     && other.Image == Image
                                     && other.Type == Type
                                     && other.Notes == Notes
                                     && other.Rarity == Rarity
                                     && other.Set == Set);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            int hash = 4;

            hash = 19 * hash +
                   (string.IsNullOrWhiteSpace(Name) ? 0 : Name.GetHashCode());
            hash = 18 * hash +
                   (string.IsNullOrWhiteSpace(Color) ? 0 : Color.GetHashCode());
            hash = 17 * hash +
                   (null == Power ? 0 : Power.GetHashCode());
            hash = 16 * hash +
                   (null == Toughness ? 0 : Toughness.GetHashCode());
            hash = 15 * hash +
                   (null == Cmc ? 0 : Cmc.GetHashCode());
            hash = 14 * hash +
                   (string.IsNullOrWhiteSpace(Image) ? 0 : Image.GetHashCode());
            hash = 13 * hash +
                   (string.IsNullOrWhiteSpace(Type) ? 0 : Type.GetHashCode());
            hash = 12*hash +
                   (string.IsNullOrWhiteSpace(Notes) ? 0 : Notes.GetHashCode());
            hash = 11 * hash +
                   (string.IsNullOrWhiteSpace(Rarity) ? 0 : Rarity.GetHashCode());
            hash = 10 * hash +
                   (string.IsNullOrWhiteSpace(Set) ? 0 : Set.GetHashCode());

            return hash;
        }

        /// <summary>
        /// Compares the specified cards.
        /// </summary>
        /// <param name="cardOne">Card one.</param>
        /// <param name="cardTwo">Card two.</param>
        /// <returns>System.Int32.</returns>
        public int Compare(BE_Card cardOne, BE_Card cardTwo)
        {
            switch (_sortOption)
            {
                case CardSortOptions.Power:
                    if (cardOne.Power.GetValueOrDefault().Equals(null) && cardTwo.Power.GetValueOrDefault().Equals(null))
                        return -1;
                    return cardOne.Power.GetValueOrDefault().CompareTo(cardTwo.Power);

                case CardSortOptions.Toughness:
                    if (cardOne.Toughness.GetValueOrDefault().Equals(null) && cardTwo.Toughness.GetValueOrDefault().Equals(null))
                        return -1;
                    return cardOne.Toughness.GetValueOrDefault().CompareTo(cardTwo.Toughness);

                case CardSortOptions.Type:
                    return cardOne.Type.CompareTo(cardTwo.Type);

                case CardSortOptions.Cmc:
                    if (cardOne.Cmc.GetValueOrDefault().Equals(null) && cardTwo.Cmc.GetValueOrDefault().Equals(null))
                        return -1;
                    return cardOne.Cmc.GetValueOrDefault().CompareTo(cardTwo.Cmc);

                case CardSortOptions.Rarity:
                    return cardOne.Rarity.CompareTo(cardTwo.Rarity);

                case CardSortOptions.Set:
                    return cardOne.Set.CompareTo(cardTwo.Set);

                default:
                    return cardOne.Color.CompareTo(cardTwo.Color);
            }
        }

        // Implement IComparable
        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.ArgumentException">Object is not a Card</exception>
        public int CompareTo(object o)
        {
            if (o is BE_Card)
                return Name.CompareTo((o as BE_Card).Name);  // compare Card names

            throw new ArgumentException("Object is not a Card");
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (Name.Contains(" // "))
                return Name;
            return Name.Contains('/') ? Name.Remove(Name.IndexOf('/')) : Name;
        }
    }
}
