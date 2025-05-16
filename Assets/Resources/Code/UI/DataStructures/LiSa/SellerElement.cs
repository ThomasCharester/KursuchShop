using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class SellerElement: DataStructureElement
    {
        public Seller seller = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldName;
        [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldRate;

        [SerializeField] private TMP_Text textLogin;
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textRate;
        
        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }

        public void UpdateTextValues()
        {
            textLogin.text = seller.Login;
            textName.text = seller.Name;
            textRate.text = seller.Rate.ToString();
        }
        
        public String FormOutputValue()
        {
            return (inputFieldName.text == "" ? seller.Name : inputFieldName.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldLogin.text == "" ? seller.Login : inputFieldLogin.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldRate.text == "" ? seller.Rate : inputFieldLogin.text);
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gsm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + seller.SellerToString(),
                UICommandType.SendQuery));
        }
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gsd;" + seller.Login.DBReadable(),
                UICommandType.SendQuery));
        }
    }
}