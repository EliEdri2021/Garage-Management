using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Bike : VehicleSpecifics
    {
        public enum RegistationTypeOptions
        {
            A,
            AA,
            B,
            BB
        }

        private int m_EngineDisplacement;
        private RegistationTypeOptions m_RegistationType;

        public Bike()
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
                this.m_EngineDisplacement = int.Parse(i_ArgumentsToParse[0]);
            }
            catch (System.ArgumentException)
            {
                throw new System.FormatException("The Argument For Bike Engine Displacemnt Is Not Formatted Correctly");
            }

            try
            {
                this.m_RegistationType = (RegistationTypeOptions)Enum.Parse(typeof(RegistationTypeOptions), i_ArgumentsToParse[1]);
            }
            catch (System.ArgumentException)
            {
                throw new System.FormatException("The Argument For Bike Registation Type Is Not Formatted Correctly");
            }
        }

        public override string ToString()
        {
            return string.Format("Bike: Engine Displacement {0} , Registation Type {1} ", this.m_EngineDisplacement, m_RegistationType);
        }
    }
}