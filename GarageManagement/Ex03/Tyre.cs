namespace Ex03.GarageLogic
{
    internal class Tyre
    {
        private readonly string r_ManufecturerName;
        private readonly float r_MaximumPressurePsi;
        private float m_CurrentPressurePsi;

        public Tyre(string i_ManufecturerName, float i_CurrentPressurePsi, float i_MaximumPressurePsi)
        {
            this.r_ManufecturerName = i_ManufecturerName;
            this.r_MaximumPressurePsi = i_MaximumPressurePsi;
            this.m_CurrentPressurePsi = i_CurrentPressurePsi;
        }

        public void FillTireToMax()
        {
            this.m_CurrentPressurePsi = this.r_MaximumPressurePsi;
        }

        public float GetAirPressure()
        {
            return this.m_CurrentPressurePsi;
        }

        public override string ToString()
        {
            return string.Format("Tyre: Maneufactured By {0} , Maximum Pressure {1} [psi] , Current Pressure {2} [psi]", this.r_ManufecturerName, this.r_MaximumPressurePsi, this.m_CurrentPressurePsi);
        }
    }
}