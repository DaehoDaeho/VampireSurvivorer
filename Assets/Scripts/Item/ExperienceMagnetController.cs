using UnityEngine;

public class ExperienceMagnetController : MonoBehaviour
{
    private float remainTime = 0.0f;

    public bool IsMagnetActive => remainTime > 0.0f;
    
    public void Activate(float duration)
    {
        if(remainTime < duration)
        {
            remainTime = duration;
        }
    }

    private void Update()
    {
        if(remainTime > 0.0f)
        {
            remainTime -= Time.deltaTime;

            if(remainTime < 0.0f)
            {
                remainTime = 0.0f;
            }
        }
    }
}
