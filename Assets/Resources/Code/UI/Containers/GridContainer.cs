using System;
using Resources.Code.DataStructures.LiSa;
using UnityEngine;

namespace Resources.Code
{
    public class GridContainer : Container
    {
        // [Header("Prefabs")] 
        // [SerializeField] private GoodElement goodPrefab;
        // [SerializeField] private GoodElement goodEditPrefab;
        //
        // public void ShowPlantsDiseases(String goods)
        // {
        //     if (goods.Length == 0) return;
        //
        //     Clear();
        //
        //     foreach (var plantDisease in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
        //     {
        //         GoodElement temp = Instantiate(goodPrefab, transform);
        //         temp.plantDisease.goodName = plantDisease.Split(DataParsingExtension.ValueSplitter)[0];
        //         temp.plantDisease.sellerName = plantDisease.Split(DataParsingExtension.ValueSplitter)[1];
        //         temp.plantDisease.DiseaseName = plantDisease.Split(DataParsingExtension.ValueSplitter)[2];
        //         temp.plantDisease.description = plantDisease.Split(DataParsingExtension.ValueSplitter)[3];
        //         temp.plantDisease.Medicine = plantDisease.Split(DataParsingExtension.ValueSplitter)[4];
        //         temp.plantDisease.price = int.Parse(plantDisease.Split(DataParsingExtension.ValueSplitter)[5]);
        //         temp.plantDisease.stock = int.Parse(plantDisease.Split(DataParsingExtension.ValueSplitter)[6]);
        //         
        //         temp.UpdateTextValues();
        //         
        //         children.Add(temp);
        //     }
        // }
        // public void ShowGoodsEdit(String goods)
        // {
        //     if (goods.Length == 0) return;
        //
        //     Clear();
        //
        //     foreach (var plantDisease in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
        //     {
        //         GoodElement temp = Instantiate(goodPrefab, transform);
        //         temp.plantDisease.PlantId = plantDisease.Split(DataParsingExtension.ValueSplitter)[0];
        //         temp.plantDisease.sellerNaseId = plantDisease.Split(DataParsingExtension.ValueSplitter)[1];
        //         temp.plantDisease.DiseaseName = plantDisease.Split(DataParsingExtension.ValueSplitter)[2];
        //         temp.plantDisease.description = plantDisease.Split(DataParsingExtension.ValueSplitter)[3];
        //         temp.plantDisease.Medicine = plantDisease.Split(DataParsingExtension.ValueSplitter)[4];
        //         temp.plantDisease.price = int.Parse(plantDisease.Split(DataParsingExtension.ValueSplitter)[5]);
        //         temp.plantDisease.stock = int.Parse(plantDisease.Split(DataParsingExtension.ValueSplitter)[6]);
        //         
        //         temp.UpdateTextValues();
        //         temp.Editable = true;
        //         
        //         children.Add(temp);
        //     }
        // }
    }
}