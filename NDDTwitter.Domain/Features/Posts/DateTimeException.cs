using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDDTwitter.Domain.Exceptions
{
    public class DateTimeException : BusinessException
    {
        public DateTimeException() : base("Data do post inválida!")
        {
        }
    }
}
