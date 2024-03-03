using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BE_User
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BE_User" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="password">The password.</param>
        /// <param name="secret">The secret.</param>
        public BE_User(int id, string name, string salt, string password, string secret)
        {
            Id = id;
            Name = name.Trim();
            Salt = salt.Trim();
            Password = password.Trim();
            Secret = secret.Trim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BE_User" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="password">The password.</param>
        /// <param name="secret">The secret.</param>
        public BE_User(string name, string salt, string password, string secret)
        {
            Name = name.Trim();
            Salt = salt.Trim();
            Password = password.Trim();
            Secret = secret.Trim();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" }, is equal to this instance.
        /// </summary>
        /// <param name="o">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object o)
        {
            if (ReferenceEquals(this, o)) return true;
            if (o.GetType() != typeof(BE_User)) return false;

            var other = o as BE_User;

            return other != null && (other.Name == Name
                                     && other.Salt == Salt
                                     && other.Password == Password);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            int hash = 3;

            hash = 17 * hash +
                   (string.IsNullOrWhiteSpace(Name) ? 0 : Name.GetHashCode());
            hash = 16 * hash +
                   (string.IsNullOrWhiteSpace(Salt) ? 0 : Salt.GetHashCode());
            hash = 15 * hash +
                   (string.IsNullOrWhiteSpace(Password) ? 0 : Password.GetHashCode());
            hash = 14 * hash +
                   (string.IsNullOrWhiteSpace(Secret) ? 0 : Secret.GetHashCode());

            return hash;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Object is not a User</exception>
        public int CompareTo(object o)
        {
            if (o is BE_User)
                return Name.CompareTo((o as BE_User).Name);  // compare Card names

            throw new ArgumentException("Object is not a User");
        }
    }
}
