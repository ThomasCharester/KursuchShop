using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;
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

        public static String DBReadable(this String str)
        {
            return '\'' + str + '\'';
        }

        public static String GoodToString(this Good good)
        {
            return good.goodName.DBReadable() + ValueSplitter
                                              + good.sellerName.DBReadable() + ValueSplitter
                                              + good.gameName.DBReadable() + ValueSplitter
                                              + good.description.DBReadable() + ValueSplitter
                                              + good.paymentMethod.DBReadable() + ValueSplitter
                                              + good.stock + ValueSplitter
                                              + good.price;
        }
    }
}