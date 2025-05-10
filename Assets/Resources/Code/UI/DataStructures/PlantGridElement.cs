using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class PlantGridElement : MonoBehaviour
    {
        public void SendPlantAddQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppa;\'{gameObject.GetComponent<IdField>().field.text}\',\'{gameObject.GetComponent<NameField>().field.text}\'",
                UICommandType.SendQuery));
            UIQuerySender.Instance.StartPlantAdding();
        }
    }
}