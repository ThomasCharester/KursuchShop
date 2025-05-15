using UnityEngine;

namespace Resources.Code
{
    public class ControlPanel : MonoBehaviour, IUIElement
    {
        [Header("SubMenus")] 
        [SerializeField] private GameObject adminMenu;
        [SerializeField] private GameObject sellerMenu;
        [SerializeField] private GameObject accountMenu;
        
        private UISliding _sliding;
        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void ToggleAdminMenu(bool show)
        {
            adminMenu.SetActive(show);
        }
        public void ToggleSellerMenu(bool show)
        {
            sellerMenu.SetActive(show);
        }
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
            //
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);
            //gameObject.SetActive(show);
        }

        
    }
}