using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : VehicleSpecifics
    {
        private bool m_IsCargoContainmentCooled;
        private float m_MaximumUsableWeight;

        // $G$ DSN-999 (0) No need to implement an empty default constructor, it's already given by class definition.
        public Truck()
        {

        }

        public override void ParseArguments(List<string> i_ArgumentsToParse)
        {
            if (i_ArgumentsToParse.Count != 2)
            {
                throw new System.ArgumentException("Parameter List Length Mismatch");
            }

            try
            {
                this.m_IsCargoContainmentCooled = bool.Parse(i_ArgumentsToParse[0]);
            }
            catch (System.ArgumentException)
            {
                throw new System.FormatException("The Argument For Truck Cargo Cooling Is Not Formatted Correctly");
            }

            try
            {
                this.m_MaximumUsableWeight = float.Parse(i_ArgumentsToParse[1]);
            }
            catch (System.ArgumentException)
            {
                throw new System.FormatException("The Argument For Truck Max Usable Weight Is Not Formatted Correctly");
            }
        }

        public override string ToString()
        {
            return string.Format("Truck: Is Cargo Cooled {0} , Maximum Usable Weight {1}", this.m_IsCargoContainmentCooled, this.m_MaximumUsableWeight);
        }
    }
}