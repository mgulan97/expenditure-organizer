using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OrganizerProject.Helper
{
    public class PasswordHelper
    {
       public String EncryptedPassword(string _data, string _hash)
       {
            string _result;
            byte[] data = UTF8Encoding.UTF8.GetBytes(_data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    _result = Convert.ToBase64String(results, 0, results.Length);
                    
                }
            }
            return _result;
       }

        public String DecryptedPassword(string _data, string _hash)
        {
            string _result;
            byte[] data = Convert.FromBase64String(_data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    _result = UTF8Encoding.UTF8.GetString(results);

                }
            }
            return _result;
        }

    }
}