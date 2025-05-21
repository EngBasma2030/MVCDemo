using System.Net;
using System.Net.Mail;

namespace Demo.PL.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("basmabahaa610@gmail.com", "tolaoyibzpbhsrit");
            client.Send("basmabahaa610@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
