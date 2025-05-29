namespace Resources.Code.DataStructures.LiSa
{
    public struct MedicineDisease
    {
        public int DiseaseId;
        public int MedicineId;
        public float MinDosage;
        public float MaxDosage;

        public MedicineDisease(int i, int i1, float f1, float f2)
        {
            DiseaseId = i;
            MedicineId = i1;
            MinDosage = f1;
            MaxDosage = f2;
        }
    }
}