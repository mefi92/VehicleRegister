using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUi.MessageObjects
{
    internal class GenericCommandMessage<T> : ICommandMessage
    {
        public string Command { get; set; }
        public T Data { get; set; }
        public ErrorData Error { get; set; }

        public static GenericCommandMessage<T> Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<GenericCommandMessage<T>>(json);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
