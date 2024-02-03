using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessageObjects
{
    internal interface ICommandMessage
    {
        string Command { get; set; }
    }
}
