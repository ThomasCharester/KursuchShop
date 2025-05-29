using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;

namespace Resources.Code
{
    using System.Text;

    public static class DataParsingExtension
    {
        public static readonly char ValueSplitter = ',';
        public static readonly char QuerySplitter = ';';
        public static readonly char AdditionalQuerySplitter = '|';


        public static readonly String ATableName = "Accounts";
        public static readonly String AKTableName = "AdminKeys";
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

        public static Account StringSToAccount(this String account)
        {
            return new Account(account.Split(ValueSplitter)[0], account.Split(ValueSplitter)[1], false);
        }

        public static String AccountToStringS(this Account account)
        {
            return account.Login + ValueSplitter + account.Password;
        }

        public static String AccountToString(this Account account)
        {
            return account.Login + ValueSplitter + account.Password + ValueSplitter + account.AdminKey;
        }

        public static String AccountToStringDB(this Account account)
        {
            return account.Login.DBReadable() + ValueSplitter + account.Password.DBReadable() + ValueSplitter +
                   account.AdminKey.DBReadable();
        }

        public static String PlantDiseaseToString(this PlantDisease plantDisease)
        {
            return plantDisease.PlantId.ToString() 
                   + ValueSplitter 
                   + plantDisease.DiseaseId.ToString();
        }
        public static String PlantMedicineToString(this PlantMedicine plantMedicine)
        {
            return plantMedicine.PlantId.ToString() 
                   + ValueSplitter 
                   + plantMedicine.MedicineId.ToString();
        }
        public static String MedicineDiseaseToStringS(this MedicineDisease medicineDisease)
        {
            return medicineDisease.DiseaseId.ToString()
                   + ValueSplitter
                   + medicineDisease.MedicineId.ToString() ;
        }

        public static String DBReadable(this String str)
        {
            return '\'' + str + '\'';
        }

        public static String PlantToString(this Plant plant)
        {
            return plant.PlantName.DBReadable();
        }
    }
}