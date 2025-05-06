using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Code.DataStructures;
using UnityEditor.PackageManager;

namespace Resources.Code
{
    using System.Text;

    public static class DataParsingExtension
    {
        public static readonly char ValueSplitter = ',';
        public static readonly char QuerySplitter = ';';
        public static readonly char AdditionalQuerySplitter = '|';

        public static readonly String ATableName = "Accounts";
        public static readonly String AKTableName = "adminKeys";

        public static readonly String ATTableName = "AquireTypes";
        public static readonly String GATableName = "Games";
        public static readonly String PMTableName = "PaymentMethods";
        public static readonly String STableName = "Sellers";
        public static readonly String GOTableName = "Goods";

        public const string DiseasesTable = "Diseases";
        public const string MedicinesTable = "Medicines";
        public const string PlantsTable = "Plants";
        public const string MedicineDiseasesTable = "MedicineDiseases";
        public const string PlantMedicinesTable = "PlantMedicines";
        public const string DosagesTable = "Dosages";

        public static String AccountToString(this Account account)
        {
            return $"{account.login}{ValueSplitter}{account.password}{ValueSplitter}{account.adminKey}";
        }

        public static String AccountToStringDB(this Account account)
        {
            return $"\'{account.login}\'{ValueSplitter}\'{account.password}\'{ValueSplitter}\'{account.adminKey}\'";
        }

        public static void StringToAccount(this String accountStr, Account account)
        {
            account.SetValues(accountStr.Split(ValueSplitter)[0], accountStr.Split(ValueSplitter)[1],
                accountStr.Split(ValueSplitter)[2]);
        }

        public static void StringToAccountLP(this String accountStr, Account account)
        {
            account.SetValues(accountStr.Split(ValueSplitter)[0], accountStr.Split(ValueSplitter)[1], "NAN");
        }

        public static String AccountsToString(this List<Account> accounts)
        {
            StringBuilder builder = new();
            foreach (Account account in accounts)
                builder.Append(account.AccountToStringDB() + AdditionalQuerySplitter);
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        // Для Diseases
        public static string DiseaseToString(this Disease disease)
        {
            return $"{disease.diseaseId}{ValueSplitter}{disease.diseaseName}";
        }

        public static string DiseaseToStringDB(this Disease disease)
        {
            return $"'{disease.diseaseName}'";
        }

        public static string DiseasesToString(this List<Disease> diseases)
        {
            return string.Join(AdditionalQuerySplitter.ToString(), diseases.Select(d => d.DiseaseToString()));
        }

// Для Medicine
        public static string MedicineToString(this Medicine medicine)
        {
            return $"{medicine.medicineId}{ValueSplitter}{medicine.medicineName}";
        }

        public static string MedicineToStringDB(this Medicine medicine)
        {
            return $"'{medicine.medicineName}'";
        }

        public static string MedicinesToString(this List<Medicine> medicines)
        {
            return string.Join(AdditionalQuerySplitter.ToString(), medicines.Select(m => m.MedicineToString()));
        }

// Для Plants
        public static string PlantToString(this Plant plant)
        {
            return $"{plant.plantId}{ValueSplitter}{plant.plantName}";
        }

        public static string PlantToStringDB(this Plant plant)
        {
            return $"'{plant.plantName}'";
        }

        public static string PlantsToString(this List<Plant> plants)
        {
            return string.Join(AdditionalQuerySplitter.ToString(), plants.Select(p => p.PlantToString()));
        }

// Для PlantMedicine (связи растения-лекарства)
        public static string PlantMedicineToString(this PlantMedicine pm)
        {
            return $"{pm.plantId}{ValueSplitter}{pm.medicineId}";
        }

        public static string PlantMedicineToStringDB(this PlantMedicine pm)
        {
            return $"'{pm.plantId}'{ValueSplitter}'{pm.medicineId}'";
        }

        public static string PlantMedicinesToString(this List<PlantMedicine> pms)
        {
            return string.Join(AdditionalQuerySplitter.ToString(), pms.Select(pm => pm.PlantMedicineToString()));
        }

// Для MedicineDiseases (связи лекарства-болезни)
        public static string MedicineDiseaseToString(this MedicineDisease md)
        {
            return $"{md.medicineId}{ValueSplitter}{md.diseaseId}";
        }

        public static string MedicineDiseaseToStringDB(this MedicineDisease md)
        {
            return $"'{md.medicineId}'{ValueSplitter}'{md.diseaseId}'";
        }

        public static string MedicineDiseasesToString(this List<MedicineDisease> mds)
        {
            return string.Join(AdditionalQuerySplitter.ToString(), mds.Select(md => md.MedicineDiseaseToString()));
        }

// Для Dosages (дозировки)
        public static string DosageToString(this Dosage dosage)
        {
            return $"{dosage.medicineId}{ValueSplitter}{dosage.diseaseId}{ValueSplitter}" +
                   $"{dosage.minDosage}{ValueSplitter}{dosage.maxDosage}";
        }

        public static string DosageToStringDB(this Dosage dosage)
        {
            return $"'{dosage.medicineId}'{ValueSplitter}'{dosage.diseaseId}'{ValueSplitter}" +
                   $"{dosage.minDosage}{ValueSplitter}{dosage.maxDosage}";
        }

        public static string DosagesToString(this List<Dosage> dosages)
        {
            return string.Join(AdditionalQuerySplitter.ToString(), dosages.Select(d => d.DosageToString()));
        }// Для Diseases
        public static void StringToDisease(this String diseaseStr, Disease disease)
        {
            var parts = diseaseStr.Split(ValueSplitter);
            disease.diseaseId = int.Parse(parts[0]);
            disease.diseaseName = parts[1];
        }

// Для Medicines
        public static void StringToMedicine(this String medicineStr, Medicine medicine)
        {
            var parts = medicineStr.Split(ValueSplitter);
            medicine.medicineId = int.Parse(parts[0]);
            medicine.medicineName = parts[1];
        }

// Для Plants
        public static void StringToPlant(this String plantStr, Plant plant)
        {
            var parts = plantStr.Split(ValueSplitter);
            plant.plantId = int.Parse(parts[0]);
            plant.plantName = parts[1];
        }

// Для PlantMedicines (связи растения-лекарства)
        public static void StringToPlantMedicine(this String pmStr, PlantMedicine pm)
        {
            var parts = pmStr.Split(ValueSplitter);
            pm.plantId = int.Parse(parts[0]);
            pm.medicineId = int.Parse(parts[1]);
        }

// Для MedicineDiseases (связи лекарства-болезни)
        public static void StringToMedicineDisease(this String mdStr, MedicineDisease md)
        {
            var parts = mdStr.Split(ValueSplitter);
            md.medicineId = int.Parse(parts[0]);
            md.diseaseId = int.Parse(parts[1]);
        }

// Для Dosages (дозировки)
        public static void StringToDosage(this String dosageStr, Dosage dosage)
        {
            var parts = dosageStr.Split(ValueSplitter);
            dosage.medicineId = int.Parse(parts[0]);
            dosage.diseaseId = int.Parse(parts[1]);
            dosage.minDosage = float.Parse(parts[2]);
            dosage.maxDosage = float.Parse(parts[3]);
        }
    }
}