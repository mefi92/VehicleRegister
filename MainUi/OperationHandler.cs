using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUi
{
    public class OperationHandler
    {
        private UiController uiController;

        public OperationHandler(UiController uiController)
        {
            this.uiController = uiController;
        }

        public void OperationSelection()
        {
            bool isSelected = false;
            while (!isSelected)
            {
                Console.WriteLine("Válassz tevékenységet!\r\nÚj jármű regisztrálása (r), adatlekérdezés rendszám alapján (l),  kilépés (q):");
                string input = Console.ReadLine();
                isSelected = SelectAction(input);
            }
        }

        private bool SelectAction(string input)
        {
            bool isSelected = false;
            switch (input)
            {
                case "r":
                    RegisterNewVehicleInput();
                    break;
                case "l":
                    LoadVehicleDataInput();
                    break;
                case "q":
                    isSelected = QuitProgram();
                    break;
                default:
                    break;
            }

            return isSelected;
        }

        public void RegisterNewVehicleInput()
        {
            Console.WriteLine("Jármű típusa: ");
            string vehicleType = Console.ReadLine();
            Console.WriteLine("Motor száma: ");
            string engineNumber = Console.ReadLine();
            uiController.addNewVehicle(vehicleType, engineNumber);
        }

        public void LoadVehicleDataInput()
        {
            Console.WriteLine("Írja be a rendszámot a következő formátumba: AAAA123");
            string plateNumber = Console.ReadLine();
            uiController.LoadVehicle(plateNumber);
        }

        private static bool QuitProgram()
        {
            Console.WriteLine("A program leáll.");
            return true;
        }
    }
}
