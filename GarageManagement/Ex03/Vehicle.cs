using System;
using System.Text;

namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    // $G$ CSS-999 (-5) Every Class/Enum/Struct which is not nested should be in a separate file.
    public enum RepairStatusOptions
    {
        UnderGoingRepair,
        RepairComplete,
        TransactionComplete
    }

    // $G$ CSS-999 (-5) Every Class/Enum/Struct which is not nested should be in a separate file.
    internal struct Owner
    {
        private readonly string r_name;
        private readonly string r_phone;

        public Owner(string i_name, string i_phone)
        {
            this.r_name = i_name;
            this.r_phone = i_phone;
        }

        public string GetName()
        {
            return this.r_name;
        }

        public string GetPhone()
        {
            return this.r_phone;
        }

        public override string ToString()
        {
            return string.Format("Owner: Name {0}, Phone {1}", this.r_name, this.r_phone);
        }
    }

    // $G$ DSN-999 (-5) This class should have been abstract.
    public class Vehicle
    {

        private RepairStatusOptions m_RepairStatus;
        private string m_RegistrationPlateNumber;
        private string m_ModelName;

        private Owner m_VehicleOwner;

        private List<Tyre> m_VehicleTires;

        private VehicleEnergy m_EnergyInformation;

        private VehicleSpecifics m_VehicleSpecifics;

        public Vehicle(string i_ModelName, string i_RegistrationPlateNumber)
        {
            this.m_RepairStatus = RepairStatusOptions.UnderGoingRepair;
            this.m_ModelName = i_ModelName;
            this.m_RegistrationPlateNumber = i_RegistrationPlateNumber;
        }

        public RepairStatusOptions getStatus()
        {
            return this.m_RepairStatus;
        }

        public bool ChargeEV(float i_HoursToChange)
        {
            bool sucsses = false;

            if(this.m_EnergyInformation is ElectricVehicleEnergy)
            {
                (m_EnergyInformation as ElectricVehicleEnergy).ChargeBattery(i_HoursToChange);
            
                sucsses = true;
            }

            return sucsses;
        }

        public bool FillGasTank(float i_AmountOfFuelToAdd, FuelTypeOptions i_FuelType)
        {
            bool sucsses = false;

            if (this.m_EnergyInformation is ChemicalVehicleEnergy)
            {
                (m_EnergyInformation as ChemicalVehicleEnergy).FuelUp(i_AmountOfFuelToAdd, i_FuelType);

                sucsses = true;
            }

            return sucsses;
        }

        internal void SetOwner(Owner i_VehcileOwner)
        {
            this.m_VehicleOwner = i_VehcileOwner;
        }

        internal void SetTires(List<Tyre> i_VehicleTires)
        {
            this.m_VehicleTires = i_VehicleTires;
        }

        internal void SetEnergySolution(VehicleEnergy i_EnergyInformation)
        {
            this.m_EnergyInformation = i_EnergyInformation;
        }

        internal void SetSpecificsInformation(VehicleSpecifics i_VehicleSpecifics)
        {
            this.m_VehicleSpecifics = i_VehicleSpecifics;
        }


        public void FillTiresToMax()
        {
            foreach(Tyre currentVehicleTire in this.m_VehicleTires)
            {
                currentVehicleTire.FillTireToMax();
            }
        }

        public void UpdateRepairStatus(RepairStatusOptions i_RepairStatus)
        {
            this.m_RepairStatus = i_RepairStatus;
        }


        public string getVeichleRegistation()
        {
            return this.m_RegistrationPlateNumber;
        }

        public override string ToString()
        {
            StringBuilder tireInfo = new StringBuilder();
            foreach(Tyre currentTyre in this.m_VehicleTires)
            {
                tireInfo.Append(currentTyre.ToString());
            }

            return string.Format("Vehicle Registration : {0} , Vehicle Model : {1} {2}{3}{2}{4}{5}{2}{6}", 
                this.m_RegistrationPlateNumber, this.m_ModelName, System.Environment.NewLine, tireInfo.ToString(),
                this.m_EnergyInformation, this.m_VehicleOwner, this.m_VehicleSpecifics);
        }

    }

    // $G$ CSS-027 (-2) Unnecessary blank lines.


}