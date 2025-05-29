using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Code.DataStructures.LiSa
{
    public class DosagePanel1 : Panel
    {
        [SerializeField] private TMP_Text dosage;
        [SerializeField] private TMP_Dropdown plant;
        [SerializeField] private TMP_Dropdown disease;
        [SerializeField] private TMP_Dropdown medicine;
        [SerializeField] private Slider dosageBar;

        private int stage = 0;

        public new void Show()
        {
            base.Show();
            
            Clear();
            
            PlantStage();
            
            UIQuerySender.Instance.SendGoodsRequest();
        }
        public new void Toggle(bool show)
        {
            base.Toggle(show);
            
            Clear();
            
            if(show) PlantStage();
            
            //if(show) UIQuerySender.Instance.SendGoodsRequest();
        }

        public new void Toggle()
        {
            base.Toggle();
            
            Clear();
            
            PlantStage();
        }
        
        public void Clear()
        {
            plant.ClearOptions();
            plant.RefreshShownValue();
            plant.enabled = true;
            
            disease.ClearOptions();
            disease.RefreshShownValue();
            disease.enabled = false;
            
            medicine.enabled = false;
            medicine.ClearOptions();
            medicine.RefreshShownValue();
            
            dosageBar.enabled = false;
            dosageBar.value = dosageBar.minValue;
        }

        public void UpdateTextValue()
        {
            dosage.text = dosageBar.value.ToString(CultureInfo.InvariantCulture);
        }

        public void SendBUSSINESSQUERY()
        {
            if (stage < 3) return;
            
            UIQuerySender.Instance.SendPointRequest(dosageBar.value, $"|{plant};{disease.value};{medicine.value}");
        }

        public void PlantStage()
        {
            stage = 0;
            plant.AddOptions(SessionService.Plants.Select(x => x.PlantName).ToList());
            plant.RefreshShownValue();

            plant.enabled = true;
        }

        public void DosageStage()
        {
            stage = 3;
            dosageBar.maxValue = SessionService.MedicinesDiseases
                .First(x => x.MedicineId == medicine.value && x.DiseaseId == disease.value).MaxDosage;
            dosageBar.minValue = SessionService.MedicinesDiseases
                .First(x => x.MedicineId == medicine.value && x.DiseaseId == disease.value).MinDosage;

            dosageBar.enabled = true;
        }

        public void DiseaseStage()
        {
            stage = 1;
            int plantId = plant.value;
            var diseaseIds = new HashSet<int>(
                SessionService.PlantsDiseases
                    .Where(x => x.PlantId == plantId)
                    .Select(x => x.DiseaseId)
            );

            disease.AddOptions(SessionService.Diseases
                .Where(x => diseaseIds.Contains(x.Id))
                .Select(x => x.DiseaseName)
                .ToList());

            disease.RefreshShownValue();
            disease.enabled = true;
        }

        public void MedicineStage()
        {
            stage = 2;
            int plantId = plant.value;
            int diseaseId = disease.value;

            var medicinesDiseasesIds = new HashSet<int>(
                SessionService.MedicinesDiseases
                    .Where(x => x.DiseaseId == diseaseId)
                    .Select(x => x.MedicineId)
            );

            var medicinesPlantsIds = new HashSet<int>(
                SessionService.PlantsMedicines
                    .Where(x => x.PlantId == plantId)
                    .Select(x => x.MedicineId)
            );

            var commonMedicineIds = medicinesDiseasesIds.Intersect(medicinesPlantsIds);

            medicine.AddOptions(
                SessionService.Medicines
                    .Where(x => commonMedicineIds.Contains(x.Id))
                    .Select(x => x.MedicineName)
                    .ToList()
            );

            medicine.RefreshShownValue();
            medicine.enabled = true;
        }
    }
}