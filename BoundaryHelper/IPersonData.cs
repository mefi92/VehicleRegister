using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundaryHelper
{
    public interface IPersonData
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string AdPostalCode { get; set; }
        string AdCity { get; set; }
        string AdStreet { get; set; }
        string AdStreetNumber { get; set; }
    }
}
