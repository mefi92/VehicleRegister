using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IPersistentPersonGateway
    {
        Person LoadPerson(string hashNumber);

        void SavePerson(Person person);

    }
}
