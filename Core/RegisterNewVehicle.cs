using Entity;
using System.Text;

namespace Core
{
    public class RegisterNewVehicle
    {
        public Vehicle addNewVehicle(string vehicleType, string engineNumber, string latestRegNumber)
        {
            return new Vehicle(vehicleType, GetNextRegistrationNumber(latestRegNumber), engineNumber);
        }

        public string GetNextRegistrationNumber(string plateNumber) 
        {
            string firstPart = plateNumber.Substring(0, 4);

            string secondPart = plateNumber.Substring(4, 3);
            secondPart = secondPart.TrimStart('0');
            int secondPartValue = Int32.Parse(secondPart);
            secondPartValue += 1;
            bool increment = false;

            if (secondPartValue > 999)
            {
                secondPartValue = 1;
                increment = true;
            }

            var sb = new StringBuilder();

            foreach (char c in firstPart.Reverse())
            {
                if (increment)
                {
                    int new_c = c + 1;
                    if (new_c > 90)
                    {
                        sb.Insert(0, 'A');
                        increment = true;
                    }
                    else
                    {
                        sb.Insert(0, (char)new_c);
                        increment = false;
                    }
                }
                else
                {
                    sb.Insert(0, c);
                }

            }
            sb.Append(secondPartValue.ToString("000"));

            return sb.ToString();
        }
            
    }
}
