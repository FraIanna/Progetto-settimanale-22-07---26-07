using System.Text;

namespace BusinessLayer.Implementation.PasswordEncoder
{
    public static class Helpers
    {
        public static string Repeat(this string source, int len)
        {
            var sb = new StringBuilder(source);
            while (sb.Length < len)
            {
                sb.Append(source);
            }
            return sb.ToString()[..len];
        }
    }

    internal class VigenerePasswordEncoder : IPasswordEncoder
    {
        private const string WORM = "PROVA";


        public string Encode(string password)
        {
            var worm = WORM.Repeat(password.Length);
            int index = 0;
            return string.Join("", password.ToUpper().Select(x => (x + worm[index++]) % 26 + 'A'));
        }

        public bool IsSame(string plainText, string codedText)
        {
            return Encode(plainText) == codedText;
        }
    }
}
