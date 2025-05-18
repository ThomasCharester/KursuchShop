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

        public static String GoodToStringS(this Good good)
        {
            return good.goodName.DBReadable() + ValueSplitter + good.sellerName.DBReadable() + ValueSplitter +
                   good.gameName.DBReadable();
        }

        public static String DBReadable(this String str)
        {
            return '\'' + str + '\'';
        }

        public static String SellerToString(this Seller seller)
        {
            return seller.Name.DBReadable() + ValueSplitter + seller.Login.DBReadable() + ValueSplitter + seller.Rate;
        }

        public static String GoodToString(this Good good)
        {
            return good.goodName.DBReadable() + ValueSplitter
                                              + good.sellerName.DBReadable() + ValueSplitter
                                              + good.gameName.DBReadable() + ValueSplitter
                                              + good.description.DBReadable() + ValueSplitter
                                              + good.paymentMethod.DBReadable() + ValueSplitter
                                              + good.price + ValueSplitter
                                              + good.stock;
        }
        public static void StringToGood(this Good good, string goodStr)
        {
            var values = goodStr.Split(ValueSplitter);
            good.goodName = values[0];
            good.sellerName = values[1];
            good.gameName = values[2];
            good.description = values[3];
            good.paymentMethod = values[4];
            good.price = int.Parse(values[5]);
            good.stock = int.Parse(values[6]);
        }
    }
}