using Core;

namespace MainUi
{
    public class UiController
    {
        private VehicleManagerInBoundary vehicleManagerInBoundary;

        public UiController(VehicleManagerInBoundary vehicleManagerInBoundary)
        {
            this.vehicleManagerInBoundary = vehicleManagerInBoundary;
        }

        public void addNewVehicle(string vehicleType, string engineNumber)
        {
            vehicleManagerInBoundary.registerNewVehicle(vehicleType, engineNumber);
        }

    }
}
