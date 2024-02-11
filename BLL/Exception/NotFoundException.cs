using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exception
{
    public class NotFoundException : IOException
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
