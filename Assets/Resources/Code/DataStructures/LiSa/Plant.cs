using System;

namespace Resources.Code.DataStructures.LiSa
{
    public struct Plant
    {
        public int Id;
        public String PlantName;

        public Plant(int id, String plantName)
        {
            this.Id = id;
            this.PlantName = plantName;
        }
    }
}