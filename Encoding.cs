using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FarmAppApi
{
    public class Encoding
    {
        public static string Hash(string password)
        {
            byte[] bytesData = System.Text.Encoding.ASCII.GetBytes(password);
            bytesData = new SHA256Managed().ComputeHash(bytesData);
            string hashedPassword = System.Text.Encoding.ASCII.GetString(bytesData);

            return hashedPassword;
        }

    }

}
