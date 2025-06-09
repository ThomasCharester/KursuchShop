using System;
using Resources.Code.DataStructures.LiSa;
using UnityEngine;

namespace Resources.Code
{
    public class GridContainer : Container
    {
        [Header("Prefabs")] 
        [SerializeField] private GoodElement goodPrefab;
        [SerializeField] private GoodElement goodEditPrefab;

        public void ShowGoods(String goods)
        {
            Clear();

            if (goods.Length == 0) return;

            foreach (var good in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                GoodElement temp = Instantiate(goodPrefab, transform);
                temp.good.goodName = good.Split(DataParsingExtension.ValueSplitter)[0];
                temp.good.sellerName = good.Split(DataParsingExtension.ValueSplitter)[1];
                temp.good.gameName = good.Split(DataParsingExtension.ValueSplitter)[2];
                temp.good.description = good.Split(DataParsingExtension.ValueSplitter)[3];
                temp.good.paymentMethod = good.Split(DataParsingExtension.ValueSplitter)[4];
                temp.good.price = int.Parse(good.Split(DataParsingExtension.ValueSplitter)[5]);
                temp.good.stock = int.Parse(good.Split(DataParsingExtension.ValueSplitter)[6]);
                
                temp.UpdateTextValues();
                
                children.Add(temp);
            }
        }
        public void ShowGoodsEdit(String goods)
        {
            Clear();

            if (goods.Length == 0) return;

            foreach (var good in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                GoodElement temp = Instantiate(goodPrefab, transform);
                temp.good.goodName = good.Split(DataParsingExtension.ValueSplitter)[0];
                temp.good.sellerName = good.Split(DataParsingExtension.ValueSplitter)[1];
                temp.good.gameName = good.Split(DataParsingExtension.ValueSplitter)[2];
                temp.good.description = good.Split(DataParsingExtension.ValueSplitter)[3];
                temp.good.paymentMethod = good.Split(DataParsingExtension.ValueSplitter)[4];
                temp.good.price = int.Parse(good.Split(DataParsingExtension.ValueSplitter)[5]);
                temp.good.stock = int.Parse(good.Split(DataParsingExtension.ValueSplitter)[6]);
                
                temp.UpdateTextValues();
                temp.Editable = true;
                
                children.Add(temp);
            }
        }
    }
}