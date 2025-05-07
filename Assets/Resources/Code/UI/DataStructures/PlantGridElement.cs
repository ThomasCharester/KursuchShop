using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class PlantGridElement : MonoBehaviour
    {
        
        public void SendPlantAddQuery()
        {
            SimpleTCPClient.Instance.SendQuery(
                $"ppa;\'{gameObject.GetComponent<IdField>().field.text}\',\'{gameObject.GetComponent<NameField>().field.text}\'");
            
            UIQuerySender.Instance.AddGridElementPlantAdd();
        }
    }
}