using System;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code
{
    public class VerticalContainer : Container
    {
        [Header("Prefabs")] [SerializeField] private AccountElement accountPrefab;
        [SerializeField] private AccountAddElement accountAddPrefab;
        [SerializeField] private DiseaseElement diseasePrefab;
        [SerializeField] private DiseaseAddElement diseaseAddPrefab;
        [SerializeField] private MedicineElement mPrefab;
        [SerializeField] private MedicineAdd mAddPrefab;
        [SerializeField] private PlantElement plantPrefab;
        [SerializeField] private PlantAddElement plantAddPrefab;
        [SerializeField] private AdminKeyElement adminKeyPrefab;
        [SerializeField] private AdminKeyAddElement adminKeyAddPrefab;
        [SerializeField] private PlantDiseaseElement pdPrefab;
        [SerializeField] private PlantDiseaseAddElement pdaPrefab;
        [SerializeField] private PlantMedicineElement pmPrefab;
        [SerializeField] private PlantMedicineAddElement pmaPrefab;
        [SerializeField] private MedicineDiseaseElement mdPrefab;
        [SerializeField] private MedicineDiseaseAddElement mdaPrefab;

        public void ShowAccounts(String accounts)
        {
            Clear();

            if (accounts.Length == 0) return;

            foreach (var account in accounts.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                AccountElement temp = Instantiate(accountPrefab, transform);
                temp.account.Login = account.Split(DataParsingExtension.ValueSplitter)[0];
                temp.account.Password = account.Split(DataParsingExtension.ValueSplitter)[1];
                temp.account.AdminKey = account.Split(DataParsingExtension.ValueSplitter)[2];

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartAccountsEdit(String accounts)
        {
            ShowAccounts(accounts);

            ContinueAccountsEdit();
        }

        public void ContinueAccountsEdit() => children.Add(Instantiate(accountAddPrefab, transform));

        public void ShowDiseases(String diseases)
        {
            Clear();

            if (diseases.Length == 0) return;

            foreach (var disease in diseases.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                DiseaseElement temp = Instantiate(diseasePrefab, transform);
                temp.Disease.Id = int.Parse(disease.Split(DataParsingExtension.ValueSplitter)[0]);
                temp.Disease.DiseaseName = disease.Split(DataParsingExtension.ValueSplitter)[1];

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartDiseasesEdit(String diseases)
        {
            ShowDiseases(diseases);

            ContinueDiseaseEdit();
        }

        public void ContinueDiseaseEdit() => children.Add(Instantiate(diseaseAddPrefab, transform));

        public void ShowMedicines(String medicines)
        {
            Clear();

            if (medicines.Length == 0) return;


            foreach (var medicine in medicines.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                MedicineElement temp = Instantiate(mPrefab, transform);
                temp.Medicine.Id = int.Parse(medicine.Split(DataParsingExtension.ValueSplitter)[0]);
                temp.Medicine.MedicineName = medicine.Split(DataParsingExtension.ValueSplitter)[1];
                temp.Medicine.Concentration = int.Parse(medicine.Split(DataParsingExtension.ValueSplitter)[2]);

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartMedicinesEdit(String medicines)
        {
            ShowMedicines(medicines);

            ContinueMedicinesEdit();
        }

        public void ContinueMedicinesEdit() => children.Add(Instantiate(mAddPrefab, transform));

        public void ShowPlants(String plants)
        {
            Clear();

            if (plants.Length == 0) return;

            foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                PlantElement temp = Instantiate(plantPrefab, transform);
                temp.Plant.Id = int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]);
                temp.Plant.PlantName = plant.Split(DataParsingExtension.ValueSplitter)[1];

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartPlantsEdit(String plants)
        {
            ShowPlants(plants);

            ContinuePlantsEdit();
        }

        public void ContinuePlantsEdit() => children.Add(Instantiate(plantAddPrefab, transform));

        public void ShowAdminKey(String adminKeys)
        {
            Clear();

            if (adminKeys.Length == 0) return;

            foreach (var adminKey in adminKeys.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                AdminKeyElement temp = Instantiate(adminKeyPrefab, transform);
                temp.adminKey.adminKey = adminKey;

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartAdminKey(String games)
        {
            ShowAdminKey(games);

            ContinueAdminKeyEdit();
        }

        public void ContinueAdminKeyEdit() => children.Add(Instantiate(adminKeyAddPrefab, transform));

        public void ShowPlantsDiseases(String goods)
        {
            Clear();

            if (goods.Length == 0) return;

            foreach (var plantDisease in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                PlantDiseaseElement temp = Instantiate(pdPrefab, transform);
                temp.PlantDisease.PlantId = int.Parse(plantDisease.Split(DataParsingExtension.ValueSplitter)[0]);
                temp.PlantDisease.DiseaseId = int.Parse(plantDisease.Split(DataParsingExtension.ValueSplitter)[1]);

                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartPlantsDiseasesEdit(String plantsDiseases)
        {
            ShowPlantsDiseases(plantsDiseases);

            ContinuePlantsDiseasesEdit();
        }

        public void ContinuePlantsDiseasesEdit() => children.Add(Instantiate(pdaPrefab, transform));
        public void ShowPlantsMedicines(String medicine)
        {
            Clear();

            if (medicine.Length == 0) return;
            
            foreach (var plantMedicine in medicine.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                PlantMedicineElement temp = Instantiate(pmPrefab, transform);
                temp.PlantMedicine.PlantId = int.Parse(plantMedicine.Split(DataParsingExtension.ValueSplitter)[0]);
                temp.PlantMedicine.MedicineId = int.Parse(plantMedicine.Split(DataParsingExtension.ValueSplitter)[1]);

                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartPlantsMedicinesEdit(String plantsMedicines)
        {
            ShowPlantsMedicines(plantsMedicines);

            ContinuePlantsMedicinesEdit();
        }

        public void ContinuePlantsMedicinesEdit() => children.Add(Instantiate(pmaPrefab, transform));
        public void ShowMedicinesDiseases(String medicine)
        {
            Clear();

            if (medicine.Length == 0) return;
            
            foreach (var plantMedicine in medicine.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                MedicineDiseaseElement temp = Instantiate(mdPrefab, transform);
                temp.MedicineDisease.DiseaseId = int.Parse(plantMedicine.Split(DataParsingExtension.ValueSplitter)[0]);
                temp.MedicineDisease.MedicineId = int.Parse(plantMedicine.Split(DataParsingExtension.ValueSplitter)[1]);
                temp.MedicineDisease.MinDosage = float.Parse(plantMedicine.Split(DataParsingExtension.ValueSplitter)[2]);
                temp.MedicineDisease.MaxDosage = float.Parse(plantMedicine.Split(DataParsingExtension.ValueSplitter)[3]);

                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartMedicinesDiseasesEdit(String medicinesDiseases)
        {
            ShowMedicinesDiseases(medicinesDiseases);

            ContinueMedicinesDiseasesEdit();
        }

        public void ContinueMedicinesDiseasesEdit() => children.Add(Instantiate(mdaPrefab, transform));

    }
}