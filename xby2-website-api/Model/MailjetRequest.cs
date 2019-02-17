using Newtonsoft.Json;

namespace Xby2WebsiteAPI.Model
{
    public class MailjetRequest
    {
        public MailjetMessage[] Messages { get; private set; }

        public MailjetRequest(MailjetMessage message)
        {
            Messages = new MailjetMessage[] { message };
        }

        public MailjetRequest(MailjetMessage[] messages)
        {
            Messages = messages;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
