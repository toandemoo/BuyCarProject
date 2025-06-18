using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Project.Models;
using Project.Services.interfaces;
using MailKit.Security;

namespace Project.Services
{
   public class MailService : IMailService
   {
      private readonly MailSettings _mailSettings;

      private readonly ILogger<MailService> _log;

      public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> log)
      {
         _mailSettings = mailSettings.Value;
         _log = log;
         _log.LogInformation("Create SendMailService");
      }

      public async Task<Object> SendMail(MailRequest mailContents)
      {
         try
         {
            //MimeMessage - a class from Mimekit
            MimeMessage email_Message = new MimeMessage();
            MailboxAddress email_From = new MailboxAddress(_mailSettings.Name, _mailSettings.EmailId);
            MailboxAddress email_To = new MailboxAddress(mailContents.EmailName, mailContents.EmailTo);

            email_Message.From.Add(email_From);
            email_Message.To.Add(email_To);
            email_Message.Subject = mailContents.EmailSubject;

            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = mailContents.EmailBody;
            email_Message.Body = emailBodyBuilder.ToMessageBody();

            //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
            SmtpClient MailClient = new SmtpClient();
            MailClient.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            MailClient.Authenticate(_mailSettings.EmailId, _mailSettings.Password);
            await MailClient.SendAsync(email_Message);
            MailClient.Disconnect(true);
            MailClient.Dispose();
            return "OK";
         }
         catch (Exception ex)
         {
            _log.LogInformation("Lỗi gửi mail");
            _log.LogError(ex.Message);
            return "Bad Request";
         }
      }
   }
}