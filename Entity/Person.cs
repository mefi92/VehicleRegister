using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string AdPostalCode { get; }
        public string AdCity { get; }
        public string AdStreet { get; }
        public string AdStreetNumber { get; }

        public string Hash { get; set; }

        public Person(string firstName, string lastName, string postalCode, string city, string street, string streetnumber)
        {
            FirstName = firstName;
            LastName = lastName;
            AdPostalCode = postalCode;
            AdCity = city;
            AdStreet = street;
            AdStreetNumber = streetnumber;            
        }

        public void GenerateHash()
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

                Hash = hashStringBuilder.ToString();

                //return hashStringBuilder.ToString();
            }
        }
    }
}
