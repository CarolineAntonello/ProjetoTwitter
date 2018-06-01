using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDDTwitter.Domain.Exceptions
{
    public class MessageIsNullOrEmpty : BusinessException
    {
        public MessageIsNullOrEmpty() : base("Mensagem do post não pode ser vazia!")
        {
        }
    }
}
