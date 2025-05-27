using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantDiseaseElement: DataStructureElement
    {
        public PlantDisease PlantDisease = new();

        [SerializeField] private TMP_Text plantName;
        [SerializeField] private TMP_Text diseaseName;
        [SerializeField] private TMP_Text id;

        public void UpdateTextValues()
        {
            plantName.text = SessionService.Plants[PlantDisease.PlantId].PlantName;
            diseaseName.text = SessionService.Diseases[PlantDisease.DiseaseId].DiseaseName;
            id.text = PlantDisease.Id.ToString();
        }
        
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gtpd;" + PlantDisease.GoodToStringS(),
                UICommandType.SendQuery));
        }
    }
}