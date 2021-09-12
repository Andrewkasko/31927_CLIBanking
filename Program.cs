using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927
{


    class UserInterface {

        protected static int origRow;
        protected static int origCol;
        string[] loginWindowFields = { "Username: ", "Password: " };

        int[,] loginFieldPos = new int[2, 2];
        string[] loginUserInputs = new string[2];

        //Method to create login screen
        public void LoginScreen(int noLines, int formWidth, int startRow, int startCol) {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            for(int line = 0; line < noLines; line++) {
                if (line == 0 | line == 2 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("-", startCol + col, startRow + line);

                    }
                }
                else {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
            WriteAt("Welcome to My Bank", startCol + 10, startRow + 1);
            WriteAt("Login To Start", startCol + 10, startRow + 4);

            int item = 0;
            foreach (string fieldName in loginWindowFields) {
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

                if (loginUserInputs[0].CompareTo("Andrew") == 0 && loginUserInputs[1].CompareTo("Password") == 0)
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


        public void HomeScreen(int noLines, int formWidth, int startRow, int startCol) {

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
        public void MainScreen(int noLines, int formWidth, int startRow, int startCol) {

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

            switch (keyInfo.Key) { 
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
            WriteAt("First Name: ", startCol + 6, startRow + 5);
            WriteAt("Last Name: ", startCol + 6, startRow + 6);
            WriteAt("Address: ", startCol + 6, startRow + 7);
            WriteAt("Phone: ", startCol + 6, startRow + 8);
            WriteAt("Email: ", startCol + 6, startRow + 9);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                MainScreen(13, 40, 2, 10);
            }

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
            WriteAt("Account Number: ", startCol + 6, startRow + 5);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                MainScreen(13, 40, 2, 10);
            }

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
            WriteAt("DEPOSIT", startCol + 4, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);
            WriteAt("Account Number: ", startCol + 6, startRow + 5);
            WriteAt("Amount: ", startCol + 6, startRow + 6);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                MainScreen(13, 40, 2, 10);
            }

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
            WriteAt("WITHDRAW", startCol + 4, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 6, startRow + 3);
            WriteAt("Account Number: ", startCol + 6, startRow + 5);
            WriteAt("Amount: ", startCol + 6, startRow + 6);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                MainScreen(13, 40, 2, 10);
            }

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
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                MainScreen(13, 40, 2, 10);
            }

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
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                MainScreen(13, 40, 2, 10);
            }

        }




        //Method to print strings at a specific position in the console string 
        protected void WriteAt(string s, int col, int row) {
            try {
                Console.SetCursorPosition(origCol + col, origRow + row);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UserInterface ConsoleInterface = new UserInterface();
            
            ConsoleInterface.LoginScreen(10, 40, 2, 10);
            Console.ReadKey();
        }
    }
}
