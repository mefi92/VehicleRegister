﻿using Core;
using Persistence;
using MainUi;
using System.Text;
using System.IO;
using System;


namespace Starter
{
    public class UiStarter
    {

        static void Main(string[] args)
        {           
            UiPresenter uiPresenter = new UiPresenter();
            IPersistentVehicleGateway persistentVehicleGateway = new VehicleFilePersistenceManager();
            IVehicleManagerInBoundary vehicleManagerInBoundary = new LogicManagerInteractor(persistentVehicleGateway, uiPresenter);
            ConsoleUi consoleUi = new ConsoleUi(vehicleManagerInBoundary);
            uiPresenter.setConsoleUI(consoleUi);
            consoleUi.mainLoop();
        }
        
    }
}
