using DotNetAssignment1_31927.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        //Authentication Method 
        public bool CheckPassword(string username, string password)
        {

            List<(string, string)> credentialsFromFile = new List<(string, string)>();
            string line;
            string[] tempCredentials;

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Credentials\login.txt"); // Make dynamic
                //Read the first line of text
                line = sr.ReadLine();

                while (line != null)
                {
                    tempCredentials = line.Split('|');
                    credentialsFromFile.Add((tempCredentials[0], tempCredentials[1]));

                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            foreach ((string, string) credential in credentialsFromFile)
            {
                //Compares the password
                if (username == credential.Item1 && password == credential.Item2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
