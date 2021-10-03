using DotNetAssignment1_31927.Interface;
using DotNetAssignment1_31927.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927.Repository
{
    public class AccountActionRepository : IAccountActionRepository
    {


        public int CreateAccount(AccountModel accountModel)
        {

            bool exists = true;
            int rInt;
            string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\";

            //Checks if the account exists, if not, creates an account
            while (exists == true)
            {
                Random r = new Random();
                rInt = r.Next(100000, 99999999);
                fileName = fileName + rInt + ".txt";
                if (!File.Exists(fileName))
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        //Write passed model

                        sw.WriteLine("First Name|{0}", accountModel.FirstName);
                        sw.WriteLine("Last Name|{0}", accountModel.LastName);
                        sw.WriteLine("Address|{0}", accountModel.Address);
                        sw.WriteLine("Phone|{0}", accountModel.Phone);
                        sw.WriteLine("Email|{0}", accountModel.Email);
                        sw.WriteLine("AccountNo|{0}", rInt);
                        sw.WriteLine("Balance|{0}", accountModel.Amount);

                    }
                    return rInt; //returns the account number
                }
            }
            return -1; // Exited with no account created
        }


        public bool SaveAccount(AccountModel accountModel)
        {
            string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\" + accountModel.AccountNumber + ".txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    //Write passed model

                    sw.WriteLine("First Name|{0}", accountModel.FirstName);
                    sw.WriteLine("Last Name|{0}", accountModel.LastName);
                    sw.WriteLine("Address|{0}", accountModel.Address);
                    sw.WriteLine("Phone|{0}", accountModel.Phone);
                    sw.WriteLine("Email|{0}", accountModel.Email);
                    sw.WriteLine("AccountNo|{0}", accountModel.AccountNumber);
                    sw.WriteLine("Balance|{0}", accountModel.Amount);

                }
                return true; //Account updating
            }
            return false; // Account not found 
        }

        public bool EmailStatement(AccountModel accountModel)
        {


            MailMessage mail = new MailMessage("akaskaniotis@gmail.com", accountModel.Email);   // From,  To
                                                                                                //mail.Dispose();

            SmtpClient client = new SmtpClient();

            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Host = "smtp.google.com";
            mail.Subject = accountModel.AccountNumber;

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("C:\\Users\\Akask\\source\\repos\\DotNetAssignment1_31927\\Accounts\\" + accountModel.AccountNumber + ".txt");
            mail.Attachments.Add(attachment);

            // Set the read file as the body of the message
            mail.Body = accountModel.AccountNumber;

            client.Send(mail);

            return true;

        }

        public AccountModel FindAccount(string accountNumber)
        {

            string[] files = Directory.GetFiles(@"C:/Users/Akask/source/repos/DotNetAssignment1_31927/Accounts/", "*.txt");

            AccountModel accountModel = new AccountModel();

            foreach (var file in files)
            {
                string dir = "C:/Users/Akask/source/repos/DotNetAssignment1_31927/Accounts/" + accountNumber + ".txt";
                if (file == dir)
                {

                    accountModel.AccountNumber = accountNumber;

                    StreamReader sr = new StreamReader(dir);
                    //Read the first line of text

                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        string[] value = line.Split('|');

                        switch (value[0])
                        {
                            case "AccountNo":
                                accountModel.AccountNumber = value[1];
                                break;
                            case "First Name":
                                accountModel.FirstName = value[1];
                                break;
                            case "Last Name":
                                accountModel.LastName = value[1];
                                break;
                            case "Address":
                                accountModel.Address = value[1];
                                break;
                            case "Phone":
                                accountModel.Phone = int.Parse(value[1]);
                                break;
                            case "Email":
                                accountModel.Email = value[1];
                                break;
                            case "Balance":
                                accountModel.Amount = int.Parse(value[1]);
                                break;
                            default:
                                break;
                        }
                        line = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                    accountModel.AccountNumber = accountNumber;
                    return accountModel;
                }
            }

            return null;
        }


        public bool DeleteAccount(string accountNumber)
        {
            string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\" + accountNumber + ".txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true; //Account deleted
            }
            return false; // Account not found 
        }




    }
}
