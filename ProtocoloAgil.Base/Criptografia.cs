using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ProtocoloAgil.Base
{
    public class Criptografia
    {
        private static byte[] _chave = {};
        private static readonly byte[] Iv = {12, 34, 56, 78, 90, 102, 114, 126};

        public static string Encrypt(string valor, string chaveCriptografia)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs;
            byte[] input;
            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = Encoding.UTF8.GetBytes(valor);
                _chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));
                cs = new CryptoStream(ms, des.CreateEncryptor(_chave, Iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt(string valor, string chaveCriptografia)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs;
            byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = new byte[valor.Length];
                input = Convert.FromBase64String(valor.Replace(" ", "+"));

                _chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateDecryptor(_chave, Iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}