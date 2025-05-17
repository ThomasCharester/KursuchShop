using System;
using Resources.Code.DataStructures.LiSa;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures
{
    public class AdminKeyElement: DataStructureElement
    {
        public AdminKey adminKey = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldAdminKey;
        [SerializeField] private TMP_Text textAdminKey;

        public void UpdateTextValues()
        {
            textAdminKey.text = adminKey.adminKey;
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }
        public String FormOutputValue()
        {
            return (inputFieldAdminKey.text == "" ? adminKey.adminKey : inputFieldAdminKey.text).DBReadable();
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"akm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + adminKey.adminKey.DBReadable(),
                UICommandType.SendQuery));
        }

        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"akd;" + adminKey.adminKey.DBReadable(),
                UICommandType.SendQuery));
        }
    }
}