using UnityEngine;

namespace Resources.Code.UI.DataStructures
{
    public class DataStructureElement : MonoBehaviour
    {
        [SerializeField] public UIScaling _uiScaling;
        
        private void Start()
        {
            _uiScaling.StartAnimation();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Clear()
        {
            _uiScaling.DIE();
        }

        public void Toggle(bool show)
        {
            gameObject.SetActive(show);
        }

        public void Toggle()
        {
            _uiScaling.StartAnimation();
        }

    }
}