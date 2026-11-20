using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int totalRounds = 10;

    private int currentRound = 0;
    private float money = 0;

    public PurchaseGenerator purchaseGenerator;
    public InvoiceGenerator invoiceGenerator;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        for (int i = 0; i < totalRounds; i++)
        {
            currentRound++;

            Debug.Log("================================");
            Debug.Log("RODADA " + currentRound + "/" + totalRounds);

            purchaseGenerator.GeneratePurchase();

            Invoice invoice = invoiceGenerator.GenerateInvoice(purchaseGenerator);

            Debug.Log("===== NOTA FISCAL =====");

            foreach (InvoiceItem item in invoice.items)
            {
                Debug.Log(
                    item.productName +
                    " x" +
                    item.quantity +
                    " = R$ " +
                    item.GetTotal().ToString("F2")
                );
            }

            Debug.Log("TOTAL DA NOTA: R$ " + invoice.total.ToString("F2"));
            Debug.Log("NOTA CORRETA: " + invoice.isCorrect);
            Debug.Log("ERRO: " + invoice.errorType);

        }

        Debug.Log("================================");
        Debug.Log("FIM DA PARTIDA");
        Debug.Log("Dinheiro: R$ " + money.ToString("F2"));
    }
}