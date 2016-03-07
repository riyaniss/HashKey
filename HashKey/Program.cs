using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace HashKey
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateMachineKey();
        }

        private static void CreateMachineKey()
        { 
            string[] commandLineArgs = System.Environment.GetCommandLineArgs();

            if (commandLineArgs.Count() <= 1)
            {
                Console.WriteLine("Error no ha definido los parámetros");
                return;
            }

            //commandLineArgs[1].ToString()

            string decryptionKey = CreateKey(System.Convert.ToInt32(commandLineArgs[1].ToString()));
            string validationKey = CreateKey(System.Convert.ToInt32(commandLineArgs[2].ToString()));

            string format = string.Format("<machineKey validationKey='{0}' decryptionKey='{1}' validation=SHA1/>", validationKey, decryptionKey);
            format = format.Replace("'", ((char)34).ToString());
            Console.WriteLine(format);
            Console.ReadLine();
        }

        private static string CreateKey(int numBytes)
        { 
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes - 1];

            rng.GetBytes(buff);

            return BytesToHexString(buff);
        }

        private static string BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);
            
            for(int counter = 0; counter < bytes.Length - 1; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();  
        }

    }
}
