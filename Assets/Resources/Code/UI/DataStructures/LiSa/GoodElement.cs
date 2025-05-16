using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class GoodElement : DataStructureElement
    {
        public Good good = new();
        [SerializeField] private GameObject outputPanel;
        
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textSeller;
        [SerializeField] private TMP_Text textGame;
        [SerializeField] private TMP_Text textDescription;
        [SerializeField] private TMP_Text textPaymentMethod;
        [SerializeField] private TMP_Text textPrice;
        [SerializeField] private TMP_Text textStock;

        public void UpdateTextValues()
        {
            textName.text = good.goodName;
            textSeller.text = good.sellerName;
            textDescription.text = good.description;
            textPrice.text = good.price.ToString();
        }


    }
}