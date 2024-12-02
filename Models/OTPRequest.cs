namespace train.Models
{
    public class OTPRequest
    {
        public string Email { get; set; }
    }

    public class OTPVerification
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}

