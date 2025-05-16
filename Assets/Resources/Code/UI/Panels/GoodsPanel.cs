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
        }

        public void Clear()
        {
            gridContainer.Clear();
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
        }
    }
}