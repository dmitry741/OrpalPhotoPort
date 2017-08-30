using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrpalPhotoPortUtils.Base;
using System.IO;
using System.Security.Cryptography;

namespace OrpalPhotoPortUtils
{
    public class Cryptograph : ICryptograph
    {
        byte[] Encrypt(byte[] plain, string password)
        {
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c });
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);

            MemoryStream memoryStream = new MemoryStream();

            using (var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(plain, 0, plain.Length);
            }

            return memoryStream.ToArray();
        }

        byte[] Decrypt(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c });
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public string Encode(string message)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(message);
            byte[] enccryptedBytes = Encrypt(byteArray, "ufh4873yt94ng945");
            char[] charEncryptedArray = Array.ConvertAll(enccryptedBytes, x => Convert.ToChar(x));
            return new string(charEncryptedArray);
        }

        public string Decode(string message)
        {
            char[] chardecryptedArray = message.ToCharArray();
            byte[] decryptedBytes = Array.ConvertAll(chardecryptedArray, x => Convert.ToByte(x));
            byte[] sourceBytes = Decrypt(decryptedBytes, "ufh4873yt94ng945");

            return new string(Encoding.UTF8.GetChars(sourceBytes));
        }       
    }
}
