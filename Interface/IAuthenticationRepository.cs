using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment1_31927.Interface
{
    public interface IAuthenticationRepository
    {
        bool CheckPassword(string username, string password);
    }
}
