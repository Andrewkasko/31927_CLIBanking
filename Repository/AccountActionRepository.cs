using DotNetAssignment1_31927.Interface;
using DotNetAssignment1_31927.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Accounts\";

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
                        sw.WriteLine("Last transaction 1|{0}", accountModel.Transaction1);
                        sw.WriteLine("Last transaction 2|{0}", accountModel.Transaction2);
                        sw.WriteLine("Last transaction 3|{0}", accountModel.Transaction3);
                        sw.WriteLine("Last transaction 4|{0}", accountModel.Transaction4);
                        sw.WriteLine("Last transaction 5|{0}", accountModel.Transaction5);
                        sw.Close();

                    }
                    return rInt; //returns the account number
                }
            }
            return -1; // Exited with no account created
        }

        public bool SaveAccount(AccountModel accountModel)
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Accounts\" + accountModel.AccountNumber + ".txt";
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
                    sw.WriteLine("Last transaction 1|{0}", accountModel.Transaction1);
                    sw.WriteLine("Last transaction 2|{0}", accountModel.Transaction2);
                    sw.WriteLine("Last transaction 3|{0}", accountModel.Transaction3);
                    sw.WriteLine("Last transaction 4|{0}", accountModel.Transaction4);
                    sw.WriteLine("Last transaction 5|{0}", accountModel.Transaction5);
                }
                return true; //Account updating
            }
            return false; // Account not found 
        }

        public bool EmailStatement(AccountModel accountModel)
        {


            //Sent from my mailbox to the account
            MailMessage mail = new MailMessage("akaskaniotis@gmail.com", accountModel.Email);   // From,  To//mail.Dispose();
            System.Net.Mail.Attachment attachment;
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    //Mail Settings
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"\Accounts\" + accountModel.AccountNumber + ".txt";
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = true;
                    client.Host = "smtp.google.com";
                    mail.Subject = accountModel.AccountNumber;

                    // Set the read file as the body of the message
                    mail.Body = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Accounts\" + accountModel.AccountNumber + ".txt"); ;

                    //client.Send(mail);
                    try
                    {
                        client.Send(mail);
                    }
                    catch (Exception e)
                    {
                        //  Console.WriteLine("Exception: {0}", e);
                    }
                }
            }
            finally
            {
                mail.Attachments.Dispose();
                mail.Dispose();
            }
            return true;
        }

        public AccountModel FindAccount(string accountNumber)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Accounts\";
            string[] files = Directory.GetFiles(path, "*.txt");

            AccountModel accountModel = new AccountModel();

            //less than 10 and greater than 6 check, to ensure that the account number that is provided is in the realm of possibility
            if (accountNumber.Length > 10 || accountNumber.Length < 6) {
                return null; //This will return an object with the value of null
            }

            foreach (var file in files)
            {
                string dir = path + accountNumber + ".txt";
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
                            case "Last transaction 1":
                                accountModel.Transaction1 = value[1];
                                break;
                            case "Last transaction 2":
                                accountModel.Transaction2 = value[1];
                                break;
                            case "Last transaction 3":
                                accountModel.Transaction3 = value[1];
                                break;
                            case "Last transaction 4":
                                accountModel.Transaction4 = value[1];
                                break;
                            case "Last transaction 5":
                                accountModel.Transaction5 = value[1];
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
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Accounts\" + accountNumber + ".txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true; //Account deleted
            }
            return false; // Account not found 
        }


        public AccountModel addTransactionDetails(AccountModel account, string transactionDetails)
        {
            //Makes sure the most up to date transactions are displayed

            if (account.Transaction1 == null)
            {
                account.Transaction1 = transactionDetails;
            }
            else if (account.Transaction2 == null)
            {
                account.Transaction2 = transactionDetails;
            }
            else if (account.Transaction3 == null)
            {
                account.Transaction3 = transactionDetails;
            }
            else if (account.Transaction4 == null)
            {
                account.Transaction4 = transactionDetails;
            }
            else if (account.Transaction5 == null)
            {
                account.Transaction5 = transactionDetails;
            }
            else
            {
                //Shuffles transactions if five already exist
                account.Transaction5 = account.Transaction4;
                account.Transaction4 = account.Transaction3;
                account.Transaction3 = account.Transaction2;
                account.Transaction2 = account.Transaction1;
                account.Transaction1 = transactionDetails;
            }
            return account;
        }


    }
}
