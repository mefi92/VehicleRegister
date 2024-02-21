using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUi
{
    public class ConsoleView
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\nVálassz tevékenységet!\r\nÚj jármű regisztrálása (r), Adatlekérdezés rendszám alapján (l),  Kilépés (q):");
        }

        public string GetUserInput()
        {
            Console.Write("Válaszott lépés: ");
            return Console.ReadLine();
        }

        public void DisplayVehicleRegistration(string registrationNumber)
        {
            Console.WriteLine($"\nSikeres jármű regisztráció!\nAz új rendszám: {registrationNumber}");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine().ToUpper();
        }

        public int GetIntegerInput(string prompt)
        {
            int result;
            string input;
            bool isValid;

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                isValid = int.TryParse(input, out result);

                if (!isValid)
                {
                    Console.WriteLine("Hibás adat.");
                }

            } while (!isValid);

            return result;
        }
    }
}
