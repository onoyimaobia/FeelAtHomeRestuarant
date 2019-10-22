using FeelAtHomeRestaurant.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Extensions
{
    public static class EmailSenderExtension
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string callbarckUrl, string username)
        {
            //var callbarckUrl = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(link)}'>clicking here</a>.";
            return emailSender.SendEmailAsync(email, "Confirm your email", callbarckUrl,username);
        }

        //public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string callbackUrl, string username)
        //{
        //    return emailSender.SendForgotPasswordAsync(email, "Reset Password",
        //       callbackUrl, username);
        //}
    }
}
