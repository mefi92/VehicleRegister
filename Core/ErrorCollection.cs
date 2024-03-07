using BoundaryHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal static class ErrorCollection
    {
        public static ErrorData RegistrationNumberNotExistsYet=new ErrorData { ErrorCode=1234, Message= Messages.RegistrationNumberNotExistsYet }

    }
}
