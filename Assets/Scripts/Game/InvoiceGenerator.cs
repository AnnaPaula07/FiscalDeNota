using UnityEngine;

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

        invoice.total = purchaseGenerator.GetCurrentPurchaseTotal();
        invoice.isCorrect = true;
        invoice.errorType = "Nenhum";

        return invoice;
    }
}