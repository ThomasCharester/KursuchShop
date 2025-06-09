namespace Resources.Code.UI.Panels
{
    public class LoadingPanel: Panel
    {
        public new void Show()
        {
            gameObject.SetActive(true);
            
            _hidden = false;
        }

        public new void Hide()
        {
            gameObject.SetActive(false);
            
            _hidden = true;
        }
        public void Clear()
        {
        }

        public new void Toggle(bool show)
        {
            gameObject.SetActive(show);
        }

        public new void Toggle()
        {
            gameObject.SetActive(!_hidden);
            _hidden = !_hidden;
        }
    }
}