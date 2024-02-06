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
            string firstPart = plateNumber.Substring(0, 4); // todo: át kéne gondolni, hogy milyen formátumban mentjük a reg számot mert ez igy katyvasz
            int secondPartValue = ExtractSecondPartValue(plateNumber);

            secondPartValue = UpdateSecondPartValue(secondPartValue, out bool increment);

            string incrementedFirstPart = IncrementFirstPart(firstPart, increment);

            return $"{incrementedFirstPart}{secondPartValue:D3}";
        }

        private static string RemoveFormatting(string plateNumber)
        {
            string firstPartOne = plateNumber.Substring(0, 2);
            string firstPartTwo = plateNumber.Substring(3, 2);
            string firstPart = firstPartOne + firstPartTwo;
            return firstPart;
        }

        private int ExtractSecondPartValue(string plateNumber)
        {
            string secondPart = plateNumber.Substring(6, 3).TrimStart('0');
            return string.IsNullOrEmpty(secondPart) ? 0 : int.Parse(secondPart);
        }

        private int UpdateSecondPartValue(int value, out bool increment)
        {
            increment = false;

            if (value > 999)
            {
                value = 1;
                increment = true;
            }

            return value + 1;
        }

        private string IncrementFirstPart(string firstPart, bool increment)
        {
            var sb = new StringBuilder();

            foreach (char c in firstPart.Reverse())
            {
                if (increment)
                {
                    int newCharValue = c + 1;
                    sb.Insert(0, newCharValue > 90 ? 'A' : (char)newCharValue);
                    increment = newCharValue > 90;
                }
                else
                {
                    sb.Insert(0, c);
                }
            }

            return sb.ToString();
        }
    }
}
