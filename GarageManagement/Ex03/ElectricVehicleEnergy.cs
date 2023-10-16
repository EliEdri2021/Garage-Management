namespace Ex03.GarageLogic
{
    internal class ElectricVehicleEnergy : VehicleEnergy
    {
        private float m_EnergyLeftInHours;
        private readonly float r_MaximumEnergyCapacity;

        public ElectricVehicleEnergy(float energyLeftInHours, float maximumEnergyCapacity)
        {
            m_EnergyLeftInHours = energyLeftInHours;
            r_MaximumEnergyCapacity = maximumEnergyCapacity;
        }

        public void ChargeBattery(float i_BatteryHoursToCharge)
        {
            float maximumFuelUpAmount = this.r_MaximumEnergyCapacity - this.m_EnergyLeftInHours;

            if (i_BatteryHoursToCharge < 0 || i_BatteryHoursToCharge > maximumFuelUpAmount)
            {
                throw new ValueOutOfRangeException(maximumFuelUpAmount, 0);
            }

            this.m_EnergyLeftInHours += i_BatteryHoursToCharge;
        }
        public override string ToString()
        {
            return string.Format("Electrically Powered: Max Charge {0} , Current Change {1}", this.r_MaximumEnergyCapacity, this.m_EnergyLeftInHours);
        }
    }
}
