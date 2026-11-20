using UnityEngine;

public enum InvoiceError
{
    None,
    ProductName,
    Quantity,
    Price,
    Total
}

public class InvoiceGenerator : MonoBehaviour
{
    public Invoice GenerateInvoice(PurchaseGenerator purchaseGenerator)
    {
        Invoice invoice = new Invoice();

        foreach (PurchaseItem purchaseItem in purchaseGenerator.GetCurrentPurchase())
        {
            InvoiceItem invoiceItem = new InvoiceItem();

            invoiceItem.productName = purchaseItem.product.name;
            invoiceItem.quantity = purchaseItem.quantity;
            invoiceItem.price = purchaseItem.product.price;

            invoice.items.Add(invoiceItem);
        }

        bool correct = Random.value < 0.5f;

        invoice.total = purchaseGenerator.GetCurrentPurchaseTotal();
        invoice.isCorrect = correct;

        if (correct)
        {
            invoice.errorType = InvoiceError.None;
        }
        else
        {
            InvoiceError error = (InvoiceError)Random.Range(1, 5);

            invoice.errorType = error;
        }

        return invoice;
    }
}