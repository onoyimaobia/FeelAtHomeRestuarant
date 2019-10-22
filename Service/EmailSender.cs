using FeelAtHomeRestaurant.Rpository;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service
{
    public class EmailSender : Interface.IEmailSender
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EmailSender(IEmailSenderService emailSenderService, IHostingEnvironment hostingEnvironment)
        {
            _emailSenderService = emailSenderService;
            _hostingEnvironment = hostingEnvironment;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage, string username)
        {
            return Execute(email, subject, htmlMessage,username);
        }
        //public Task SendForgotPasswordAsync(string email, string subject, string htmlMessage, string username)
        //{
        //    return ExecuteForgotPassword(email, subject, htmlMessage,username);
        //}
        public Task Execute(string email, string subject, string message, string username)
        {
            
            var EmailSenderDetail = _emailSenderService.GetEmailSender("toochinkuzi@gmail.com");
            SmtpClient client = new SmtpClient(EmailSenderDetail.SMTPHost.Trim(), EmailSenderDetail.SMTPPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(EmailSenderDetail.SenderEmail.Trim(), EmailSenderDetail.SenderPassword.Trim());
            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailSenderDetail.SenderEmail, "no-reply");
            mail.To .Add(email);
            mail.Body = Helper.SendEmailVerification(message, subject, username, _hostingEnvironment);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            

             return client.SendMailAsync(mail); ;
        }
        //public Task ExecuteForgotPassword(string email, string subject, string message,string username)
        //{
        //    var encodeLink = HtmlEncoder.Default.Encode(message);
        //    var EmailSenderDetail = _emailSenderService.GetEmailSender("toochinkuzi@gmail.com");
        //    SmtpClient client = new SmtpClient(EmailSenderDetail.SMTPHost, EmailSenderDetail.SMTPPort);
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new NetworkCredential(EmailSenderDetail.SenderEmail, EmailSenderDetail.SenderPassword);
        //    client.EnableSsl = true;
        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress(EmailSenderDetail.SenderEmail);
        //    mail.To.Add(email);
        //    mail.Body = Helper.SendPasswordReset(message, subject,);
        //    mail.IsBodyHtml = true;
        //    mail.Subject = subject;


        //    return client.SendMailAsync(mail); ;
        //}
    }
}
