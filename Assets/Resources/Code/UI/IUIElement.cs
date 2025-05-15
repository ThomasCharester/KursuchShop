namespace Resources.Code
{
    public interface IUIElement
    {
        public void Clear();
        public void Hide();
        public void Show();
        public void Toggle(bool show);
        public void Toggle();
    }
}