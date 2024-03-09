using BoundaryHelper;
using Core.Interfaces;
using Core.Mappers;
using Core.Resources;
using Core.VerificationObjects;
using Entity;

namespace Core
{
    public class LogicManagerInteractor : IVehicleManagerInBoundary
    {
        private readonly RegisterVehicleProcess register;
        private readonly LoadVehicleProcess load;

        public LogicManagerInteractor(IPersistentVehicleGateway persistentVehicleGateway, IPersistentPersonGateway persistentPersonGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.register = new RegisterVehicleProcess(persistentVehicleGateway, persistentPersonGateway, presenterManager);
            this.load = new LoadVehicleProcess(persistentVehicleGateway, persistentPersonGateway, presenterManager);  
        }

        public void LoadVehicleData(string request)
        {
            load.ProcessLoadVehicleRequest(request);
        }

        public void RegisterNewVehicle(string request)
        {
            register.ProcessVehicleRegistrationRequest(request);
        }
    }
}