using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace InventoryManagementSystem.ModelClass
{
    class Password
    {
        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();

            return new SHA256Managed().ComputeHash(saltedValue);
        }

        public static byte[] CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return buff;
        }

        public static bool ConfirmPassword(string username, string password)
        {
            var context = new InventoryManagementSystem.InventoryDBDataSet();
            byte[] recordedPassword = context.Users.Where(u => u.emailAddress == username).Single().hashedPassword;
            byte[] salt = context.Users.Where(u => u.emailAddress == username).Single().passwordSalt;
            byte[] passwordHash = Hash(password, salt);
            byte[] recordedHash = recordedPassword;
            return recordedHash.SequenceEqual(passwordHash);
        }
    }
}
