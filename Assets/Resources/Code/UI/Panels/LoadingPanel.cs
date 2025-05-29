using UnityEngine;

namespace Resources.Code.UI.Panels
{
    public class LoadingPanel: Panel
    {
       
        public new void Show()
        {
            //base.Show();
            
            gameObject.SetActive(true);
        }
        public new void Hide()
        {
            //base.Show();
            
            gameObject.SetActive(false);
        }

        public new void Toggle(bool show)
        {
            //base.Toggle(show);
            
            gameObject.SetActive(show);
        }

        public void StartLoading()
        {
            
        }
    }
}