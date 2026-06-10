using UnityEngine;

public class PlayerExperienceCollector : MonoBehaviour
{
    [SerializeField] private int currentExperience = 0;

    public int CurrentExperience
    {
        get { return currentExperience; }
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        Debug.Log("Player EXP: " + currentExperience);
    }
}
