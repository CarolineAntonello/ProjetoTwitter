using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDDTwitter.Domain.Exceptions
{
    public class MessageIsToLong : BusinessException
    {
        public MessageIsToLong() : base("Mensagem ultrapassou o limite de caracteres!")
        {
        }
    }
}
