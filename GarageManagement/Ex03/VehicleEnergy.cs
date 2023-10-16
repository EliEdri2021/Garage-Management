namespace Ex03.GarageLogic
{
    // $G$ CSS-019 (-5) Enum should be in form of ePascalCase.
    // $G$ CSS-999 (-5) Every Class/Enum/Struct which is not nested should be in a separate file.
    public enum EnergyTypeOptions
    {
        ElectricVehicleEnergy,
        ChemicalVehicleEnergy
    }

    // $G$ CSS-999 (-3) The class must have an access modifier.
    abstract class VehicleEnergy
    {
        EnergyTypeOptions EnergyType;
        private float EnergyPrecentage;

        public abstract override string ToString();
    }
}