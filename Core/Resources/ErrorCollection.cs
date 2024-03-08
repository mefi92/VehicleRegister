using BoundaryHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resources
{
    internal static class ErrorCollection
    {
        // Vehicle related data
        public static ErrorData RegistrationNumberDoesNotExist = new ErrorData { ErrorCode = 100, Message = Messages.RegistrationNumberDoesNotExist };
        public static ErrorData EngineNumberDoesNotExist = new ErrorData { ErrorCode = 101, Message = Messages.EngineNumberDoesNotExist };
        public static ErrorData InvalidVehicleType = new ErrorData { ErrorCode = 102, Message = Messages.InvalidVehicleType };
        public static ErrorData InvalidEngineNumber = new ErrorData { ErrorCode = 103, Message = Messages.InvalidEngineNumber };

        // Person related data
        public static ErrorData InvalidFirstNameNull = new ErrorData { ErrorCode = 201, Message = Messages.InvalidFirstNameNull };
        public static ErrorData InvalidFirstNameLength = new ErrorData { ErrorCode = 202, Message = Messages.InvalidFirstNameLength };
        public static ErrorData InvalidFirstNameChar = new ErrorData { ErrorCode = 203, Message = Messages.InvalidFirstNameChar };
        public static ErrorData InvalidLastNameNull = new ErrorData { ErrorCode = 204, Message = Messages.InvalidLastNameNull };
        public static ErrorData InvalidLastNameLength = new ErrorData { ErrorCode = 205, Message = Messages.InvalidLastNameLength };
        public static ErrorData InvalidLastNameChar = new ErrorData { ErrorCode = 206, Message = Messages.InvalidLastNameChar };
        public static ErrorData InvalidPostalCode = new ErrorData { ErrorCode = 207, Message = Messages.InvalidPostalCode };

    }
}
