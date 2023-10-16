using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    abstract class VehicleSpecifics
    {
        System.Type VehicleObjectType;

        public abstract void ParseArguments(List<string> i_ArgumentsToParse);
        public override abstract string ToString();
    }
}