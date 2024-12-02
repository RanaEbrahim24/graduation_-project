using System;
using System.ComponentModel.DataAnnotations;

namespace train.Models
{
    public class OtpRecord
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "OTP is required.")]
        public string OTP { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }
    }
}




