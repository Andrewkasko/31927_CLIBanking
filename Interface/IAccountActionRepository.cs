using DotNetAssignment1_31927.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927.Interface
{
    public interface IAccountActionRepository
    {
        int CreateAccount(AccountModel accountModel);
        bool SaveAccount(AccountModel accountModel);
        bool EmailStatement(AccountModel accountModel);
        AccountModel FindAccount(string accountNumber);
        bool DeleteAccount(string accountNumber);
    }
}
