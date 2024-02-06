﻿using Core;
using Entity;
using System.Xml;

namespace Persistence
{
    public class VehicleFileSystem : IPersistentVehicleGateway
    {
        private string fileType = ".txt";

        public Vehicle LoadVehicle(string carRegistrationNumber)
        {
            string fileName = FindVehicleTextFile(carRegistrationNumber);
            
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open);
                StreamReader reader = new StreamReader(stream);

                string vehicleType = InputSplitter(reader.ReadLine());
                string engineNumber = InputSplitter(reader.ReadLine());
                string registrationNumber = InputSplitter(reader.ReadLine());

                reader.Close();

                Vehicle vehicle = new Vehicle(vehicleType, registrationNumber, engineNumber);
                return vehicle;
            }
            catch (FileNotFoundException)
            {
                return null;
            }            
        }        

        public void SaveVehicle(Vehicle vehicle)
        {
            string rawRegNumber = RawPlateNumber(vehicle.registrationNumber);
            string fileName = rawRegNumber + "_" + vehicle.engineNumber + fileType;

            try
            {
                FileStream stream = File.Open(fileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);
                writer.Write("Vehicle brand: " + vehicle.type);
                writer.Write("\n");
                writer.Write("Eninge number: " + vehicle.engineNumber);
                writer.Write("\n");
                writer.Write("Registration number: " + FormatPlateNumber(vehicle.registrationNumber));
                writer.Write("\n");
                writer.Close();
            }
            catch
            {
                throw new Exception();
            }

        }

        private static string? FindVehicleTextFile(string carRegistrationNumber)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(baseDirectory);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                if (fileName != null && fileName.Length >= 7 && fileName.Substring(0, 7).Equals(carRegistrationNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return fileName;
                }
            }
            return null;
        }

        public string? GetLatestRegNumber()
        {
            string? latestPlateText = null;
            string defaultPlateNumber = "AAAA000";
            int highestWeigth = 0;

            FileInfo[] Files = CollectTextFilesInBaseDirecotry();

            if (Files.Length > 0)
            {
                foreach (FileInfo file in Files)
                {
                    int weigth = 0;

                    try { weigth = GetPlateValue(file.Name); }
                    catch { return FormatPlateNumber(defaultPlateNumber); }

                    if (weigth > highestWeigth) { highestWeigth = weigth; latestPlateText = file.Name; }
                }

                if (highestWeigth != 0) { return FormatPlateNumber(latestPlateText); }
            }
            return FormatPlateNumber(defaultPlateNumber);
        }

        private static FileInfo[] CollectTextFilesInBaseDirecotry()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo d = new DirectoryInfo(baseDirectory);
            FileInfo[] Files = d.GetFiles("*.txt");
            return Files;
        }

        private static int GetPlateValue(string plateNumber)
        {
            int finalValue = 0;
            //if (plateNumber.Length != 11) { return 0; }            

            string secondPart = plateNumber.Substring(4, 3);
            secondPart = secondPart.TrimStart('0');
            finalValue += Int32.Parse(secondPart);

            string firstPart = plateNumber.Substring(0, 4);

            int[] multipliers = [10000, 1000, 100, 10];

            for (int i = 0; i < firstPart.Length; i++)
            {
                finalValue += firstPart[i] * multipliers[i];
            }

            return finalValue;
        }

        private static string FormatPlateNumber(string plateNumber)
        {
            return $"{plateNumber.Substring(0, 2)}:{plateNumber.Substring(2, 2)}-{plateNumber.Substring(4, 3)}";
        }

        private static string RawPlateNumber(string plateNumber)
        {
            string rawPlateNumber = new string(plateNumber.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            rawPlateNumber = rawPlateNumber.ToUpper();
            return rawPlateNumber;
        }

        private static string InputSplitter(string input)
        {
            string[] parts = input.Split(new string[] { ": " }, StringSplitOptions.None);
            string result = parts.Length > 1 ? parts[1].Trim() : string.Empty;
            return result;
        }

        public bool IsEngineNumberInUse(string engineNumber)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;  //refactor later. These lines used multiple times in this project.
            DirectoryInfo d = new DirectoryInfo(baseDirectory);
            FileInfo[] files = d.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                if (file.Name.Contains("_" + engineNumber + "."))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
