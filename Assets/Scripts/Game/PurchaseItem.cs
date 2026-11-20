[System.Serializable]
public class PurchaseItem
{
    public Product product;
    public int quantity;

    public float GetTotal()
    {
        return product.price * quantity;
    }
}