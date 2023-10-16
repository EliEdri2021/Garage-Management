using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : VehicleSpecifics
    {
        public enum eDoorCountOptions
        {
            Two,
            Three,
            Four,
            Five
        }
        public enum eCarColorOptions
        {
            White,
            Grey,
            Black,
            Blue
        }

        private eCarColorOptions m_CarColor;
        private eDoorCountOptions m_DoorCount;

        public Car()
        {

        }

        public override void ParseArguments(List<string> i_ArgumentsToParse)
        {
            if(i_ArgumentsToParse.Count != 2)
            {
                throw new ArgumentException("Parameter List Length Mismatch");
            }

            try
            {
                this.m_DoorCount = (eDoorCountOptions)Enum.Parse(typeof(eDoorCountOptions), i_ArgumentsToParse[0]);
            }
            catch (ArgumentException)
            {
                throw new FormatException("The Argument For Car Color Is Not Formatted Correctly");
            }

            try
            {
                this.m_CarColor = (eCarColorOptions)Enum.Parse(typeof(eCarColorOptions), i_ArgumentsToParse[1]);

            }
            catch (ArgumentException)
            {
                throw new FormatException("The Argument For Door Count Is Not Formatted Correctly");
            }
        }

        public override string ToString()
        {
            return string.Format("Car: Car Color {0} , Car Door Count {1}", this.m_CarColor, this.m_DoorCount);
        }
    }
}