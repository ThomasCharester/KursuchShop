using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class CartItemElement : DataStructureElement
    {
        public CartItem cartItem = new(0, 0, "NAN", 0);

        [SerializeField] private TMP_Text textMethodName;
        [SerializeField] private TMP_Text textQuantity;

        public void UpdateTextValues()
        {
            textMethodName.text = cartItem.GoodName;
            textQuantity.text = cartItem.Quantity.ToString();
        }

        public void SendModifyQuery(int count)
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gcia;" + SessionService.UserAccount.Login.DBReadable() +
                DataParsingExtension.AdditionalQuerySplitter + cartItem.GoodName.DBReadable() +
                DataParsingExtension.ValueSplitter + count,
                UICommandType.SendQuery));
        }

        public void SendRemoveQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gcid;" + cartItem.CartId + DataParsingExtension.ValueSplitter + cartItem.ItemId,
                UICommandType.SendQuery));
        }
    }
}