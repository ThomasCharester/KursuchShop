using UnityEngine;

namespace Resources.Code.Panels
{
    public class SellerPanel: Panel
    {
        [SerializeField] public GridContainer gridContainer;

        public new void Show()
        {
            base.Show();
            
            Clear();
        }

        public void Clear()
        {
            gridContainer.Clear();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);
            
            Clear();
            
            if(show) UIQuerySender.Instance.SendGoodsEditRequest();
        }

        public new void Toggle()
        {
            base.Toggle();
            
            Clear();
        }
    }
}