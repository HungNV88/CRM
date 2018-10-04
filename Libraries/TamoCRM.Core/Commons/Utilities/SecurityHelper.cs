using System;
using System.Security.Cryptography;
using System.Text;

namespace TamoCRM.Core.Commons.Utilities
{
	public class SecurityHelper
	{
        public static string GetMD5Hash(string input)
        {
            var x = new MD5CryptoServiceProvider();
            var bs = Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            var s = new StringBuilder();
            foreach (var b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

	    public static string Encrypt(string toEncrypt)
	    {
	        const string key = "TPE";
	        var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

	        var hashmd5 = new MD5CryptoServiceProvider();
	        var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
	        hashmd5.Clear();

	        var tdes = new TripleDESCryptoServiceProvider
	                   {
	                       Key = keyArray,
	                       Mode = CipherMode.ECB,
	                       Padding = PaddingMode.PKCS7
	                   };

	        var cTransform = tdes.CreateEncryptor();
	        var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
	        tdes.Clear();
	        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
	    }

	    public static string Decrypt(string cipherString)
	    {
	        const string key = "TPE";
	        var toEncryptArray = Convert.FromBase64String(cipherString);

	        var hashmd5 = new MD5CryptoServiceProvider();
	        var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
	        hashmd5.Clear();

	        var tdes = new TripleDESCryptoServiceProvider
	                   {
	                       Key = keyArray,
	                       Mode = CipherMode.ECB,
	                       Padding = PaddingMode.PKCS7
	                   };

	        var cTransform = tdes.CreateDecryptor();
	        var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

	        tdes.Clear();
	        return Encoding.UTF8.GetString(resultArray);
	    }
	}
}
