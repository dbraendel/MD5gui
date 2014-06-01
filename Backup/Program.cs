using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace NS_MD5
{
	// The main class.
    class MD5Check
    {
        private bool showShort; // show short form
        private string fileName;

        public MD5Check()
        {
            fileName = "";
            showShort = false; 
        }

        public bool checkParameters (ref string[] args)
        {
            if (args.Length >= 1)
            {
                foreach (string anArg in args)
                {
                    if (anArg == "/s") // show short form
                        showShort = true;

                    else if (anArg[0] != '/') // no '/' so is assumed a filename
                        fileName = anArg; 

                    else if (anArg == "/h") // show help then exit
                    {
                        showHelp ();
                        return false;
                    }
                }

                if (fileName == "") //ensure filename has been specified
                    return false;

                return true; // atleast file name exists as well as any other parameters
            }
            return false; // not enough parameters
        }

        public void showHelp()
        {
            Console.WriteLine("MD5dos fileName [/s] [/h]");
            Console.WriteLine("/s = short MD5 list");
            Console.WriteLine("/h = help");
        }

        public byte[] GetMD5HashFromFile()
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            return retVal;
        }
    };

    class Program
    {
        static void Main(string[] args)
        {
            byte[] md5Val;
            MD5Check md5Check = new MD5Check();

            if (md5Check.checkParameters(ref args))
            {
                md5Val = md5Check.GetMD5HashFromFile();
                Console.Write("MD5 ({0}) = ", args[0]);
                foreach (byte aVal in md5Val)
                {
                    Console.Write ("{0:x2}", aVal);
                }
                Console.WriteLine();
            }
        }

    }
}
