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
        Rijndael cryptograph
        {
            get
            {                
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("ufh4A73yt94ng945", new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c });
                Rijndael rijndael = Rijndael.Create();
                rijndael.Key = pdb.GetBytes(32);
                rijndael.IV = pdb.GetBytes(16);

                return rijndael;
            }
        }

        byte[] BaseCrypt(ICryptoTransform ict, byte[] plain)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (var cryptoStream = new CryptoStream(memoryStream, ict, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plain, 0, plain.Length);
            }

            return memoryStream.ToArray();
        }

        byte[] Encrypt(byte[] plain)
        {
            return BaseCrypt(cryptograph.CreateEncryptor(), plain);
        }

        byte[] Decrypt(byte[] plain)
        {
            return BaseCrypt(cryptograph.CreateDecryptor(), plain);
        }

        public string Encode(string message)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(message);
            byte[] enccryptedBytes = Encrypt(byteArray);
            char[] charEncryptedArray = Array.ConvertAll(enccryptedBytes, x => Convert.ToChar(x));
            return new string(charEncryptedArray);
        }

        public string Decode(string message)
        {
            char[] charDecryptedArray = message.ToCharArray();
            byte[] decryptedBytes = Array.ConvertAll(charDecryptedArray, x => Convert.ToByte(x));
            byte[] sourceBytes = Decrypt(decryptedBytes);

            return new string(Encoding.UTF8.GetChars(sourceBytes));
        }       
    }
}
