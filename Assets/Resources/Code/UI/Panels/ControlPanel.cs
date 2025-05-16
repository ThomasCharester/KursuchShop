using UnityEngine;

namespace Resources.Code
{
    public class ControlPanel : Panel
    {
        [Header("SubMenus")] 
        [SerializeField] private GameObject adminMenu;
        [SerializeField] private GameObject sellerMenu;
        [SerializeField] private GameObject accountMenu;

        public void ToggleAdminMenu(bool show)
        {
            adminMenu.SetActive(show);
        }
        public void ToggleSellerMenu(bool show)
        {
            sellerMenu.SetActive(show);
        }
        public void ToggleAccountMenu(bool show)
        {
            accountMenu.SetActive(show);
        }
    }
}