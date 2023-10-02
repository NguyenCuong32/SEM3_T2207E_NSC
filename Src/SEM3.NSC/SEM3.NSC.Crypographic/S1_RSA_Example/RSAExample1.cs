using System;
using System.Security.Cryptography;
using System.Text;

namespace SEM3.NSC.Crypographic.S1_RSA_Example
{
	public class RSAExample1
	{
		public RSAExample1()
		{
		}
		public void EncryptAndDeencrypt()
		{
            try
            {
                // Create an instance of the RSACryptoServiceProvider class
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    // Generate public and private key pair
                    RSAParameters publicKey = rsa.ExportParameters(false); // Export public key
                    RSAParameters privateKey = rsa.ExportParameters(true); // Export private key
                    Console.WriteLine(ConvertPublicKeyToString(publicKey));
                    Console.WriteLine(ConvertPrivateKeyToString(privateKey));
                    // Convert a plaintext message to bytes
                    string plaintext = "Hello, RSA!";
                    byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

                    // Encrypt with the public key
                    byte[] encryptedData = EncryptWithPublicKey(plaintextBytes, publicKey);

                    // Decrypt with the private key
                    byte[] decryptedData = DecryptWithPrivateKey(encryptedData, privateKey);

                    // Convert decrypted bytes back to a string
                    string decryptedText = Encoding.UTF8.GetString(decryptedData);

                    Console.WriteLine("Original Message: " + plaintext);
                    Console.WriteLine("Decrypted Message: " + decryptedText);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        // Encrypt data using the public key
        static byte[] EncryptWithPublicKey(byte[] data, RSAParameters publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                return rsa.Encrypt(data, false);
            }
        }

        // Decrypt data using the private key
        static byte[] DecryptWithPrivateKey(byte[] data, RSAParameters privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                return rsa.Decrypt(data, false);
            }
        }
        // Convert RSA public key parameters to a string
        static string ConvertPublicKeyToString(RSAParameters publicKey)
        {
            // Convert each parameter to base64 and concatenate them
            StringBuilder publicKeyBuilder = new StringBuilder();

            publicKeyBuilder.AppendLine("Modulus: " + Convert.ToBase64String(publicKey.Modulus));
            publicKeyBuilder.AppendLine("Exponent: " + Convert.ToBase64String(publicKey.Exponent));

            return publicKeyBuilder.ToString();
        }
        // Convert RSA private key parameters to a string
        static string ConvertPrivateKeyToString(RSAParameters privateKey)
        {
            // Convert each parameter to base64 and concatenate them
            StringBuilder privateKeyBuilder = new StringBuilder();

            privateKeyBuilder.AppendLine("Modulus: " + Convert.ToBase64String(privateKey.Modulus));
            privateKeyBuilder.AppendLine("Exponent: " + Convert.ToBase64String(privateKey.Exponent));
            privateKeyBuilder.AppendLine("D: " + Convert.ToBase64String(privateKey.D));
            privateKeyBuilder.AppendLine("P: " + Convert.ToBase64String(privateKey.P));
            privateKeyBuilder.AppendLine("Q: " + Convert.ToBase64String(privateKey.Q));
            privateKeyBuilder.AppendLine("DP: " + Convert.ToBase64String(privateKey.DP));
            privateKeyBuilder.AppendLine("DQ: " + Convert.ToBase64String(privateKey.DQ));
            privateKeyBuilder.AppendLine("InverseQ: " + Convert.ToBase64String(privateKey.InverseQ));

            return privateKeyBuilder.ToString();
        }
    }
}

