using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Evention2.Services
{
    public class HashDigestServices
    {
        public static string GetMD5(byte[] src)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] hashValue = md5Hash.ComputeHash(src);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashValue.Length; i++)
                    stringBuilder.Append(hashValue[i].ToString("x2"));  // ToString(string format)
                return stringBuilder.ToString();
            }
        }
    }
}