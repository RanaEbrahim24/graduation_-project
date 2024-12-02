using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using train.Models;
using train.Services;

namespace train.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OtpService _otpService;

        public AuthController(OtpService otpService)
        {
            _otpService = otpService;
        }

        // Endpoint لطلب OTP
        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] OTPRequest otpRequest)
        {
            if (string.IsNullOrWhiteSpace(otpRequest?.Email))
                return BadRequest("Email is required.");

            // Generate OTP
            string otp = _otpService.GenerateOtp();

            // Save OTP to database
            await _otpService.SaveOtpToDatabase(otpRequest.Email, otp);

            // Send OTP via email
            _otpService.SendOtpEmail(otpRequest.Email, otp);

            return Ok(new { Message = "OTP sent to email." });
        }

        // Endpoint للتحقق من OTP
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OTPVerification otpVerification)
        {
            if (string.IsNullOrWhiteSpace(otpVerification?.Email) || string.IsNullOrWhiteSpace(otpVerification?.OTP))
                return BadRequest("Email and OTP are required.");

            // Validate OTP
            bool isValid = await _otpService.ValidateOtp(otpVerification.Email, otpVerification.OTP);

            if (!isValid)
                return Unauthorized("Invalid or expired OTP.");

            return Ok(new { Message = "OTP verified successfully." });
        }
    }
}



