﻿using Core.Exceptions;
using Core.Resources;
using System.Text;

namespace Core
{
    public static class RegistrationNumberGenerator
    {
        public static string GetNextRegistrationNumber(string plateNumber)
        {
            if (plateNumber == ConstantCollection.LastPlateNumber)
            {
                throw new OutOfRegistrationNumberException(Messages.OutOfRegistrationNumber, null);
            }

            string firstPart = plateNumber.Substring(0, 4); 
            int secondPartValue = ExtractSecondPartValue(plateNumber);

            secondPartValue = UpdateSecondPartValue(secondPartValue, out bool increment);
            string incrementedFirstPart = IncrementFirstPart(firstPart, increment);
            string mergedRegistrationNumber = $"{incrementedFirstPart}{secondPartValue:D3}";

            return RegistrationNumberFormatter.FormatRegistrationNumber(mergedRegistrationNumber);
        }

        private static int ExtractSecondPartValue(string plateNumber)
        {
            string secondPart = plateNumber.Substring(4, 3).TrimStart('0');
            return string.IsNullOrEmpty(secondPart) ? 0 : int.Parse(secondPart);
        }

        private static int UpdateSecondPartValue(int value, out bool increment)
        {
            increment = false;

            if (value >= 999)
            {
                value = 0;
                increment = true;
            }

            return value + 1;
        }

        private static string IncrementFirstPart(string firstPart, bool increment)
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
