using System;
using System.Runtime.CompilerServices;

namespace Ex03.GarageLogic
{
    public enum FuelTypeOptions
    {
        octan95,
        octan96,
        octan98,
        soler
    }

    internal class ChemicalVehicleEnergy : VehicleEnergy
    {

        private readonly FuelTypeOptions r_FuelType;
        private readonly float r_MaximumFuelAmountLitters;
        private float m_CurrentFuelAmountLittrers;

        public ChemicalVehicleEnergy(FuelTypeOptions i_FuelType, float i_CurrentFuelAmountLittrers, float i_MaximumFuelAmountLitters)
        {
            this.r_FuelType = i_FuelType;
            this.r_MaximumFuelAmountLitters = i_MaximumFuelAmountLitters;
            this.m_CurrentFuelAmountLittrers = i_CurrentFuelAmountLittrers;
        }

        public void FuelUp(float i_AmountOfFuelToAdd, FuelTypeOptions i_FuelType)
        {
            float maximumFuelUpAmount = this.r_MaximumFuelAmountLitters - this.m_CurrentFuelAmountLittrers;

            if(i_AmountOfFuelToAdd < 0 || i_AmountOfFuelToAdd > maximumFuelUpAmount)
            {
                throw new ValueOutOfRangeException(maximumFuelUpAmount, 0);
            }

            if(i_FuelType != this.r_FuelType)
            {
                throw new ArgumentException("Invalid Argument Give For Fuel Type");
            }

            this.m_CurrentFuelAmountLittrers += i_AmountOfFuelToAdd;
        }

        public override string ToString()
        {
            return String.Format("Chemically Powered: Fuel Type {0} , Max Fuel [L]: {1} , Current Fuel Amount {2}", r_FuelType, r_MaximumFuelAmountLitters, m_CurrentFuelAmountLittrers);
        }
    }
}