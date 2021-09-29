using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927
{
    public class Transactions : Account
    {

        //public bool DepositMoney(string accountNumber, int depositMoney)
        //{

        //    string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\" + accountNumber + ".txt";
        //    if (File.Exists(fileName))
        //    {
        //        var account = FindAccount(accountNumber);
        //        account.Amount += depositMoney;
        //        return SaveAccount(account) ? true : false; // if account wasnt saved return false
        //    }
        //    return false; //Account not found
        //}


        //public bool WithdrawMoney(string accountNumber, int withdrawMoney)
        //{
        //    string fileName = @"C:\Users\Akask\source\repos\DotNetAssignment1_31927\Accounts\" + accountNumber + ".txt";
        //    if (File.Exists(fileName))
        //    {
        //        var account = FindAccount(accountNumber);

        //        if (account.Amount < withdrawMoney)
        //        {
        //            account.Amount -= withdrawMoney;
        //            SaveAccount(account);
        //            return true;
        //        }
        //    }
        //    return false; //Account not found
        //}

    }
}
