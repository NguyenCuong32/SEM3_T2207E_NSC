using System;
using System.Security.Cryptography;
using System.Text;

namespace SEM3.NSC.Crypographic.S1_RSA_Example
{
	public class AES_Example1
	{
		public AES_Example1()
		{
		}
		public void AES_EncryptDecrypt()
		{
            try
            {
                string originalText = "Hello, AES!";

                // Generate a random encryption key and IV (Initialization Vector)
                byte[] encryptionKey = GenerateRandomKey();
                byte[] iv = GenerateRandomIV();

                // Encrypt the original text
                byte[] encryptedData = EncryptString(originalText, encryptionKey, iv);

                // Decrypt the encrypted data
                string decryptedText = DecryptBytes(encryptedData, encryptionKey, iv);

                Console.WriteLine("Original Text: " + originalText);
                Console.WriteLine("Decrypted Text: " + decryptedText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        // Generate a random encryption key (256 bits)
        static byte[] GenerateRandomKey()
        {
            using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
            {
                aesCrypto.GenerateKey();
                return aesCrypto.Key;
            }
        }

        // Generate a random IV (Initialization Vector) (128 bits)
        static byte[] GenerateRandomIV()
        {
            using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
            {
                aesCrypto.GenerateIV();
                return aesCrypto.IV;
            }
        }

        // Encrypt a string
        static byte[] EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
            {
                aesCrypto.Key = key;
                aesCrypto.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesCrypto.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();  
                    }
                }
            }
        }

        // Decrypt bytes to a string
        static string DecryptBytes(byte[] encryptedData, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
            {
                aesCrypto.Key = key;
                aesCrypto.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

