using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PaymentMethodElement: DataStructureElement
    {
        public PaymentMethod paymentMethod = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldMethodName;
        [SerializeField] private TMP_Text textMethodName;

        public void UpdateTextValues()
        {
            textMethodName.text = paymentMethod.methodName;
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }
        public String FormOutputValue()
        {
            return (inputFieldMethodName.text == "" ? paymentMethod.methodName : inputFieldMethodName.text).DBReadable();
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gpm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + paymentMethod.methodName.DBReadable(),
                UICommandType.SendQuery));
        }

        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gpd;" + paymentMethod.methodName.DBReadable(),
                UICommandType.SendQuery));
        }
    }
}