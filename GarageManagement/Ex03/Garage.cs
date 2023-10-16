using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<Vehicle> r_VehiclesInGarage;

        public Garage()
        {
            this.r_VehiclesInGarage = new List<Vehicle>();
        }

        // $G$ CSS-013 (-3) Bad parameter name (should be in the form of i_PascalCase).
        public Vehicle GetVehicle(string i_registation)
        {
            if(!IsCarExist(i_registation))
            {
                throw new ArgumentException();
            }

            int vehicleIndex = getCarIndex(i_registation);
            return this.r_VehiclesInGarage[vehicleIndex];
        }

        public bool ChargeEV(string i_Registration, float i_HoursToCharge)
        {
            int indexToCharge = getCarIndex(i_Registration);

            return this.r_VehiclesInGarage[indexToCharge].ChargeEV(i_HoursToCharge);
        }

        public bool fillTank(string i_Registration, float i_AmountOfFuelToAdd, FuelTypeOptions i_FuelType)
        {
            int indexToCharge = getCarIndex(i_Registration);

            return this.r_VehiclesInGarage[indexToCharge].FillGasTank(i_AmountOfFuelToAdd, i_FuelType);
        }

        public bool IsCarExist(string i_Registration)
        {
            return this.getCarIndex(i_Registration) >= 0;
        }

        public void FillCarTires(string i_Registation)
        {
            if(!IsCarExist(i_Registation))
            {
                throw new ArgumentException("No Car Exsits With Such Registation Number");
            }

            int index = getCarIndex(i_Registation);

            this.r_VehiclesInGarage[index].FillTiresToMax();
        }

        public List<Vehicle> GetAllVeichles(RepairStatusOptions? i_RepairStatus)
        {
            List<Vehicle> resultingVehicles = new List<Vehicle>();

            foreach(Vehicle currentVehicle in this.r_VehiclesInGarage)
            {
                if(i_RepairStatus != null && currentVehicle.getStatus() == i_RepairStatus)
                {
                    resultingVehicles.Add(currentVehicle);
                }
            }

            return resultingVehicles;
        }

        public void AdmitVehicle(Vehicle i_Vehicle)
        {
            this.r_VehiclesInGarage.Add(i_Vehicle);
        }

        // $G$ CSS-013 (-3) Bad parameter name (should be in the form of i_PascalCase).
        public void ShowCurrentVehicles(RepairStatusOptions repairStatusOptions)
        {
            foreach(Vehicle currentVehicle in this.r_VehiclesInGarage)
            {
                Console.WriteLine(currentVehicle);
            }
        }

        public void VehicleStatusUpdate(string i_VeichleRegistration, RepairStatusOptions i_RepairStatus)
        {
            bool succsess = false;

            foreach (Vehicle currentVehicle in this.r_VehiclesInGarage)
            {
                if(currentVehicle.getVeichleRegistation().Equals(i_VeichleRegistration))
                {
                    currentVehicle.UpdateRepairStatus(i_RepairStatus);
                    succsess = true;
                }
            }

            if(!succsess)
            {
                throw new ArgumentException("No Vehicle With Such Registration Found");
            }
        }

        private int getCarIndex(string i_Registration)
        {
            int resultingIndex = -1;

            for (int i = 0; i < this.r_VehiclesInGarage.Count; i++)
            {
                if (this.r_VehiclesInGarage[i].getVeichleRegistation().Equals(i_Registration))
                {
                    resultingIndex = i;
                    break;
                }
            }

            return resultingIndex;
        }
    }
}