using LibraryManagementSystem.Helper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementSystem.Helper
{
    public class SecurityHelper
    {

        public static Guid GenerateSalt()
        {
            return Guid.NewGuid();
        }
        public static string HashPassword(string password, string salt)
        {
            var sha256 = SHA256.Create();
            var combined = password + salt;

            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}


// use to password hash and salt to database

//var members = _context.Members.ToList();

//foreach (var member in members)
//{
//    var salt = SecurityHelper.GenerateSalt();
//    var hash = SecurityHelper.HashPassword("Password@123", salt.ToString());


//    member.PasswordHash = hash;
//    member.Salt = salt;

//}

//_context.SaveChanges();
