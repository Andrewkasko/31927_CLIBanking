using DotNetAssignment1_31927.Interface;
using DotNetAssignment1_31927.Models;
using DotNetAssignment1_31927.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927
{
    public class UserInterface
    {

        AuthenticationRepository Auth = new AuthenticationRepository();
        AccountActionRepository Acc = new AccountActionRepository();

        protected static int origRow;
        protected static int origCol;
        string[] loginWindowFields = { "Username: ", "Password: " };

        int[,] loginFieldPos = new int[2, 2];
        string[] loginUserInputs = new string[2];

        string[] createNewAccountFields = { "First Name: ", "Last Name: ", "Address: ", "Phone: ", "Email: " };
        int[,] createNewAccountPos = new int[5, 5];
        string[] createNewAccountInputs = new string[5];

        string[] searchAccountFields = { "Account Number: " };
        int[,] searchAccountPos = new int[1, 1];
        string[] searchAccountInputs = new string[1];

        string[] depositWithdrawAccountFields = { "Amount: $" };
        int[,] depositWithdrawAccountPos = new int[1, 1];
        string[] depositWithdrawAccountInputs = new string[1];

        string[] findAccountFields = { "Account Number: " };
        int[,] findAccountPos = new int[1, 1];
        string[] findAccountInputs = new string[1];

        public bool DepositMoney(string accountNumber, int depositMoney)
        {
            string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\" + accountNumber + ".txt";
            if (File.Exists(fileName))
            {
                var account = Acc.FindAccount(accountNumber);
                account.Amount += depositMoney;
                return Acc.SaveAccount(account) ? true : false; // if account wasnt saved return false
            }
            return false; //Account not found
        }

        public bool WithdrawMoney(string accountNumber, int withdrawMoney)
        {
            string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\" + accountNumber + ".txt";
            if (File.Exists(fileName))
            {
                var account = Acc.FindAccount(accountNumber);

                if (account.Amount > withdrawMoney)
                {
                    account.Amount -= withdrawMoney;
                    Acc.SaveAccount(account);
                    return true;
                }
            }
            return false; //Account not found
        }

        //Method to create login screen
        public void LoginScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("Welcome to My Bank", startCol + 10, startRow + 1);
            WriteAt("Login To Start", startCol + 10, startRow + 4);

            int item = 0;
            foreach (string fieldName in loginWindowFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 6 + item);
                loginFieldPos[item, 1] = Console.CursorTop;
                loginFieldPos[item, 0] = Console.CursorLeft;
                item++;
            }

            do
            {
                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(loginFieldPos[field, 0], loginFieldPos[field, 1]);
                    loginUserInputs[field] = Console.ReadLine();
                }

                if (Auth.CheckPassword(loginUserInputs[0], loginUserInputs[1]))
                {
                    WriteAt("Valid Credentials!... Please press enter", startCol, noLines + 2);
                    // Console.ReadKey();
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        //Here is your enter key pressed!
                        MainScreen(13, 40, 2, 10);
                    }
                    break;
                }
                else
                {
                    WriteAt("Invalid Credentials", startCol, noLines + 2);
                }
            } while (true);
            Console.ReadKey();
        }

        public void HomeScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("Welcome to My Bank", startCol + 10, startRow + 1);
            WriteAt("Login To Start", startCol + 10, startRow + 4);
        }

        //MAIN SCREEN
        public void MainScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 10 | line == 12 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", startCol + 4, startRow + 1);
            WriteAt("1. Create a new account", startCol + 6, startRow + 3);
            WriteAt("2. Search for an account", startCol + 6, startRow + 4);
            WriteAt("3. Deposit", startCol + 6, startRow + 5);
            WriteAt("4. Withdraw", startCol + 6, startRow + 6);
            WriteAt("5. A/C statement", startCol + 6, startRow + 7);
            WriteAt("6. Delete account", startCol + 6, startRow + 8);
            WriteAt("7. Exit", startCol + 6, startRow + 9);
            WriteAt("Enter your choice (1-7): ", startCol + 4, startRow + 11);
            //Console.ReadKey();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    CreateNewAccountScreen(11, 40, 2, 10);
                    break;
                case ConsoleKey.D2:
                    SearchAccountScreen(7, 40, 2, 10);
                    break;
                case ConsoleKey.D3:
                    DepositScreen(8, 40, 2, 10);
                    break;
                case ConsoleKey.D4:
                    WithdrawScreen(8, 40, 2, 10);
                    break;
                case ConsoleKey.D5:
                    AccountStatementScreen(7, 40, 2, 10);
                    break;
                case ConsoleKey.D6:
                    DeleteAccountScreen(7, 40, 2, 10);
                    break;
                case ConsoleKey.D7:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                MainScreen(13, 40, 2, 10);
            }
        }

        public void CreateNewAccountScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 10 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("CREATE A NEW ACCOUNT", startCol + 4, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);

            int item = 0;
            foreach (string fieldName in createNewAccountFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                createNewAccountPos[item, 1] = Console.CursorTop;
                createNewAccountPos[item, 0] = Console.CursorLeft;
                item++;
            }

            do
            {
                int newAccountNumber;

                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(createNewAccountPos[field, 0], createNewAccountPos[field, 1]);
                    createNewAccountInputs[field] = Console.ReadLine();
                }

                if (createNewAccountInputs[0] != null && createNewAccountInputs[1] != null &&
                    createNewAccountInputs[2] != null && createNewAccountInputs[3] != null &&
                    createNewAccountInputs[4] != null)
                {
                    AccountModel account = new AccountModel();
                    account.FirstName = createNewAccountInputs[0];
                    account.LastName = createNewAccountInputs[1];
                    account.Address = createNewAccountInputs[2];
                    account.Phone = int.Parse(createNewAccountInputs[3]);
                    account.Email = createNewAccountInputs[4];
                    newAccountNumber = Acc.CreateAccount(account);
                }

            } while (true);
        }

        public void SearchAccountScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 7 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);

                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("SEARCH AN ACCOUNT", startCol + 4, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);

            int item = 0;
            foreach (string fieldName in searchAccountFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                searchAccountPos[item, 0] = Console.CursorLeft;
                item++;
            }

            do
            {
                AccountModel foundAccount;

                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(32, 7);
                    searchAccountInputs[field] = Console.ReadLine();
                }

                if (searchAccountInputs[0] != null)
                {
                    foundAccount = Acc.FindAccount(searchAccountInputs[0]);

                    if (foundAccount != null)
                    {
                        WriteAt("Account found!", startCol, startRow + 8);
                        PrintAccountDetails(13, 40, 1, -15, foundAccount);

                        WriteAt("Check another account (y/n)?", -15, noLines + 8);
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Y)
                        {
                            SearchAccountScreen(7, 40, 2, 10);
                        }
                        else if (keyInfo.Key == ConsoleKey.N)
                        {
                            MainScreen(13, 40, 2, 10);
                        }
                    }
                    else
                    {
                        WriteAt("Account not found!", startCol, startRow + 8);
                        WriteAt("Check another account (y/n)?", startCol, startRow + 9);
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Y)
                        {
                            SearchAccountScreen(7, 40, 2, 10);
                        }
                        else if (keyInfo.Key == ConsoleKey.N)
                        {
                            MainScreen(13, 40, 2, 10);
                        }
                    }
                }

            } while (true);
        }

        public void DepositScreen(int noLines, int formWidth, int startRow, int startCol)
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 7 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("DEPOSIT", startCol + 16, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);

            int item = 0;
            foreach (string fieldName in findAccountFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                findAccountPos[item, 0] = Console.CursorLeft;
                item++;
            }

            WriteAt("Amount: $", startCol + 6, startRow + 6);

            do
            {
                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(32, 7);
                    findAccountInputs[field] = Console.ReadLine();
                }

                if (Acc.FindAccount(findAccountInputs[0]) != null)
                {
                    WriteAt("Account found! Enter the amount...", startCol, noLines + 2);
                    Console.SetCursorPosition(25, 8);
                    string depositAmount = Console.ReadLine();
                    int depositAmountInt = int.Parse(depositAmount);
                    bool result = DepositMoney(findAccountInputs[0], depositAmountInt);

                    if (result)
                    {
                        WriteAt("Deposit Successful! Press Enter to go to the main menu.", startCol, noLines + 3);
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {

                            //Here is your enter key pressed!
                            MainScreen(13, 40, 2, 10);
                        }
                    }
                    else
                    {
                        WriteAt("Deposit Unsuccessful!", startCol, noLines + 3);
                    }

                }
                else
                {
                    WriteAt("Account not found!", startCol, noLines + 2);
                    WriteAt("Retry (y/n)?", startCol, noLines + 3);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        DepositScreen(8, 40, 2, 10);
                    }
                    else
                    {
                        MainScreen(13, 40, 2, 10);
                    }
                }
            } while (true);
        }

        public void WithdrawScreen(int noLines, int formWidth, int startRow, int startCol)
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 7 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);

                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("WITHDRAW", startCol + 16, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);

            int item = 0;
            foreach (string fieldName in findAccountFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                findAccountPos[item, 0] = Console.CursorLeft;
                item++;
            }

            WriteAt("Amount: $", startCol + 6, startRow + 6);

            do
            {
                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(32, 7);
                    findAccountInputs[field] = Console.ReadLine();
                }

                AccountModel accountModel = Acc.FindAccount(findAccountInputs[0]);

                if (accountModel != null)
                {
                    WriteAt("Account found! Enter the amount...", startCol, noLines + 2);
                    Console.SetCursorPosition(25, 8);
                    string withdrawAmount = Console.ReadLine();
                    int withdrawAmountInt = int.Parse(withdrawAmount);
                    if (accountModel.Amount > withdrawAmountInt)
                    {
                        if (WithdrawMoney(findAccountInputs[0], withdrawAmountInt))
                        {
                            WriteAt("Withdraw Successful! Press Enter to go to the main menu.", startCol, noLines + 3);
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                            if (keyInfo.Key == ConsoleKey.Enter)
                            {

                                //Here is your enter key pressed!
                                MainScreen(13, 40, 2, 10);
                            }
                        }
                        else
                        {
                            WriteAt("Withdraw Unsuccessful!", startCol, noLines + 3);
                        }
                    }
                    else
                    {
                        WriteAt("Amount less than balance", startCol, noLines + 2);
                        WriteAt("Retry (y/n)?", startCol, noLines + 3);
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Y)
                        {
                            WithdrawScreen(8, 40, 2, 10);
                        }
                        else
                        {
                            MainScreen(13, 40, 2, 10);
                        }
                    }
                }
                else
                {
                    WriteAt("Account not found!", startCol, noLines + 2);
                    WriteAt("Retry (y/n)?", startCol, noLines + 3);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        WithdrawScreen(8, 40, 2, 10);
                    }
                    else
                    {
                        MainScreen(13, 40, 2, 10);
                    }
                }
            } while (true);
        }

        public void AccountStatementScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 6 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);

                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("STATEMENT", startCol + 4, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);
            WriteAt("Account Number: ", startCol + 6, startRow + 5);

            int item = 0;
            foreach (string fieldName in searchAccountFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                searchAccountPos[item, 0] = Console.CursorLeft;
                item++;
            }

            do
            {
                AccountModel foundAccount;

                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(32, 7);
                    searchAccountInputs[field] = Console.ReadLine();
                }

                foundAccount = Acc.FindAccount(searchAccountInputs[0]);

                if (foundAccount != null)
                {
                    WriteAt("Account Found! The statement is displayed below... ", startCol, startRow + 8);
                    PrintAccountDetails(13, 40, 1, -50, foundAccount);

                    WriteAt("Email Statement (y/n)?", -50, noLines + 8);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        if (Acc.EmailStatement(foundAccount))
                        {
                            WriteAt("Email sent successfully!", -50, noLines + 10);
                            ConsoleKeyInfo keyInfo2 = Console.ReadKey(true);
                            if (keyInfo2.Key == ConsoleKey.Enter)
                            {
                                MainScreen(13, 40, 2, 10);
                                return;
                            }
                        }

                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        MainScreen(13, 40, 2, 10);
                    }
                }
                else
                {
                    WriteAt("Account not found! Search another account (y/n)?", startCol, startRow + 8);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        AccountStatementScreen(7, 40, 2, 10);

                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        MainScreen(13, 40, 2, 10);
                    }
                }
            } while (true);
        }

        public void PrintAccountDetails(int noLines, int formWidth, int startRow, int startCol, AccountModel account)
        {

            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("SIMPLE BANKING SYSTEM", startCol + 4, startRow + 1);
            WriteAt("Account Statement", startCol + 6, startRow + 3);
            WriteAt("Account No: " + account.AccountNumber, startCol + 6, startRow + 5);
            WriteAt("Account Balance: $" + account.Amount, startCol + 6, startRow + 6);
            WriteAt("First Name: " + account.FirstName, startCol + 6, startRow + 7);
            WriteAt("Last Name: " + account.LastName, startCol + 6, startRow + 8);
            WriteAt("Address: " + account.Address, startCol + 6, startRow + 9);
            WriteAt("Phone: " + account.Phone, startCol + 6, startRow + 10);
            WriteAt("Email: " + account.Email, startCol + 6, startRow + 11);
        }

        public void DeleteAccountScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for (int line = 0; line < noLines; line++)
            {
                if (line == 0 | line == 2 | line == 6 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("DELETE AN ACCOUNT", startCol + 4, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);
            WriteAt("Account Number: ", startCol + 6, startRow + 5);

            int item = 0;
            foreach (string fieldName in searchAccountFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                searchAccountPos[item, 0] = Console.CursorLeft;
                item++;
            }

            do
            {
                AccountModel foundAccount;
                //Get User inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(32, 7);
                    searchAccountInputs[field] = Console.ReadLine();
                }

                foundAccount = Acc.FindAccount(searchAccountInputs[0]);

                if (foundAccount != null)
                {
                    WriteAt("Account Found! The statement is displayed below... ", startCol, startRow + 8);
                    PrintAccountDetails(13, 40, 1, -50, foundAccount);

                    WriteAt("Delete (y/n)?", -50, noLines + 8);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        if (Acc.DeleteAccount(foundAccount.AccountNumber))
                        {
                            WriteAt("Account Deleted!... Press enter to go to the main menu.", -50, noLines + 10);
                            ConsoleKeyInfo keyInfo2 = Console.ReadKey(true);
                            if (keyInfo2.Key == ConsoleKey.Enter)
                            {
                                MainScreen(13, 40, 2, 10);
                                return;
                            }
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        DeleteAccountScreen(7, 40, 2, 10);
                    }
                }
                else
                {
                    WriteAt("Account not found! Search another account (y/n)?", startCol, startRow + 8);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        DeleteAccountScreen(7, 40, 2, 10);
                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        MainScreen(13, 40, 2, 10);
                    }
                }
            } while (true);
        }

        //Method to print strings at a specific position in the console string 
        protected void WriteAt(string s, int col, int row)
        {
            try
            {
                Console.SetCursorPosition(origCol + col, origRow + row);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface ConsoleInterface = new UserInterface();

            ConsoleInterface.LoginScreen(10, 40, 2, 10);
            Console.ReadKey();
        }
    }
}
