using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class AdminPanel: MonoBehaviour, IUIElement
    {
        private UISliding _sliding;

        private bool _hidden = true;
        public bool Hidden => _hidden;

        public void SendModifyRequest()
        {
           
        }

        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void Show()
        {
            _sliding.StartAnimation(true);
            Clear();
            _hidden = false;
            //gameObject.SetActive(true);
        }

        public void Hide()
        {
            _sliding.StartAnimation(false);
            _hidden = true;
            //gameObject.SetActive(false);
        }

        public void Clear()
        {
            
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);
            
            if (show)
                Clear();
            
            _hidden = !show;
        }

        public void Toggle()
        {
            _hidden = !_hidden;
            _sliding.StartAnimation();

            if (_hidden)
                Clear();
        }
    }
}