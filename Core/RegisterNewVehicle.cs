﻿using Entity;
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
            // Remove Formating
            string firstPartOne = plateNumber.Substring(0, 2);
            string firstPartTwo = plateNumber.Substring(3, 2);
            string firstPart = firstPartOne + firstPartTwo;

            string secondPart = plateNumber.Substring(6, 3);
            secondPart = secondPart.TrimStart('0');
            int secondPartValue = 0;

            if (secondPart != "")
            {
                secondPartValue = Int32.Parse(secondPart);
            }

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

            return FormatPlateNumber(sb.ToString());
        }

        private static string FormatPlateNumber(string plateNumber) // M: ez már létezik máshol is...
        {
            return $"{plateNumber.Substring(0, 2)}:{plateNumber.Substring(2, 2)}-{plateNumber.Substring(4)}";
        }

    }
}
