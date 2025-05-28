using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class MedicineDiseaseElement: DataStructureElement
    {
        public MedicineDisease MedicineDisease = new();

        [SerializeField] private TMP_Text medicineName;
        [SerializeField] private TMP_Text diseaseName;
        [SerializeField] private TMP_Text minDosage;
        [SerializeField] private TMP_Text maxDosage;

        public void UpdateTextValues()
        {
            medicineName.text = SessionService.Medicines[MedicineDisease.MedicineId].MedicineName;
            diseaseName.text = SessionService.Diseases[MedicineDisease.DiseaseId].DiseaseName;
            minDosage.text = MedicineDisease.MinDosage.ToString();
            maxDosage.text = MedicineDisease.MaxDosage.ToString();
        }
        
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pmdd;" + MedicineDisease.MedicineDiseaseToStringS(),
                UICommandType.SendQuery));
        }
    }
}