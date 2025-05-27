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
                case UICommandType.ContinuePlantsDiseasesAdding:
                    UIQuerySender.Instance.ContinueGoodsAdding();
                    break;
                case UICommandType.SendQuery:
                    UIQuerySender.Instance.SendQuery(Value);
                    break;
                case UICommandType.ShowGoodList:
                    // UIQuerySender.Instance.ShowPlantsDiseases(Value);
                    break;
                case UICommandType.SendGoodsRequest:
                    UIQuerySender.Instance.SendGoodsRequest();
                    break;
                case UICommandType.ShowGoodListAP:
                    UIQuerySender.Instance.ShowGoodsAP(Value);
                    break;
                case UICommandType.SendGoodsAPRequest:
                    UIQuerySender.Instance.SendGoodsAPRequest();
                    break;
                case UICommandType.ShowGoodListEdit:
                    // UIQuerySender.Instance.ShowGoodsEdit(Value);
                    break;
                case UICommandType.SendGoodsEditRequest:
                    UIQuerySender.Instance.SendGoodsEditRequest();
                    break;
                case UICommandType.MakeAdmin:
                    UIQuerySender.Instance.EnterAdminMode();
                    break;
                case UICommandType.SendDiseasesRequest:
                    UIQuerySender.Instance.SendDiseasesRequest();
                    break;
                case UICommandType.ShowDiseasesList:
                    UIQuerySender.Instance.ShowDiseases(Value);
                    break;
                case UICommandType.ContinueDiseasesAdding:
                    UIQuerySender.Instance.ContinueDiseasesAdding();
                    break;
                case UICommandType.SendMedicineRequest:
                    UIQuerySender.Instance.SendMedicineRequest();
                    break;
                case UICommandType.ShowMedicineList:
                    UIQuerySender.Instance.ShowMedicines(Value);
                    break;
                case UICommandType.ContinueMedicinesAdding:
                    UIQuerySender.Instance.ContinueMedicinesAdding();
                    break;
                case UICommandType.SendPlantsRequest:
                    UIQuerySender.Instance.SendPlantsRequest();
                    break;
                case UICommandType.ShowPlantsList:
                    UIQuerySender.Instance.ShowPlants(Value);
                    break;
                case UICommandType.ContinuePlantsAdding:
                    UIQuerySender.Instance.ContinuePlantsAdding();
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