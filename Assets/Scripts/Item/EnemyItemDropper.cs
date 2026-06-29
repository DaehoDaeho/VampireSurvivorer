using Unity.VisualScripting;
using UnityEngine;

public class EnemyItemDropper : MonoBehaviour
{
    [SerializeField] private GameObject healItemPrefab;
    [SerializeField] private GameObject magnetItemPrefab;
    [SerializeField] private GameObject speedBoostItemPrefab;

    [SerializeField] private float dropChance = 0.35f;

    public void TryDropItem()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if(randomValue > dropChance)
        {
            //return;
        }

        GameObject selectedItemPrefab = GetRandomItemPrefab();

        Instantiate(selectedItemPrefab, transform.position, Quaternion.identity);
    }

    GameObject GetRandomItemPrefab()
    {
        int randomIndex = Random.Range(0, 3);
        if(randomIndex == 0)
        {
            return healItemPrefab;
        }
        else if(randomIndex == 1)
        {
            return magnetItemPrefab;
        }

        return speedBoostItemPrefab;
    }    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
