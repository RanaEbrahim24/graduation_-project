using System.ComponentModel.DataAnnotations;

namespace train.Models
{
    public class OTPRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
    }

   


        public class OTPVerification
        {
            [Required(ErrorMessage = "OTP is required.")]
            public string OTP { get; set; } = string.Empty;

            
        }
    }




