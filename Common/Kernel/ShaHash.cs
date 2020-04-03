using System.Security.Cryptography;
using System.Text;

namespace Kernel
{
    public class ShaHash
    {
        public static string GetHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sringBuilder = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sringBuilder.Append(b.ToString("X2"));
                }

                return sringBuilder.ToString();
            }            
        }
    }
}
