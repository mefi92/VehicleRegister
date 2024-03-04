using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Exceptions
{
    public class FilePersistenceException : Exception
    {
        public FilePersistenceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
