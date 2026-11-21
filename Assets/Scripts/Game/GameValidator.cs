using UnityEngine;

public enum PlayerDecision
{
    Accept,
    Reject
}

public class GameValidator : MonoBehaviour
{
    public ValidationResult ValidateDecision(
    PlayerDecision decision,
    PurchaseGenerator purchaseGenerator,
    Invoice invoice)
{
    ValidationResult result = new ValidationResult();

    bool shouldAccept = invoice.isCorrect;

    if (decision == PlayerDecision.Accept && shouldAccept)
    {
        result.decisionCorrect = true;
        result.moneyEarned = purchaseGenerator.GetCurrentPurchaseTotal();
    }
    else if (decision == PlayerDecision.Reject && !shouldAccept)
    {
        result.decisionCorrect = true;
        result.moneyEarned = purchaseGenerator.GetCurrentPurchaseTotal();
    }
    else
    {
        result.decisionCorrect = false;
        result.moneyEarned = 0;
    }

    return result;
}
}