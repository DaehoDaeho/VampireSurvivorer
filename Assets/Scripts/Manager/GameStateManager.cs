using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] private GameState currentState = GameState.Playing;

    public GameState CurrentState => currentState;

    public bool IsPlaying => currentState == GameState.Playing;
    public bool IsGameOver => currentState == GameState.GameOver;

    private void Awake()
    {
        Instance = this;

        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            TogglePause();
        }
    }

    public void ChangeState(GameState nextState)
    {
        if(currentState == nextState)
        {
            return;
        }

        currentState = nextState;

        ApplyTimeScaleByState();
    }

    void ApplyTimeScaleByState()
    {
        if(currentState == GameState.Playing)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }

    public void TogglePause()
    {
        if(currentState == GameState.GameOver || currentState == GameState.LevelUp)
        {
            return;
        }

        ChangeState(currentState == GameState.Playing ? GameState.Pause : GameState.Playing);
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Pause)
        {
            ChangeState(GameState.Playing);
        }
    }

    public void EnterLevelUp()
    {
        if(currentState == GameState.Playing)
        {
            ChangeState(GameState.LevelUp);
        }
    }

    public void ExitLevelUp()
    {
        if (currentState == GameState.LevelUp)
        {
            ChangeState(GameState.Playing);
        }
    }

    public void EnterGameOver()
    {
        ChangeState(GameState.GameOver);
    }
}
