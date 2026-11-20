using System.Collections.Generic;
using UnityEngine;

public class PurchaseGenerator : MonoBehaviour
{
    public List<Product> products = new List<Product>();

    private List<PurchaseItem> currentPurchase = new List<PurchaseItem>();

    private void Start()
    {
        GeneratePurchase();
    }

    private void GeneratePurchase()
    {
        currentPurchase.Clear();

        int numberOfProducts = Random.Range(1, 6);

        List<Product> availableProducts = new List<Product>(products);

        for (int i = 0; i < numberOfProducts; i++)
        {
            int randomIndex = Random.Range(0, availableProducts.Count);

            Product selectedProduct = availableProducts[randomIndex];

            availableProducts.RemoveAt(randomIndex);

            PurchaseItem item = new PurchaseItem();
            item.product = selectedProduct;
            item.quantity = Random.Range(1, 4);

            currentPurchase.Add(item);
        }

        PrintPurchase();
    }

    private void PrintPurchase()
{
    Debug.Log("===== NOVA COMPRA =====");

    foreach (PurchaseItem item in currentPurchase)
    {
        Debug.Log(
            item.product.name +
            " x" +
            item.quantity +
            " = R$ " +
            item.GetTotal().ToString("F2")
        );
    }

    float total = CalculatePurchaseTotal();

    Debug.Log("TOTAL: R$ " + total.ToString("F2"));
}

private float CalculatePurchaseTotal()
{
    float total = 0;

    foreach (PurchaseItem item in currentPurchase)
    {
        total += item.GetTotal();
    }

    return total;
}

}