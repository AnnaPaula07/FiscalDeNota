
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gameplayPanel;

    public GameSession gameSession;

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gameplayPanel.SetActive(true);

        gameSession.BeginGame();
    }

    public void ReturnToMainMenu()
    {
        gameplayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
