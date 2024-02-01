using Core.MessageObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class JsonConverter
    {
        public string ConvertToStringRegisterNewVehicleOutputMessage(RegisterNewVehicleOutputMessage message)
        {
            string json = JsonConvert.SerializeObject(message);

            return json;
        }
    }
}
