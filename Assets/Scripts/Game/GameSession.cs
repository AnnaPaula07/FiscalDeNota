
using System.Collections;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public int totalRounds = 10;
    public float timeBetweenRounds = 2f;

    [Header("Game Systems")]
    public PurchaseGenerator purchaseGenerator;
    public InvoiceGenerator invoiceGenerator;
    public GameValidator gameValidator;

    [Header("Interface Texts")]
    public TMP_Text roundText;
    public TMP_Text moneyText;
    public TMP_Text purchaseText;
    public TMP_Text invoiceText;
    public TMP_Text feedbackText;

    [Header("Interface Buttons")]
    public Button acceptButton;
    public Button rejectButton;

    private int currentRound = 0;
    private float money = 0;

    private Invoice currentInvoice;
    private bool decisionSubmitted = false;

    private readonly CultureInfo brazilianCulture =
        new CultureInfo("pt-BR");

    public void BeginGame()
    {
        currentRound = 0;
        money = 0;
        currentInvoice = null;
        decisionSubmitted = false;

        UpdateMoneyText();

        feedbackText.text = "";

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

        purchaseGenerator.GeneratePurchase();

        currentInvoice = invoiceGenerator.GenerateInvoice(
            purchaseGenerator
        );

        UpdateRoundText();
        UpdateMoneyText();
        UpdatePurchaseText();
        UpdateInvoiceText();

        feedbackText.text = "";

        acceptButton.interactable = true;
        rejectButton.interactable = true;

        Debug.Log("RODADA " + currentRound + "/" + totalRounds);
        Debug.Log("NOTA CORRETA: " + currentInvoice.isCorrect);
        Debug.Log("ERRO: " + currentInvoice.errorType);
    }

    private void UpdateRoundText()
    {
        roundText.text = "Rodada " + currentRound + "/" + totalRounds;
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Dinheiro: " +
            money.ToString("C2", brazilianCulture);
    }

    private void UpdatePurchaseText()
    {
        StringBuilder text = new StringBuilder();

        text.AppendLine("COMPRA");
        text.AppendLine();

        foreach (PurchaseItem item in purchaseGenerator.GetCurrentPurchase())
        {
            text.AppendLine(
                item.product.name +
                " x" +
                item.quantity +
                " - " +
                item.GetTotal().ToString("C2", brazilianCulture)
            );
        }

        text.AppendLine();
        text.Append(
            "TOTAL: " +
            purchaseGenerator.GetCurrentPurchaseTotal()
                .ToString("C2", brazilianCulture)
        );

        purchaseText.text = text.ToString();
    }

    
    private void UpdateInvoiceText()
    {
        StringBuilder text = new StringBuilder();

        text.AppendLine("NOTA FISCAL");
        text.AppendLine();

        foreach (InvoiceItem item in currentInvoice.items)
        {
            text.AppendLine(
                item.productName +
                " x" +
                item.quantity +
                " - " +
                item.GetTotal().ToString("C2", brazilianCulture)
            );
        }

        text.AppendLine();

        text.Append(
            "TOTAL: " +
            currentInvoice.total.ToString("C2", brazilianCulture)
        );

        invoiceText.text = text.ToString();
    }


    public void AcceptInvoice()
    {
        SubmitDecision(PlayerDecision.Accept);
    }

    public void RejectInvoice()
    {
        SubmitDecision(PlayerDecision.Reject);
    }

    public void SubmitDecision(PlayerDecision decision)
    {
        if (currentInvoice == null || decisionSubmitted)
        {
            return;
        }

        decisionSubmitted = true;

        acceptButton.interactable = false;
        rejectButton.interactable = false;

        ValidationResult result = gameValidator.ValidateDecision(
            decision,
            purchaseGenerator,
            currentInvoice
        );

        money += result.moneyEarned;

        UpdateMoneyText();

        if (result.decisionCorrect)
        {
            feedbackText.text =
                "DECISÃO CORRETA!\n" +
                "Você ganhou " +
                result.moneyEarned.ToString("C2", brazilianCulture);
        }
        else
        {
            feedbackText.text =
                "DECISÃO INCORRETA!\n" +
                "Você não ganhou dinheiro nesta rodada.";
        }

        Debug.Log("DECISÃO: " + decision);
        Debug.Log("DECISÃO CORRETA: " + result.decisionCorrect);
        Debug.Log("DINHEIRO GANHO: " + result.moneyEarned);
        Debug.Log("DINHEIRO ACUMULADO: " + money);

        StartCoroutine(AdvanceAfterDelay());
    }

    private IEnumerator AdvanceAfterDelay()
    {
        yield return new WaitForSeconds(timeBetweenRounds);

        currentInvoice = null;

        StartNextRound();
    }

    private void EndGame()
    {
        roundText.text = "Partida concluída!";
        purchaseText.text = "";
        invoiceText.text = "";

        feedbackText.text =
            "FIM DE JOGO!\n\n" +
            "Dinheiro final: " +
            money.ToString("C2", brazilianCulture);

        acceptButton.interactable = false;
        rejectButton.interactable = false;

        Debug.Log("FIM DA PARTIDA");
        Debug.Log("DINHEIRO FINAL: " + money);
    }
}
