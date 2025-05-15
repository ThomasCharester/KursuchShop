using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class ExceptionPanel : MonoBehaviour, IUIElement
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text _exceptionText;
        
        [SerializeField] private UISliding _sliding;
        
        public void SetExceptionText(string exceptionText) => _exceptionText.text = exceptionText;

        public void Show()
        {
            _sliding.StartAnimation(true);
            //gameObject.SetActive(true);
        }

        public void Hide()
        {
            _sliding.StartAnimation(false);
            //gameObject.SetActive(false);
        }

        public void Clear()
        {
            _exceptionText.text = "";
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);
            //gameObject.SetActive(show);
        }
        
    }
}