namespace BusinessLayer.Implementation.PasswordEncoder
{
    public class NoOpPasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {
            return password;
        }
        public bool IsSame(string plainText, string codedText)
        {
            return plainText == codedText;
        }
    }
}
