using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class BadRequestExeption(List<string> errors) :Exception("Validation Faild")
    {

        public List<string> Errors { get; set; } = errors;


    }
}
