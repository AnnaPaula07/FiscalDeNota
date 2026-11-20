using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int totalRounds = 10;

    private int currentRound = 0;
    private float money = 0;

    public PurchaseGenerator purchaseGenerator;

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

            Debug.Log(
                "Valor da compra: R$ " +
                purchaseGenerator.GetCurrentPurchaseTotal().ToString("F2")
            );
        }

        Debug.Log("================================");
        Debug.Log("FIM DA PARTIDA");
        Debug.Log("Dinheiro: R$ " + money.ToString("F2"));
    }
}