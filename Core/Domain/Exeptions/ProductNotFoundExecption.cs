using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class ProductNotFoundExecption(int Id) : NotFoundExecption($"Product Wit Id :{Id} Is Not Found ")
    {
    }
   
}
