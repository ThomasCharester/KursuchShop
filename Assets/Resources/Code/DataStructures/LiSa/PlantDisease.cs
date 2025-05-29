using System;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantDisease
    {
        public int PlantId;
        public int DiseaseId;

        public PlantDisease(int i, int i1)
        {
            PlantId = i;
            DiseaseId = i1;
        }
        public PlantDisease()
        {
            PlantId = 0;
            DiseaseId = 0;
        }
    }
}