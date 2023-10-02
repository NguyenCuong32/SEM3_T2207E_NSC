// See https://aka.ms/new-console-template for more information
using SEM3.NSC.Crypographic.S1_RSA_Example;

Console.WriteLine("Hello, World!");

//RSAExample1 example1 = new RSAExample1();
//example1.EncryptAndDeencrypt();

//AES_Example1 example2 = new AES_Example1();
//example2.AES_EncryptDecrypt();
AES_Example_Class example_Class = new AES_Example_Class();
example_Class.AES_EncryptDeEncrypr();
Console.ReadKey();