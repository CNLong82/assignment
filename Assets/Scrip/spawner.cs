using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;    
    public float spawnInterval = 10f;   

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
} 

