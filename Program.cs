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

                    WriteAt("Valid Credentials", startCol, noLines + 2);
                    Console.ReadKey();
                    break;
                }
                else
                {
                    WriteAt("Invalid Credentials", startCol, noLines + 2);
                }
            } while (true);
            Console.ReadKey();
        }

        public void MainScreen(int noLines, int formWidth, int startRow, int startCol) { 
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
