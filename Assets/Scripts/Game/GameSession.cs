using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int totalRounds = 10;

    private int currentRound = 0;
    private float money = 0;

    public PurchaseGenerator purchaseGenerator;
    public InvoiceGenerator invoiceGenerator;
    public GameValidator gameValidator;

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

            PlayerDecision testDecision = invoice.isCorrect
            ? PlayerDecision.Accept
            : PlayerDecision.Reject;

            ValidationResult result = gameValidator.ValidateDecision(
                testDecision,
                purchaseGenerator,
                invoice
            );

            money += result.moneyEarned;

            Debug.Log("DECISÃO: " + testDecision);
            Debug.Log("DECISÃO CORRETA: " + result.decisionCorrect);
            Debug.Log("DINHEIRO GANHO: R$ " + result.moneyEarned.ToString("F2"));
            }

        Debug.Log("================================");
        Debug.Log("FIM DA PARTIDA");
        Debug.Log("Dinheiro: R$ " + money.ToString("F2"));
    }
}