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
                    UIQuerySender.Instance.SetExceptionText(Value);
                    break;
                case UICommandType.AuthoriseActivate:
                    UIQuerySender.Instance.ActiveAuthorisePanel(true);
                    break;
                case UICommandType.AuthoriseDeactivate:
                    UIQuerySender.Instance.ActiveAuthorisePanel(false);
                    break;
                case UICommandType.ShowPlantList:
                    UIQuerySender.Instance.ShowPlantsGrid(Value);
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