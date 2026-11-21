using System.Collections.Generic;
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
    public List<Product> products;

    public Invoice GenerateInvoice(PurchaseGenerator purchaseGenerator)
    {
        products = purchaseGenerator.products;

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

            switch (error)
            {
                case InvoiceError.ProductName:
                    ApplyProductNameError(invoice);
                    break;

                case InvoiceError.Quantity:
                    ApplyQuantityError(invoice);
                    break;

                case InvoiceError.Price:
                    ApplyPriceError(invoice);
                    break;

                case InvoiceError.Total:
                    ApplyTotalError(invoice);
                    break;
            }
        }

        return invoice;
    }

    private void ApplyProductNameError(Invoice invoice)
    {
        int itemIndex = Random.Range(0, invoice.items.Count);

        string originalName = invoice.items[itemIndex].productName;

        string newName;

        do
        {
            int productIndex = Random.Range(0, products.Count);
            newName = products[productIndex].name;
        }
        while (newName == originalName);

        invoice.items[itemIndex].productName = newName;
    }

    private void ApplyQuantityError(Invoice invoice)
    {
        int itemIndex = Random.Range(0, invoice.items.Count);

        int originalQuantity = invoice.items[itemIndex].quantity;

        int newQuantity;

        do
        {
            newQuantity = Random.Range(1, 4);
        }
        while (newQuantity == originalQuantity);

        invoice.items[itemIndex].quantity = newQuantity;
    }

    private void ApplyPriceError(Invoice invoice)
    {
        int itemIndex = Random.Range(0, invoice.items.Count);

        float originalPrice = invoice.items[itemIndex].price;

        float variation = Random.Range(0.8f, 1.2f);

        float newPrice = originalPrice * variation;

        invoice.items[itemIndex].price =
            Mathf.Round(newPrice * 100f) / 100f;
    }

    private void ApplyTotalError(Invoice invoice)
    {
        float variation = Random.Range(0.8f, 1.2f);

        float newTotal = invoice.total * variation;

        invoice.total =
            Mathf.Round(newTotal * 100f) / 100f;
    }
}