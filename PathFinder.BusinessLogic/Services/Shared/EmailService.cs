using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using PathFinder.SharedKernel.Enums;
using PathFinder.SharedKernel.Constants;
using SendGrid;
using SendGrid.Helpers.Mail;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.Core.Interface.Shared.IServices;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class EmailService : IEmailService
    {
        private IEmailSettingsService _SettingsService;
        public EmailService(IEmailSettingsService SettingsService)
        {
            _SettingsService = SettingsService;
        }
        public async Task SendEmailAsync(EmailDTO emailRequest)
        {
            try
            {
                var emailSettings = await _SettingsService.GetEmailSettingsAsync();
                switch (emailSettings.MailServer)
                {
                    case MailServer.SendGrid:
                        var client = new SendGridClient(emailSettings.Password.Trim());
                        var from = new EmailAddress(emailSettings.Email);
                        var to = new EmailAddress(emailRequest.ToEmail);
                        var msg = MailHelper.CreateSingleEmail(from, to, emailRequest.Subject, "", emailRequest.Body);
                        await client.SendEmailAsync(msg);
                        break;
                    default:
                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse(emailSettings.Email));
                        email.Sender = MailboxAddress.Parse(emailSettings.Email);
                        email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                        email.Subject = emailRequest.Subject;
                        var builder = new BodyBuilder();
                        if (emailRequest.Attachments != null)
                        {
                            byte[] fileBytes;
                            foreach (var file in emailRequest.Attachments)
                            {
                                if (file.Length > 0)
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        file.CopyTo(ms);
                                        fileBytes = ms.ToArray();
                                    }
                                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                                }
                            }
                        }
                        builder.HtmlBody = emailRequest.Body;
                        email.Body = builder.ToMessageBody();
                        var smtp = new SmtpClient();
                        smtp.Connect(emailSettings.ServerName, emailSettings.Port, (SecureSocketOptions)emailSettings.Encryption);
                        smtp.Authenticate(emailSettings.Email, emailSettings.Password.Trim());
                        await smtp.SendAsync(email);
                        smtp.Disconnect(true);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ExceptionConstants.EmailSettingIsWrong);
            }
        }
    }
}