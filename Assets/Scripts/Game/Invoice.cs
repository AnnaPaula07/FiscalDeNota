using System.Collections.Generic;

[System.Serializable]
public class Invoice
{
    public List<InvoiceItem> items = new List<InvoiceItem>();
    public float total;
    public bool isCorrect;
    public string errorType;
}