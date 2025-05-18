using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Code.DataStructures.LiSa
{
    public class GoodElement : DataStructureElement, IPointerClickHandler
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

        public bool Editable;
        public void UpdateTextValues()
        {
            textName.text = good.goodName;
            textSeller.text = good.sellerName;
            textDescription.text = good.description;
            textPrice.text = good.price.ToString();
        }
        
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if(Editable) UIQuerySender.Instance.ShowGoodEdit(good);
            else UIQuerySender.Instance.ShowGood(good);
        }
        
    }
}