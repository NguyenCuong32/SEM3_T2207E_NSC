using System;
using System.Security.Cryptography;
using System.Text;

namespace SEM3.NSC.Crypographic.S1_RSA_Example
{
	public class AES_Example_Class
	{
		public AES_Example_Class()
		{
		}
		public void AES_EncryptDeEncrypr()
		{
			string inputText = "Hello AES";
			byte[] encryptionKey = this.GennerateRandomKey();
			byte[] aesIV = this.GennetateIV();
			byte[] encrypted = Encryption(inputText, encryptionKey, aesIV);
			string decrypt = DeCryption(encrypted, encryptionKey, aesIV);
			Console.WriteLine("Dencrypt "+ decrypt);
		}
		private byte[] GennerateRandomKey()
		{
			using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
			{
				aesProvider.GenerateKey();
				return aesProvider.Key;
			}
		}
		private byte[] GennetateIV()
		{
			using (AesCryptoServiceProvider aesIv = new AesCryptoServiceProvider())
			{
				aesIv.GenerateIV();
				return aesIv.IV;
			}
		}
		private byte[] Encryption(string text, byte[]key, byte[] iv)
		{
			using (AesCryptoServiceProvider  encrtpt = new AesCryptoServiceProvider())
			{
				encrtpt.Key = key;
				encrtpt.IV = iv;
				using (MemoryStream stream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(stream,encrtpt.CreateEncryptor(),CryptoStreamMode.Write))
					{
						byte[] textStream = System.Text.Encoding.UTF8.GetBytes(text);
						cryptoStream.Write(textStream, 0, textStream.Length);
						cryptoStream.FlushFinalBlock();
						return stream.ToArray();
					}
				}
			}
		}
		private string DeCryption(byte[] encrypted, byte[] key, byte[] iv)
		{
			using (AesCryptoServiceProvider dencrypt = new AesCryptoServiceProvider())
			{
				dencrypt.Key = key;
				dencrypt.IV = iv;
				using (MemoryStream stream = new MemoryStream(encrypted))
				{
					using (CryptoStream decryptStream = new CryptoStream(stream,dencrypt.CreateDecryptor(),CryptoStreamMode.Read))
					{
                        using (StreamReader streamReader = new StreamReader(decryptStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
				}
			}
		}
	}
}

