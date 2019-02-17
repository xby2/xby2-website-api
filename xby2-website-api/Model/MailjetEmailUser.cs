namespace Xby2WebsiteAPI.Model
{
    public class MailjetEmailUser
    {
        public string Email { get; private set; }
        public string Name { get; private set; }

        public MailjetEmailUser(string email, string name = "")
        {
            Email = email;
            Name = name;
        }
    }
}
