using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class ExceptionPanel : Panel
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text _exceptionText;
        
        public void SetExceptionText(string exceptionText) => _exceptionText.text = exceptionText;

        public void Clear()
        {
            _exceptionText.text = "";
        }
    }
}