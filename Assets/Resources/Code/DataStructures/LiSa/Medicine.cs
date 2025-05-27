using System;

namespace Resources.Code.DataStructures.LiSa
{
    public struct Medicine
    {
        public int Id;
        public String MedicineName;
        public int Concentration;

        public Medicine(int id, String medicineName, int concentration)
        {
            Id = id;
            MedicineName = medicineName;
            Concentration = concentration;
        }
    }
}