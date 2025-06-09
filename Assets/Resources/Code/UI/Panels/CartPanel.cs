using UnityEngine;

namespace Resources.Code.UI.Panels
{
    public class CartPanel: Panel
    {
        [SerializeField] public VerticalContainer verticalContainer;

        public new void Show()
        {
            base.Show();
            
            Clear();
            
            UIQuerySender.Instance.SendCartItemsRequest();
        }

        public void Clear()
        {
            verticalContainer.Clear();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);
            
            Clear();
        }

        public new void Toggle()
        {
            base.Toggle();
            
            Clear();
            
            UIQuerySender.Instance.SendCartItemsRequest();
        }

        public void Checkout()
        {
            UIQuerySender.Instance.SendCheckOutRequest();
        }
    }
}