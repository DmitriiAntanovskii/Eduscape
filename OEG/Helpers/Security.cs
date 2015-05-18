using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OEG.Helpers

{
    public class Security
    {
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string CreateRandomPassword(int passwordLength) //usually 6
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rNum = new Random();
            string NewPassWord = "";
            for (int i = 0; i < passwordLength; i++)
            {
                NewPassWord += allowedChars[rNum.Next(allowedChars.Length)];
            }
            return NewPassWord;
        }
    }
}