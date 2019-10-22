using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Utility
{
    public class Helper
    {
        private readonly ApplicationDbContext _dbContext;
        public Helper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static string SendEmailVerification(string Message, string EmailSubject,string username, IHostingEnvironment hostingEnvironment)
        {
            try
            {
              var  pathToFile = hostingEnvironment.ContentRootPath
                           + Path.DirectorySeparatorChar.ToString()
                           + "HtmlPages"
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailVerification.html";
                string body = string.Empty;
                using (StreamReader reader = File.OpenText(pathToFile))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{EmailSubject}", EmailSubject);
                body = body.Replace("{Message}", Message);
                body = body.Replace("{username}", username);

                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetPicture(string userName)
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            var u = new ApplicationDbContext(options).Users.Where(a => a.UserName == userName).FirstOrDefault();

            if (u == null)
            {
                return "";
            }

            var profilepic = u.ProfilePic;
            return profilepic;
        }
        public static string GetUserFullName(string userName)
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            var u = new ApplicationDbContext(options).Users.Where(a => a.UserName == userName).FirstOrDefault();

            if (u == null)
            {
                return "";
            }

            var fullname = u.FirstName + " " + u.LastName;
            return fullname;
        }
        public static string GetUserRole(string userName)
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            var u = new ApplicationDbContext(options).Users.Where(a => a.UserName == userName).FirstOrDefault();

            if (u == null)
            {
                return "";
            }

            var fullname = u.FirstName + " " + u.LastName;
            return fullname;
        }
        public static IList<ImageSlider> GetAll()
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            var allMenuItem = new ApplicationDbContext(options).ImageSliders.ToList();
            
            return allMenuItem;
        }
    }
}
