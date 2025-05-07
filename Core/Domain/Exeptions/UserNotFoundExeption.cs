using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class UserNotFoundExeption(string Email):NotFoundExecption($"User With Email : {Email} Not Found")
    {
    }
}
