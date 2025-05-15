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
        public void Execute()
        {
            switch (Type)
            {
                case UICommandType.SetExceptionText:
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
                case UICommandType.ShowGoodList:
                    UIQuerySender.Instance.ShowGoods(Value);
                    break;
                case UICommandType.SendQuery:
                    UIQuerySender.Instance.SendQuery(Value);
                    break;
                case UICommandType.MakeAdmin:
                    UIQuerySender.Instance.EnterAdminMode();
                    break;
            }
        }

        public void Undo()
        {
            
        }
    }
}