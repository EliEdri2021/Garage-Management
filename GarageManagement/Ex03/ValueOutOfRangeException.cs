using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException: Exception
    {
        // $G$ CSS-999 (-3) Members should be in form of m_PascalCased
        private readonly float maxValue;
        private readonly float minValue;
        public new string Message;
        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            this.maxValue = i_MaxValue;
            this.minValue = i_MinValue;
            this.Message = String.Format("Value Out Of Expected Range: ( Minimum {0} - Maximum {1} )", this.minValue, this.maxValue);
        }

        public float getMax()
        {
            return this.maxValue;
        }

        public float getMin()
        {
            return this.minValue;
        }
    }

}
