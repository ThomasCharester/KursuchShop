using System;

namespace Resources.Code
{
    public class UICommand
    {
        public String Value;
        public UICommandType Type;

        public UICommand(String value,UICommandType type)
        {
            Value = value;
            Type = type;
        }
        public UICommand(UICommandType type)
        {
            Type = type;
        }
        public void Execute()
        {
            switch (Type)
            {
                case UICommandType.ShowException:
                    UIQuerySender.Instance.ShowException(Value);
                    break;
                case UICommandType.AuthoriseActivate:
                    UIQuerySender.Instance.ToggleAuthorisePanel(true);
                    break;
                case UICommandType.AuthoriseDeactivate:
                    UIQuerySender.Instance.ToggleAuthorisePanel(false);
                    break;
                case UICommandType.ActivateShop:
                    UIQuerySender.Instance.ActivateShop();
                    break;
                case UICommandType.RefreshAccountInfo:
                    UIQuerySender.Instance.RefreshAccountInfo();
                    break;
                case UICommandType.SendAccountRequest:
                    UIQuerySender.Instance.SendAccountsRequest();
                    break;
                case UICommandType.ShowAccountList:
                    UIQuerySender.Instance.ShowAccounts(Value);
                    break;
                case UICommandType.ContinueAccountAdding:
                    UIQuerySender.Instance.ContinueAccountsAdding();
                    break;
                case UICommandType.ContinueGoodsAdding:
                    UIQuerySender.Instance.ContinueGoodsAdding();
                    break;
                case UICommandType.SendQuery:
                    UIQuerySender.Instance.SendQuery(Value);
                    break;
                case UICommandType.ShowGoodList:
                    UIQuerySender.Instance.ShowGoods(Value);
                    break;
                case UICommandType.SendGoodsRequest:
                    UIQuerySender.Instance.SendGoodsRequest();
                    break;
                case UICommandType.ShowGood:
                    UIQuerySender.Instance.ShowGood(Value);
                    break;
                case UICommandType.ShowGoodEdit:
                    UIQuerySender.Instance.ShowGoodEdit(Value);
                    break;
                case UICommandType.ShowGoodAdd:
                    UIQuerySender.Instance.ShowGoodAdd();
                    break;
                case UICommandType.ShowGoodListAP:
                    UIQuerySender.Instance.ShowGoodsAP(Value);
                    break;
                case UICommandType.SendGoodsAPRequest:
                    UIQuerySender.Instance.SendGoodsAPRequest();
                    break;
                case UICommandType.ShowGoodListEdit:
                    UIQuerySender.Instance.ShowGoodsEdit(Value);
                    break;
                case UICommandType.SendGoodsEditRequest:
                    UIQuerySender.Instance.SendGoodsEditRequest();
                    break;
                case UICommandType.MakeAdmin:
                    UIQuerySender.Instance.EnterAdminMode();
                    break;
                case UICommandType.MakeSeller:
                    UIQuerySender.Instance.EnterSellerMode();
                    break;
                case UICommandType.SendGamesRequest:
                    UIQuerySender.Instance.SendGamesRequest();
                    break;
                case UICommandType.ShowGamesList:
                    UIQuerySender.Instance.ShowGames(Value);
                    break;
                case UICommandType.ContinueGamesAdding:
                    UIQuerySender.Instance.ContinueGamesAdding();
                    break;
                case UICommandType.SendPaymentMethodsRequest:
                    UIQuerySender.Instance.SendPaymentMethodsRequest();
                    break;
                case UICommandType.ShowPaymentMethodsList:
                    UIQuerySender.Instance.ShowPaymentMethods(Value);
                    break;
                case UICommandType.ContinuePaymentMethodsAdding:
                    UIQuerySender.Instance.ContinuePaymentMethods();
                    break;
                case UICommandType.SendSellersRequest:
                    UIQuerySender.Instance.SendSellersRequest();
                    break;
                case UICommandType.ShowSellersList:
                    UIQuerySender.Instance.ShowSellers(Value);
                    break;
                case UICommandType.ContinueSellersAdding:
                    UIQuerySender.Instance.ContinueSellersAdding();
                    break;
                case UICommandType.SendAdminKeysRequest:
                    UIQuerySender.Instance.SendAdminKeysRequest();
                    break;
                case UICommandType.ShowAdminKeysList:
                    UIQuerySender.Instance.ShowAdminKeys(Value);
                    break;
                case UICommandType.ContinueAdminKeysAdding:
                    UIQuerySender.Instance.ContinueAdminKeyAdding();
                    break;
            }
        }

        public void Undo()
        {
            
        }
    }
}