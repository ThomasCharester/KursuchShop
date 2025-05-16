using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code
{
    public class AdminPanel: Panel
    {
        [SerializeField] public VerticalContainer verticalContainer;

        public new void Show()
        {
            base.Show();
            
            Clear();
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
        }
    }
}