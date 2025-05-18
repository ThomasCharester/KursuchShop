using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class GoodsPanel : Panel
    {
        [SerializeField] public GridContainer gridContainer;

        public new void Show()
        {
            base.Show();
            
            Clear();
            
            UIQuerySender.Instance.SendGoodsRequest();
        }

        public void Clear()
        {
            gridContainer.Clear();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);
            
            Clear();
            
            if(show) UIQuerySender.Instance.SendGoodsRequest();
        }

        public new void Toggle()
        {
            base.Toggle();
            
            Clear();
        }
    }
}