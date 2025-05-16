using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class GoodElementAP: DataStructureElement
    {
        public Good good = new();

        [SerializeField] private TMP_Text goodName;
        [SerializeField] private TMP_Text sellerName;
        [SerializeField] private TMP_Text gameName;

        public void UpdateTextValues()
        {
            goodName.text = good.goodName;
            sellerName.text = good.sellerName;
            gameName.text = good.gameName;
        }
        
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gtpd;" + good.GoodToString(),
                UICommandType.SendQuery));
        }
    }
}