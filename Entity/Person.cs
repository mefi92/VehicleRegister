using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Entity
{
    public class Person
    {
        //nem biztos, hogy ez szerencsés megoldás, hogy a set-et kiveszed belőle, majd erről beszéljünk!
        public string FirstName { get; }
        public string LastName { get; }
        public string AdPostalCode { get; }
        public string AdCity { get; }
        public string AdStreet { get; }
        public string AdStreetNumber { get; }

        [JsonIgnore]
        public string Hash { get; }

        public Person(string firstName, string lastName, string postalCode, string city, string street, string streetnumber)
        {
            FirstName = firstName;
            LastName = lastName;
            AdPostalCode = postalCode;
            AdCity = city;
            AdStreet = street;
            AdStreetNumber = streetnumber;            
            Hash = GenerateHash();
        }

        private string GenerateHash()
        {
            string dataToHash = $"{FirstName}{LastName}{AdPostalCode}{AdCity}{AdStreet}{AdStreetNumber}";

            using (MD5 md5 = MD5.Create())
            {
                // Convert the string to bytes
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataToHash);

                // Compute the hash
                byte[] hashBytes = md5.ComputeHash(dataBytes);

                // Convert the hash bytes to a string
                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }
    }
}
