using System;
using System.Linq;
using Resources.Code.DataStructures.LiSa;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.UI.Panels
{
    public class GoodPanel : Panel
    {
        public Good good = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private GameObject deleteButton;
        [SerializeField] private GameObject submitButton;

        public bool AddMode;

        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textSeller;
        [SerializeField] private TMP_Text textGame;
        [SerializeField] private TMP_Text textDescription;
        [SerializeField] private TMP_Text textPaymentMethod;
        [SerializeField] private TMP_Text textPrice;
        [SerializeField] private TMP_Text textStock;

        [SerializeField] private TMP_Dropdown dropdownGameName;

        [SerializeField] private TMP_Dropdown dropdownPaymentMethodName;

        [SerializeField] private TMP_InputField inputFieldName;

        // [SerializeField] private TMP_InputField inputFieldGame;
        [SerializeField] private TMP_InputField inputFieldDescription;

        // [SerializeField] private TMP_InputField inputFieldPaymentMethod;
        [SerializeField] private TMP_InputField inputFieldPrice;
        [SerializeField] private TMP_InputField inputFieldStock;

        public void UpdateTextValues()
        {
            textName.text = good.goodName;
            textSeller.text = good.sellerName;
            textDescription.text = good.description;
            textPrice.text = good.price.ToString();
            textGame.text = good.gameName;
            textPaymentMethod.text = good.paymentMethod;
            textPrice.text = good.price.ToString();
            textStock.text = good.stock.ToString();
        }

        public void UpdateInputFieldsValues()
        {
            inputFieldName.text = good.goodName;
            inputFieldDescription.text = good.description;
            inputFieldPrice.text = good.price.ToString();
            dropdownGameName.ClearOptions();
            dropdownGameName.AddOptions(SessionService.Games.Select(x => x.gameName).ToList());
            // dropdownGameName.RefreshShownValue();
            dropdownPaymentMethodName.ClearOptions();
            dropdownPaymentMethodName.AddOptions(SessionService.PaymentMethods.Select(x => x.methodName).ToList());
            // dropdownPaymentMethodName.RefreshShownValue();
            // inputFieldGame.text = good.gameName;
            // inputFieldPaymentMethod.text = good.paymentMethod;
            inputFieldPrice.text = good.price.ToString();
            inputFieldStock.text = good.stock.ToString();
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);

            if (edit) deleteButton.SetActive(!AddMode);

            ToggleInputs(edit);
        }

        public String FormOutputValue()
        {
            return (inputFieldName.text == "" ? good.goodName : inputFieldName.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + good.sellerName.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + SessionService.Games[dropdownGameName.value].gameName.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldDescription.text == "" ? good.description : inputFieldDescription.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + SessionService.PaymentMethods[dropdownPaymentMethodName.value].methodName.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldPrice.text == "" ? good.price : inputFieldPrice.text)
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldStock.text == "" ? good.stock : inputFieldStock.text);
        }

        public String FormAddOutputValue()
        {
            return (inputFieldName.text == "" ? good.goodName : inputFieldName.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + SessionService.Games[dropdownGameName.value].gameName.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldDescription.text == "" ? good.description : inputFieldDescription.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + SessionService.PaymentMethods[dropdownPaymentMethodName.value].methodName.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldPrice.text == "" ? good.price : inputFieldPrice.text)
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldStock.text == "" ? good.stock : inputFieldStock.text);
        }

        public void SubmitQuery()
        {
            if (AddMode) SendAddQuery();
            else SendModifyQuery();

            Hide();
        }

        public void SendModifyQuery()
        {
            // TODO Totally unsecure loL
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gtsm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + good.GoodToString(),
                UICommandType.SendQuery));
        }

        public void SendCartAddQuery()
        {
            // TODO Totally unsecure loL
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gcia;" + SessionService.UserAccount.Login.DBReadable() +
                DataParsingExtension.AdditionalQuerySplitter + good.goodName.DBReadable() +
                DataParsingExtension.ValueSplitter + '1',
                UICommandType.SendQuery));
        }

        public void SendAddQuery()
        {
            if (inputFieldName.text == "" ||
                inputFieldPrice.text == "" ||
                inputFieldStock.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Some field is empty",
                    UICommandType.ShowException));
            }

            // TODO Totally unsecure loL
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gtsa;" + SessionService.UserAccount.Login.DBReadable() +
                DataParsingExtension.AdditionalQuerySplitter + FormAddOutputValue(),
                UICommandType.SendQuery));

            ToggleInputs(false);
        }

        public void SendDeleteQuery()
        {
            // TODO Totally unsecure loL
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gtsd;" + good.GoodToStringS(),
                UICommandType.SendQuery));

            Hide();
        }

        public new void Show()
        {
            base.Show();

            Clear();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);

            if (show)
                Clear();
        }

        public new void Toggle()
        {
            base.Toggle();

            Clear();
        }

        public void Clear()
        {
            inputFieldName.text = "";
            dropdownGameName.value = 0;
            inputFieldDescription.text = "";
            dropdownPaymentMethodName.value = 0;
            inputFieldPrice.text = "";
            inputFieldStock.text = "";

            textName.text = "";
            textSeller.text = "";
            textGame.text = "";
            textDescription.text = "";
            textPaymentMethod.text = "";
            textPrice.text = "";
            textStock.text = "";
        }

        public void ToggleInputs(bool mode)
        {
            inputFieldName.enabled = mode;
            dropdownGameName.enabled = mode;
            inputFieldDescription.enabled = mode;
            dropdownPaymentMethodName.enabled = mode;
            inputFieldPrice.enabled = mode;
            inputFieldStock.enabled = mode;
        }
    }
}