namespace train.Models
{
    public class SmtpSettings
    {
        public string Host { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; } = 587; // تأكد من أن البورت مضبوط
        public bool EnableSsl { get; set; } = true; // تأكد من تمكين SSL
    }
}

