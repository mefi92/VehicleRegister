using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Entity
{
    public class Person
    {
        //nem biztos, hogy ez szerencsés megoldás, hogy a set-et kiveszed belőle, majd erről beszéljünk! M: üres person-t nem tudok létrehozni és utólag feltölteni
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string AdPostalCode { set; get; }
        public string AdCity { set; get; }
        public string AdStreet { set; get; }
        public string AdStreetNumber { set; get; }

        [JsonIgnore]
        public string Hash { set; get; }

        public Person() { }

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
