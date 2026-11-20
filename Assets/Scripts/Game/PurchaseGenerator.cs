using System.Collections.Generic;
using UnityEngine;

public class PurchaseGenerator : MonoBehaviour
{
    public List<Product> products = new List<Product>();

    private void Start()
    {
        foreach (Product product in products)
        {
            Debug.Log(product.name + " - R$ " + product.price);
        }
    }
}