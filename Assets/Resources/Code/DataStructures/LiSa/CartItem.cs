namespace Resources.Code.DataStructures.LiSa
{
    public class CartItem
    {
        public int ItemId;
        public int CartId;
        public string GoodName;
        public int Quantity;

        public CartItem(int itemId, int cartId, string goodName, int quantity)
        {
            ItemId = itemId;
            CartId = cartId;
            GoodName = goodName;
            Quantity = quantity;
        }
    }
}