using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance == null || gameOverPanel == null)
        {
            return;
        }

        bool isGameOver = GameStateManager.Instance.CurrentState == GameState.GameOver;
        gameOverPanel.SetActive(isGameOver);
    }
}
