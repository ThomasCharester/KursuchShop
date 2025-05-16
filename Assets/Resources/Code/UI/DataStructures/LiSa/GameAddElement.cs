using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class GameAddElement : DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldGameName;

        public String FormOutputValue()
        {
            if (inputFieldGameName.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
                    UICommandType.ShowException));
            }

            return inputFieldGameName.text.DBReadable();
        }

        public void SendAddQuery()
        {
            if (!inputFieldGameName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gga;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueGamesAdding));

            inputFieldGameName.enabled = false;
        }
    }
}