using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.Instance == null || pausePanel == null)
        {
            return;
        }

        bool isPaused = GameStateManager.Instance.CurrentState == GameState.Pause;
        pausePanel.SetActive(isPaused);
    }
}
