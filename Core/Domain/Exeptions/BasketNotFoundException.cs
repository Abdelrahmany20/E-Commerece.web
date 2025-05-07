using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class BasketNotFoundException(string Key) :NotFiniteNumberException($"Basktet Woth Key : {Key} Not Found")
    {


    }
}
