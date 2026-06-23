using UnityEngine;

public class KillCountTracker : MonoBehaviour
{
    // 싱글톤.
    public static KillCountTracker Instance;

    private int killCount = 0;

    public int KillCount
    {
        get { return killCount; }
    }

    private void Awake()
    {
        Instance = this;        
    }

    public void AddKillCount()
    {
        killCount++;
    }
}
