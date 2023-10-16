using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ex03.GarageLogic
{

    public static class ObjectCreator
    {
        // $G$ DSN-999 (-8) Why creating so many objects for nothing? You were supposed to get a specific type from
        // the user, and create the concrete vehicle. In addition, no use of polymorphism - all vehicle types
        // inherite from the same base class, which is the Vehicle class. You could have done something like that:
        // Create a switch case block, and create Vehicle vehicle = null instance,
        // then assign to it the proper vehicle that should be return at the moment.
        public static Dictionary<string, System.Type> m_PossibleTypes =
            new Dictionary<string, System.Type>() { { "Bike", typeof(Bike) }, {"Car", typeof(Car)} , {"Truck", typeof(Truck)}};

        public static Dictionary<System.Type, int> m_TyreAmountByType =
            new Dictionary<System.Type, int>() { {typeof(Bike) , 2}, { typeof(Car), 4 }, {typeof(Truck), 16} };

        // can be later easily altered to distinguish EV or Conventional

        public static Dictionary<System.Type, int> m_TyrMaxPressureByType =
            new Dictionary<System.Type, int>() { { typeof(Bike), 31 }, { typeof(Car), 27 }, { typeof(Truck), 25 } };

        public static Dictionary<System.Type, float> m_ElectricMaxHours =
            new Dictionary<System.Type, float>() { { typeof(Bike), 2.8f }, { typeof(Car), 4.5f }};

        public static Dictionary<System.Type, float> m_ChemicalMaxLitters =
            new Dictionary<System.Type, float>() { { typeof(Bike), 5.4f }, { typeof(Car), 52f }, { typeof(Truck), 135f } };

        public static Dictionary<System.Type, FuelTypeOptions> m_ChemicalFuelType =
            new Dictionary<System.Type, FuelTypeOptions>() { { typeof(Bike), FuelTypeOptions.octan98 }, { typeof(Car), FuelTypeOptions.octan95 }, { typeof(Truck), FuelTypeOptions.soler } };

        public static Dictionary<string, Type> m_SpecificsNameToType =
            new Dictionary<string, Type>() { {"Bike", typeof(Bike)} , {"Car", typeof(Car)} , {"Truck" , typeof(Truck)} };

        public static Dictionary<string, Type> m_VeichleEnergy =
            new Dictionary<string, Type>() { { "EV", typeof(ElectricVehicleEnergy) }, { "Regular", typeof(ChemicalVehicleEnergy) } };


        public static Vehicle CreateVehicle(string i_ModelName, string i_RegistrationPlateNumber)
        {
            return new Vehicle(i_ModelName, i_RegistrationPlateNumber);
        }



        public static void AddTiresToVehicle(List<string> i_TireBrands, List<float> i_TirePressures, Type i_VehicleType, ref Vehicle i_VehicleToModify)
        {
            if(i_TireBrands.Count != m_TyreAmountByType[i_VehicleType] || i_TireBrands.Count != i_TirePressures.Count)
            {
                throw new ArgumentException("Argument Length Mismatch");
            }

            List<Tyre> tiresOnVehicle = new List<Tyre>();

            for(int index = 0; index < i_TireBrands.Count; index++)
            {
                tiresOnVehicle.Add(new Tyre(i_TireBrands[index], i_TirePressures[index], m_TyrMaxPressureByType[i_VehicleType]));
            }

            i_VehicleToModify.SetTires(tiresOnVehicle);
        }



        public static void AddEvAsEnergySolution(float i_EnergyLeftInHours, Type i_VehicleType, ref Vehicle i_VehicleToModify)
        {
            ElectricVehicleEnergy energySolutionToAssign = new ElectricVehicleEnergy(i_EnergyLeftInHours, m_ElectricMaxHours[i_VehicleType]);

            i_VehicleToModify.SetEnergySolution(energySolutionToAssign);
        }



        public static void AddChemicalEnergySolution(float i_CurrentFuelAmountLittrers, Type i_VehicleType, ref Vehicle i_VehicleToModify)
        {
            ChemicalVehicleEnergy energySolutionToAssign = new ChemicalVehicleEnergy(m_ChemicalFuelType[i_VehicleType], i_CurrentFuelAmountLittrers, m_ChemicalMaxLitters[i_VehicleType]);

            i_VehicleToModify.SetEnergySolution(energySolutionToAssign);
        }



        public static void AddSpecificsInformation(List<string> i_VehicleSpecificsParams, string i_VehicleSpecificsTypeName, ref Vehicle i_VehicleToModify)
        {
            Type typeOfSpecificsToAssign = m_SpecificsNameToType[i_VehicleSpecificsTypeName];

            VehicleSpecifics specificsToAssign = (VehicleSpecifics) Activator.CreateInstance(m_SpecificsNameToType[i_VehicleSpecificsTypeName]);
            specificsToAssign.ParseArguments(i_VehicleSpecificsParams);

            i_VehicleToModify.SetSpecificsInformation(specificsToAssign);
        }
        


        public static void AddOwnerToVehicle(string i_OwnerName, string i_OwnerPhoneNumber, ref Vehicle i_VehicleToModify)
        {
            Owner ownerToAssign = new Owner(i_OwnerName, i_OwnerPhoneNumber);

            i_VehicleToModify.SetOwner(ownerToAssign);
        }
    }
}
