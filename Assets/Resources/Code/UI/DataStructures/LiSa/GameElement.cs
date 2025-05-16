using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class GameElement : DataStructureElement
    {
        public Game game = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldGameName;
        [SerializeField] private TMP_Text textGameName;

        public void UpdateTextValues()
        {
            textGameName.text = game.gameName;
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }
        public String FormOutputValue()
        {
            return (inputFieldGameName.text == "" ? game.gameName : inputFieldGameName.text).DBReadable();
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ggm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + game.gameName.DBReadable(),
                UICommandType.SendQuery));
        }

        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ggd;" + game.gameName.DBReadable(),
                UICommandType.SendQuery));
        }
    }
}