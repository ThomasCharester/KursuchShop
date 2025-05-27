using System;

namespace Resources.Code.DataStructures.LiSa
{
    public struct Disease
    {
        public int Id;
        public String DiseaseName;
        
        public Disease(int id, String diseaseName)
        {
            this.Id = id;
            this.DiseaseName = diseaseName;
        }
    }
}