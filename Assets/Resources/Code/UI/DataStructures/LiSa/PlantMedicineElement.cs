using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantMedicineElement: DataStructureElement
    {
        public PlantMedicine PlantMedicine = new();

        [SerializeField] private TMP_Text plantName;
        [SerializeField] private TMP_Text medicineName;

        public void UpdateTextValues()
        {
            plantName.text = SessionService.Plants[PlantMedicine.PlantId].PlantName;
            medicineName.text = SessionService.Medicines[PlantMedicine.MedicineId].MedicineName;
        }
        
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppmd;" + PlantMedicine.PlantMedicineToString(),
                UICommandType.SendQuery));
        }
    }
}