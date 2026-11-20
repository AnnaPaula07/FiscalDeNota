[System.Serializable]
public class InvoiceItem
{
    public string productName;
    public int quantity;
    public float price;

    public float GetTotal()
    {
        return quantity * price;
    }
}