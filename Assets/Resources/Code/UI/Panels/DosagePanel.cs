using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Resources.Code.DataStructures.LiSa
{
    public class DosagePanel : Panel
    {
        [SerializeField] private TMP_Text dosage;
        [SerializeField] private TMP_Dropdown plant;
        [SerializeField] private TMP_Dropdown disease;
        [SerializeField] private TMP_Dropdown medicine;
        [SerializeField] private Slider dosageBar;

        private int stage = 0;
        private int currentMedicine = 0;
        private int currentPlant = 0;
        private int currentDisease = 0;

        public void Update()
        {
            if(stage == 3)
                UpdateTextValue();
        }

        public new void Show()
        {
            base.Show();

            Clear();

            PlantStage();

            //UIQuerySender.Instance.SendGoodsRequest();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);

            Clear();

            if (show) PlantStage();

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
            currentDisease = 0;
            currentMedicine = 0;

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

            UIQuerySender.Instance.SendPointRequest(dosageBar.value,
                $"|{plant.value};{disease.value};{medicine.value}");
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
            currentMedicine = SessionService.GetMedicineIdByName(medicine.options[medicine.value].text);
            stage = 3;
            dosageBar.maxValue = SessionService.MedicinesDiseases
                .First(x => x.MedicineId == currentMedicine && x.DiseaseId == currentDisease).MaxDosage;
            dosageBar.minValue = SessionService.MedicinesDiseases
                .First(x => x.MedicineId == currentMedicine && x.DiseaseId == currentDisease).MinDosage;

            dosageBar.enabled = true;
        }

        public void DiseaseStage()
        {
            currentPlant = SessionService.GetPlantIdByName(plant.options[plant.value].text);
            
            stage = 1;

            var diseaseIds = new HashSet<int>(
                SessionService.PlantsDiseases
                    .Where(x => x.PlantId == currentPlant)
                    .Select(x => x.DiseaseId)
            );
            var options = SessionService.Diseases
                .Where(x => diseaseIds.Contains(x.Id))
                .Select(x => x.DiseaseName)
                .ToList();
            
            options.Insert(0, "");
            
            disease.AddOptions(options);

            disease.RefreshShownValue();
            disease.enabled = true;
        }

        public void MedicineStage()
        {
            currentDisease = SessionService.GetDiseaseIdByName(disease.options[disease.value].text);
            
            stage = 2;

            var medicinesDiseasesIds = new HashSet<int>(
                SessionService.MedicinesDiseases
                    .Where(x => x.DiseaseId == currentDisease)
                    .Select(x => x.MedicineId)
            );

            var medicinesPlantsIds = new HashSet<int>(
                SessionService.PlantsMedicines
                    .Where(x => x.PlantId == currentPlant)
                    .Select(x => x.MedicineId)
            );

            var commonMedicineIds = medicinesDiseasesIds.Intersect(medicinesPlantsIds);
            
            var options = SessionService.Medicines
                .Where(x => commonMedicineIds.Contains(x.Id))
                .Select(x => x.MedicineName)
                .ToList();
            
            options.Insert(0, "");
            
            medicine.AddOptions(options);

            medicine.RefreshShownValue();
            medicine.enabled = true;
        }
    }
}