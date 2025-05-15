using System;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class GoodElement : MonoBehaviour, IUIElement
    {
        public Good good = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldName;
        [SerializeField] private TMP_InputField inputFieldGame;
        [SerializeField] private TMP_InputField inputFieldDescription;
        [SerializeField] private TMP_InputField inputFieldPaymentMethod;
        [SerializeField] private TMP_InputField inputFieldPrice;
        [SerializeField] private TMP_InputField inputFieldStock;

        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textSeller;
        [SerializeField] private TMP_Text textGame;
        [SerializeField] private TMP_Text textDescription;
        [SerializeField] private TMP_Text textPaymentMethod;
        [SerializeField] private TMP_Text textPrice;
        [SerializeField] private TMP_Text textStock;
        
        [SerializeField] private UIScaling _uiScaling;

        private void Start()
        {
            _uiScaling.StartAnimation();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Clear()
        {
            _uiScaling.DIE();
        }

        public void Toggle(bool show)
        {
            gameObject.SetActive(show);
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }

        public void UpdateTextValues()
        {
            textName.text = good.goodName;
            textSeller.text = good.sellerName;
            //textGame.text = good.gameName;
            textDescription.text = good.description;
            //textPaymentMethod.text = good.paymentMethod;
            textPrice.text = good.price.ToString();
            //textStock.text = good.stock.ToString();
        }

        public void SetInputValues()
        {
            good.goodName = inputFieldName.text;
            good.gameName = inputFieldGame.text;
            good.description = inputFieldDescription.text;
            good.paymentMethod = inputFieldPaymentMethod.text;
            good.price = int.Parse(inputFieldPrice.text);
            good.stock = int.Parse(inputFieldStock.text);
        }

        public void SendAddQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gta;",
                UICommandType.SendQuery));
            UIQuerySender.Instance.ContinueGoodsAdding();
        }
    }
}