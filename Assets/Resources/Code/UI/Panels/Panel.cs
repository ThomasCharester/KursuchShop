using UnityEngine;

namespace Resources.Code
{
    public class Panel : MonoBehaviour
    {
        protected UISliding _sliding;

        protected bool _hidden = true;
        public bool Hidden => _hidden;

        protected void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void Show()
        {
            _sliding.StartAnimation(true);
            
            _hidden = false;
        }

        public void Hide()
        {
            _sliding.StartAnimation(false);
            
            _hidden = true;
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);

            _hidden = !show;
        }

        public void Toggle()
        {
            _hidden = !_hidden;
            _sliding.StartAnimation();
        }
    }
}