
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int totalRounds = 10;

    private int currentRound = 0;
    private float money = 0;

    private Invoice currentInvoice;
    private bool decisionSubmitted = false;

    public PurchaseGenerator purchaseGenerator;
    public InvoiceGenerator invoiceGenerator;
    public GameValidator gameValidator;

    public void BeginGame()
    {
        currentRound = 0;
        money = 0;

        Debug.Log("================================");
        Debug.Log("INICIANDO PARTIDA");

        StartNextRound();
    }

    private void StartGame()
    {
        currentRound = 0;
        money = 0;

        Debug.Log("================================");
        Debug.Log("INICIANDO PARTIDA");

        StartNextRound();
    }

    private void StartNextRound()
    {
        if (currentRound >= totalRounds)
        {
            EndGame();
            return;
        }

        currentRound++;
        decisionSubmitted = false;

        Debug.Log("================================");
        Debug.Log("RODADA " + currentRound + "/" + totalRounds);

        purchaseGenerator.GeneratePurchase();

        currentInvoice = invoiceGenerator.GenerateInvoice(purchaseGenerator);

        Debug.Log("===== NOTA FISCAL =====");

        foreach (InvoiceItem item in currentInvoice.items)
        {
            Debug.Log(
                item.productName +
                " x" +
                item.quantity +
                " = R$ " +
                item.GetTotal().ToString("F2")
            );
        }

        Debug.Log("TOTAL DA NOTA: R$ " + currentInvoice.total.ToString("F2"));
        Debug.Log("NOTA CORRETA: " + currentInvoice.isCorrect);
        Debug.Log("ERRO: " + currentInvoice.errorType);
    }

    public void SubmitDecision(PlayerDecision decision)
    {
        if (currentInvoice == null || decisionSubmitted)
        {
            return;
        }

        decisionSubmitted = true;

        ValidationResult result = gameValidator.ValidateDecision(
            decision,
            purchaseGenerator,
            currentInvoice
        );

        money += result.moneyEarned;

        Debug.Log("===== RESULTADO DA DECISÃO =====");
        Debug.Log("DECISÃO: " + decision);
        Debug.Log("DECISÃO CORRETA: " + result.decisionCorrect);
        Debug.Log("DINHEIRO GANHO: R$ " + result.moneyEarned.ToString("F2"));
        Debug.Log("DINHEIRO ACUMULADO: R$ " + money.ToString("F2"));
    }

    public void AdvanceToNextRound()
    {
        if (!decisionSubmitted)
        {
            return;
        }

        currentInvoice = null;

        StartNextRound();
    }

    private void EndGame()
    {
        Debug.Log("================================");
        Debug.Log("FIM DA PARTIDA");
        Debug.Log("DINHEIRO FINAL: R$ " + money.ToString("F2"));
    }
}