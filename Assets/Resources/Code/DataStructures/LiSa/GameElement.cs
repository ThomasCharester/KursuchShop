using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class GameElement : MonoBehaviour, IUIElement
    {
        public Game game = new();
        [SerializeField] private GameObject outputPanel;
        
        [SerializeField] private TMP_Text textGameName;
        
        [SerializeField] private UIScaling _uiScaling;

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
        public void UpdateTextValues()
        {
            textGameName.text = game.gameName;
        }
    }
}