using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using train.Data;
using train.Models;

namespace train.Services
{
    public class OtpService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public OtpService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Generate OTP
        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Send OTP via Email
        public void SendOtpEmail(string email, string otp)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            using (var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port))
            {
                client.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
                client.EnableSsl = smtpSettings.EnableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings.UserName),
                    Subject = "Your OTP Code",
                    Body = $"Your OTP code is: {otp}",
                    IsBodyHtml = true,
                };

                // إرسال الرسالة فقط مع الـ OTP
                mailMessage.To.Add(email);

                try
                {
                    client.Send(mailMessage);
                }
                catch (SmtpException ex)
                {
                    // Capture the exception and log it or return an error response
                    Console.WriteLine($"SMTP Error: {ex.Message}");
                }
            }
        }

        // Save OTP to Database
        public async Task SaveOtpToDatabase(string email, string otp)
        {
            var otpRecord = new OtpRecord
            {
                Email = email,
                OTP = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5)
            };

            _context.OtpRecords.Add(otpRecord);
            await _context.SaveChangesAsync();
        }

        // Validate OTP
        public async Task<bool> ValidateOtp(string otp)
        {
            var otpRecord = _context.OtpRecords
                .Where(r => r.OTP == otp)
                .FirstOrDefault();

            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
            {
                return false; // Invalid or expired OTP
            }

            // Optionally, remove OTP after validation
            _context.OtpRecords.Remove(otpRecord);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}



